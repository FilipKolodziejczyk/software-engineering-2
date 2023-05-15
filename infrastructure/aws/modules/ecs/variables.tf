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

variable "vpc_id" {
  type        = string
  description = "VPC ID"
}

variable "backend_sg_id" {
  type        = string
  description = "Backend Security Group ID"
}