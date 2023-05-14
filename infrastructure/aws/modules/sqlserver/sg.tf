resource "aws_security_group" "sqlserver_sg" {
    name   = "${var.app_name}-${var.app_environment}-sqlserver-sg"
    vpc_id = var.vpc_id

    ingress {
        from_port       = 1433
        to_port         = 1433
        protocol        = "tcp"
        cidr_blocks     = ["0.0.0.0/0"]
        security_groups = [var.backend_sg_id]
    }

    egress {
        from_port       = 0
        to_port         = 65535
        protocol        = "-1"
        cidr_blocks     = ["0.0.0.0/0"]
    }

    tags = {
        Name        = "${var.app_name}-sqlserver-sg"
        Environment = var.app_environment
    }
}