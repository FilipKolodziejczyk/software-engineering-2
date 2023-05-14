variable "sa_password_kms_key_id" {
  type = string
  description = "SQL Server SA Password KMS Key ID"
}

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

variable "subnet_ids" {
  type        = list(string)
  description = "Subnet IDs"
}

variable "backend_sg_id" {
  type        = string
  description = "Backend Security Group ID"
}