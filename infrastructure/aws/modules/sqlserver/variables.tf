variable "sa_password" {
  type = string
  sensitive = true
  description = "SQL Server SA Password"
}

variable "app_name" {
  type        = string
  description = "Application Name"
}

variable "app_environment" {
  type        = string
  description = "Application Environment"
}

variable "vpc_subnet_id" {
  type        = string
  description = "VPC Subnet ID"
}

variable "vpc_id" {
  type        = string
  description = "VPC ID"
}