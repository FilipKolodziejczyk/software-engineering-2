resource "aws_ecs_cluster" "ecs_cluster" {
  name  = "${var.app_name}-${var.app_environment}-ecs-cluster"

  tags = {
      Name        = "${var.app_name}-ecs-cluster"
      Environment = var.app_environment
  }
}

data "aws_ami" "ecs_ami" {
  most_recent = true
  owners      = ["amazon"]

  filter {
    name   = "name"
    values = ["amzn2-ami-ecs-hvm-*-x86_64-ebs"]
  }
}

resource "aws_launch_configuration" "ec2_config" {
  name_prefix             = "${var.app_name}-${var.app_environment}-ec2-"
  image_id                = data.aws_ami.ecs_ami.id
  instance_type           = "t2.micro"
  user_data              = "#!/bin/bash\necho ECS_CLUSTER=${aws_ecs_cluster.ecs_cluster.name} >> /etc/ecs/ecs.config"
  iam_instance_profile    = aws_iam_instance_profile.ecs_agent.name
}

resource "aws_autoscaling_group" "ec2_autoscaling_group" {
  name                    = "${var.app_name}-${var.app_environment}-ec2-autoscaling-group"
  launch_configuration   = aws_launch_configuration.ec2_config.name
  vpc_zone_identifier    = var.subnet_ids

  min_size                = 1
  max_size                = 5
  desired_capacity        = 3
  health_check_grace_period = 300
  health_check_type       = "EC2"
  force_delete            = true
  termination_policies    = ["OldestInstance"]
  tag {
      key                 = "Name"
      value               = "${var.app_name}-ec2-autoscaling-group"
      propagate_at_launch = true
  }
  tag {
      key                 = "Environment"
      value               = var.app_environment
      propagate_at_launch = true
  }
}

output "cluster_id" {
  value = aws_ecs_cluster.ecs_cluster.id
}