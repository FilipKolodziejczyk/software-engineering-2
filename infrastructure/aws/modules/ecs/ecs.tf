resource "aws_ecs_cluster" "ecs_cluster" {
    name  = "${var.app_name}-${var.app_environment}-ecs-cluster"

    tags = {
        Name        = "${var.app_name}-ecs-cluster"
        Environment = var.app_environment
    }
}