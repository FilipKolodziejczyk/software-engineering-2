resource "aws_ecs_task_definition" "backend_task_definition" {
  family                   = "${var.app_name}-${var.app_environment}-backend-task-definition"
  network_mode             = "awsvpc"
  requires_compatibilities = ["EC2"]
  memory                   = 512
  cpu                      = 2
  execution_role_arn       = var.ecs_agent_role_arn
  task_role_arn            = var.ecs_agent_role_arn

  container_definitions = file("${path.module}/definition.json")

  tags = {
    Name        = "${var.app_name}-backend-task-definition"
    Environment = var.app_environment
  }
}

resource "aws_ecs_service" "backend_service" {
  name          = "${var.app_name}-${var.app_environment}-backend-service"
  cluster       = var.cluster_id
  desired_count = 1

  tags = {
    Name        = "${var.app_name}-backend-service"
    Environment = var.app_environment
  }
}
