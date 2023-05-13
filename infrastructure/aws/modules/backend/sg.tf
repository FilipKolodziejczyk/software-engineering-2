resource "aws_security_group" "backend_sg" {
    name   = "${var.app_name}-${var.app_environment}-backend-sg"
    vpc_id = var.vpc_id

    ingress {
        from_port       = 80
        to_port         = 80
        protocol        = "tcp"
        cidr_blocks     = ["0.0.0.0/0"]
    }

    egress {
        from_port       = 0
        to_port         = 65535
        protocol        = "-1"
        cidr_blocks     = ["0.0.0.0/0"]
    }
    
    tags = {
        Name        = "${var.app_name}-backend-sg"
        Environment = var.app_environment
    }
}

output "sg_id" {
    value = aws_security_group.backend_sg.id
}