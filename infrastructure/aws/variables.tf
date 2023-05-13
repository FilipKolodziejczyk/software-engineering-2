variable "aws_access_key" {
  type        = string
  sensitive   = true
  description = "AWS Access Key"
}

variable "aws_secret_key" {
  type        = string
  sensitive   = true
  description = "AWS Secret Key"
}

variable "aws_region" {
  type        = string
  description = "AWS Region"
}

variable "app_name" {
  type        = string
  description = "Application Name"
}

variable "app_environment" {
  type        = string
  description = "Application Environment"
}

variable "cidr" {
  description = "The CIDR block for the VPC."
  default     = "10.0.0.0/16"
}

variable "sa_password_kms_key_id" {
  type = string
  description = "SQL Server SA Password KMS Key ID"
}