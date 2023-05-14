variable "app_name" {
  type        = string
  description = "Application Name"
}

variable "app_environment" {
  type        = string
  description = "Application Environment"
}

variable "vpc_id" {
  type        = string
  description = "VPC ID"
}

variable "subnet_id" {
  type        = string
  description = "Subnet ID"
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