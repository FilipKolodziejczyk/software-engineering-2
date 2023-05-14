data "aws_iam_policy_document" "ecs_agent" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ec2.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "ecs_agent" {
  name               = "${var.app_name}-${var.app_environment}-ecs-agent"
  assume_role_policy = data.aws_iam_policy_document.ecs_agent.json

  tags = {
    Name        = "${var.app_name}-ecs-iam-role"
    Environment = var.app_environment
  }
}

resource "aws_iam_role_policy_attachment" "ecs_agent" {
  role       = aws_iam_role.ecs_agent.name
  
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonEC2ContainerServiceforEC2Role"
}

resource "aws_iam_instance_profile" "ecs_agent" {
  name = "${var.app_name}-${var.app_environment}-ecs-agent"
  role = aws_iam_role.ecs_agent.name

  tags = {
    Name        = "${var.app_name}-ecs-iam-instance-profile"
    Environment = var.app_environment
  }
}

output "ecs_agent_role_arn" {
  value = aws_iam_role.ecs_agent.arn
}