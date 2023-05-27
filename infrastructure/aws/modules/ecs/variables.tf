variable "app_name" {
  type        = string
  description = "Application Name"
}

variable "app_environment" {
  type        = string
  description = "Application Environment"
}

variable "app_domain_name" {
  type        = string
  description = "Application Domain Name"
}

variable "acm_ssl_cert_arn" {
  type        = string
  description = "ACM SSL Certificate ARN"
}

variable "dns_zone_id" {
  type        = string
  description = "DNS Zone ID"
}

variable "subnet_ids" {
  type        = list(string)
  description = "Subnet IDs"
}

variable "vpc_id" {
  type        = string
  description = "VPC ID"
}

variable "lb_sg_id" {
  type        = string
  description = "Load Balancer Security Group ID"
}

variable "aws_region" {
  type        = string
  description = "AWS Region"
}

variable "account_id" {
  type        = string
  description = "AWS Account ID"
}

variable "sa_password_kms_key_id" {
  type        = string
  description = "SQL Server SA Password KMS Key ID"
}