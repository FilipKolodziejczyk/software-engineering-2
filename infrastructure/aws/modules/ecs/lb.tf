resource "aws_lb" "default" {
  name               = "${var.app_name}-${var.app_environment}-lb"
  security_groups    = [var.lb_sg_id]
  subnets            = var.subnet_ids
  internal           = false
  load_balancer_type = "application"
  ip_address_type    = "ipv4"


  tags = {
    Name        = "${var.app_name}-lb"
    Environment = var.app_environment
  }
}

resource "aws_lb_listener" "http_listener" {
  load_balancer_arn = aws_lb.default.arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.frontend_client.id
  }
}

resource "aws_lb_target_group" "backend" {
  name        = "${var.app_name}-${var.app_environment}-backend-tg"
  port        = 80
  protocol    = "HTTP"
  vpc_id      = var.vpc_id
  target_type = "ip"

  health_check {
    path                = "/swagger/index.html"
    port                = 80
    protocol            = "HTTP"
    healthy_threshold   = 2
    unhealthy_threshold = 2
    timeout             = 5
    interval            = 60
  }

  tags = {
    Name        = "${var.app_name}-backend-tg"
    Environment = var.app_environment
  }
}

resource "aws_lb_target_group" "frontend_client" {
  name        = "${var.app_name}-${var.app_environment}-client-tg"
  port        = 80
  protocol    = "HTTP"
  vpc_id      = var.vpc_id
  target_type = "ip"

  health_check {
    path                = ""
    protocol            = "HTTP"
    healthy_threshold   = 2
    unhealthy_threshold = 2
    timeout             = 5
    interval            = 60
  }

  tags = {
    Name        = "${var.app_name}-frontend-client-tg"
    Environment = var.app_environment
  }
}

resource "aws_lb_target_group" "frontend_delivery" {
  name        = "${var.app_name}-${var.app_environment}-delivery-tg"
  port        = 80
  protocol    = "HTTP"
  vpc_id      = var.vpc_id
  target_type = "ip"

  health_check {
    path                = "/delivery/"
    protocol            = "HTTP"
    healthy_threshold   = 2
    unhealthy_threshold = 2
    timeout             = 5
    interval            = 60
  }

  tags = {
    Name        = "${var.app_name}-frontend-delivery-tg"
    Environment = var.app_environment
  }
}

resource "aws_lb_target_group" "frontend_shop" {
  name        = "${var.app_name}-${var.app_environment}-shop-tg"
  port        = 80
  protocol    = "HTTP"
  vpc_id      = var.vpc_id
  target_type = "ip"

  health_check {
    path                = "/shop/"
    protocol            = "HTTP"
    healthy_threshold   = 2
    unhealthy_threshold = 2
    timeout             = 5
    interval            = 60
  }

  tags = {
    Name        = "${var.app_name}-frontend-shop-tg"
    Environment = var.app_environment
  }
}

resource "aws_lb_listener_rule" "backend" {
  listener_arn = aws_lb_listener.http_listener.arn
  priority     = 100

  action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.backend.id
  }

  condition {
    path_pattern {
      values = ["/api/*", "/swagger/*"]
    }
  }
}

resource "aws_lb_listener_rule" "frontend_delivery" {
  listener_arn = aws_lb_listener.http_listener.arn
  priority     = 200

  action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.frontend_delivery.id
  }

  condition {
    path_pattern {
      values = ["/delivery/*"]
    }
  }
}

resource "aws_lb_listener_rule" "frontend_shop" {
  listener_arn = aws_lb_listener.http_listener.arn
  priority     = 300

  action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.frontend_shop.id
  }

  condition {
    path_pattern {
      values = ["/shop/*"]
    }
  }
}

output "backend_lb_tg" {
  value = aws_lb_target_group.backend.arn
}

output "frontend_client_lb_tg" {
  value = aws_lb_target_group.frontend_client.arn
}

output "frontend_delivery_lb_tg" {
  value = aws_lb_target_group.frontend_delivery.arn
}

output "frontend_shop_lb_tg" {
  value = aws_lb_target_group.frontend_shop.arn
}