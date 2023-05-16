resource "aws_lb" "default" {
  name               = "${var.app_name}-${var.app_environment}-lb"
  security_groups    = [var.backend_sg_id, var.frontend_sg_id]
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

  tags = {
    Name        = "${var.app_name}-frontend-client-tg"
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

output "backend_lb_tg" {
  value = aws_lb_target_group.backend.arn
}

output "frontend_client_lb_tg" {
  value = aws_lb_target_group.frontend_client.arn
}