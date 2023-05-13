resource "aws_ecs_service" "backend_service" {
    name    = "${var.app_name}-${var.app_environment}-backend-service"
    cluster = var.cluster_id
    desired_count = 1

    tags = {
        Name        = "${var.app_name}-backend-service"
        Environment = var.app_environment
    }
}