resource "aws_security_group" "backend_sg" {
    name   = "${var.app_name}-${var.app_environment}-backend-sg"
    vpc_id = aws_vpc.default_vpc.id

    ingress {
        from_port       = 80
        to_port         = 80
        protocol        = "tcp"
        cidr_blocks     = ["0.0.0.0/0"]
    }

    ingress {
        from_port       = 443
        to_port         = 443
        protocol        = "tcp"
        cidr_blocks     = ["0.0.0.0/0"]
    }

    egress {
        from_port       = 0
        to_port         = 0
        protocol        = "-1"
        cidr_blocks     = ["0.0.0.0/0"]
    }
    
    tags = {
        Name        = "${var.app_name}-backend-sg"
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
        cidr_blocks     = ["0.0.0.0/0"]
        security_groups = [aws_security_group.backend_sg.id]
    }

    egress {
        from_port       = 0
        to_port         = 0
        protocol        = "-1"
        cidr_blocks     = ["0.0.0.0/0"]
    }

    tags = {
        Name        = "${var.app_name}-sqlserver-sg"
        Environment = var.app_environment
    }
}

output "backend_sg_id" {
    value = aws_security_group.backend_sg.id
}

output "sqlserver_sg_id" {
    value = aws_security_group.sqlserver_sg.id
}
