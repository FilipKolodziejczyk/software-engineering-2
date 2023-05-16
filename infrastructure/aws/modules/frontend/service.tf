resource "aws_ecs_task_definition" "frontend_task_definition" {
  family                   = "${var.app_name}-${var.app_environment}-frontend-${var.module_name}-task-definition"
  network_mode             = "awsvpc"
  requires_compatibilities = ["EC2"]
  memory                   = 512
  cpu                      = 512
  execution_role_arn       = var.ecs_agent_role_arn
  task_role_arn            = var.ecs_agent_role_arn

  container_definitions = templatefile("${path.module}/definition.tftpl", {
    repository_url         = var.repository_url
    env_file_arn           = "arn:aws:s3:::${var.app_name}-${var.app_environment}-env-files/frontend-${var.module_name}.env"
    default_image_tag      = var.default_image_tag
    logs_group_name        = var.logs_group_name
    aws_region             = var.aws_region
    module_name            = var.module_name
  })

  tags = {
    Name        = "${var.app_name}-frontend-${var.module_name}-task-definition"
    Environment = var.app_environment
  }
}

resource "aws_ecs_service" "frontend_service" {
  name            = "${var.app_name}-${var.app_environment}-frontend-${var.module_name}-service"
  cluster         = var.cluster_id
  desired_count   = 1
  task_definition = aws_ecs_task_definition.frontend_task_definition.arn

  network_configuration {
    subnets = var.subnet_ids
    security_groups = [
      var.sg_id,
      var.lb_sg_id
    ]
    assign_public_ip = false
  }

  load_balancer {
    target_group_arn = var.lb_tg
    container_name   = "frontend-${var.module_name}"
    container_port   = 5173
  }

  tags = {
    Name        = "${var.app_name}-frontend-${var.module_name}-service"
    Environment = var.app_environment
  }
}
