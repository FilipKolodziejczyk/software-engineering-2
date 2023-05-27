resource "aws_security_group" "loadbalancer_sg" {
  name   = "${var.app_name}-${var.app_environment}-loadbalancer-sg"
  vpc_id = aws_vpc.default_vpc.id

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }
}

resource "aws_security_group" "backend_sg" {
  name   = "${var.app_name}-${var.app_environment}-backend-sg"
  vpc_id = aws_vpc.default_vpc.id

  ingress {
    from_port       = 80
    to_port         = 80
    protocol        = "tcp"
    security_groups = [aws_security_group.loadbalancer_sg.id]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name        = "${var.app_name}-backend-sg"
    Environment = var.app_environment
  }
}

resource "aws_security_group" "frontend_sg" {
  name   = "${var.app_name}-${var.app_environment}-frontend-sg"
  vpc_id = aws_vpc.default_vpc.id

  ingress {
    from_port       = 5173
    to_port         = 5173
    protocol        = "tcp"
    security_groups = [aws_security_group.loadbalancer_sg.id]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name        = "${var.app_name}-frontend-sg"
    Environment = var.app_environment
  }
}

resource "aws_security_group" "sqlserver_sg" {
  name   = "${var.app_name}-${var.app_environment}-sqlserver-sg"
  vpc_id = aws_vpc.default_vpc.id

  ingress {
    from_port       = 1433
    to_port         = 1433
    protocol        = "tcp"
    security_groups = [aws_security_group.backend_sg.id]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name        = "${var.app_name}-sqlserver-sg"
    Environment = var.app_environment
  }
}

output "loadbalancer_sg_id" {
  value = aws_security_group.loadbalancer_sg.id
}

output "backend_sg_id" {
  value = aws_security_group.backend_sg.id
}

output "frontend_sg_id" {
  value = aws_security_group.frontend_sg.id
}

output "sqlserver_sg_id" {
  value = aws_security_group.sqlserver_sg.id
}
