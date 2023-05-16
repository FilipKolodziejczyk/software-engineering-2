resource "aws_ecs_task_definition" "frontend-client_task_definition" {
  family                   = "${var.app_name}-${var.app_environment}-frontend-client-task-definition"
  network_mode             = "awsvpc"
  requires_compatibilities = ["EC2"]
  memory                   = 512
  cpu                      = 512
  execution_role_arn       = var.ecs_agent_role_arn
  task_role_arn            = var.ecs_agent_role_arn

  container_definitions = templatefile("${path.module}/definition.tftpl", {
    repository_url         = var.repository_url
    env_file_arn           = "arn:aws:s3:::${var.app_name}-${var.app_environment}-env-files/frontend-client.env"
    default_image_tag      = var.default_image_tag
  })

  tags = {
    Name        = "${var.app_name}-frontend-client-task-definition"
    Environment = var.app_environment
  }
}

resource "aws_ecs_service" "frontend-client_service" {
  name            = "${var.app_name}-${var.app_environment}-frontend-client-service"
  cluster         = var.cluster_id
  desired_count   = 1
  task_definition = aws_ecs_task_definition.frontend-client_task_definition.arn

  network_configuration {
    subnets = var.subnet_ids
    security_groups = [var.sg_id]
    assign_public_ip = false
  }

  load_balancer {
    target_group_arn = var.lb_tg
    container_name   = "frontend-client"
    container_port   = 80
  }

  tags = {
    Name        = "${var.app_name}-frontend-client-service"
    Environment = var.app_environment
  }
}
