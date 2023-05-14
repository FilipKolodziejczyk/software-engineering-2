resource "aws_ecr_repository" "backend_ecr" {
  name = "${var.app_name}-${var.app_environment}-backend-ecr"
  
  tags = {
    Name        = "${var.app_name}-backend-ecr"
    Environment = var.app_environment
  }
}

resource "aws_ecr_repository" "frontend_client_ecr" {
  name = "${var.app_name}-${var.app_environment}-frontend-client-ecr"
  
  tags = {
    Name        = "${var.app_name}-frontend-client-ecr"
    Environment = var.app_environment
  }
}

resource "aws_ecr_repository" "frontend_delivery_ecr" {
  name = "${var.app_name}-${var.app_environment}-frontend-delivery-ecr"
  
  tags = {
    Name        = "${var.app_name}-frontend-delivery-ecr"
    Environment = var.app_environment
  }
}

resource "aws_ecr_repository" "frontend_shop_ecr" {
  name = "${var.app_name}-${var.app_environment}-frontend_shop-ecr"
  
  tags = {
    Name        = "${var.app_name}-frontend_shop-ecr"
    Environment = var.app_environment
  }
}

output "backend_repository_url" {
  value = aws_ecr_repository.backend_ecr.repository_url
}

output "frontend_client_repository_url" {
  value = aws_ecr_repository.frontend_client_ecr.repository_url
}

output "frontend_delivery_repository_url" {
  value = aws_ecr_repository.frontend_delivery_ecr.repository_url
}

output "frontend_shop_repository_url" {
  value = aws_ecr_repository.frontend_shop_ecr.repository_url
}