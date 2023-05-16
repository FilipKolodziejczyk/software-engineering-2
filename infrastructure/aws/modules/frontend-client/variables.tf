variable "app_name" {
  type        = string
  description = "Application Name"
}

variable "app_environment" {
  type        = string
  description = "Application Environment"
}

variable "subnet_ids" {
  type        = list(string)
  description = "Subnet IDs"
}

variable "cluster_id" {
  type        = string
  description = "Cluster ID"
}

variable "repository_url" {
  type        = string
  description = "Repository URL"
}

variable "ecs_agent_role_arn" {
  type        = string
  description = "ECS Agent Role ARN"
}

variable "lb_tg" {
  type        = string
  description = "Load Balancer Target Group ARN"
}

variable "sg_id" {
  type        = string
  description = "Security Group ID"
}

variable "default_image_tag" {
  type        = string
  description = "Default Image Tag"
}

variable "aws_region" {
  type        = string
  description = "AWS Region"
}

variable "logs_group_name" {
  type        = string
  description = "Logs Group Name"
}