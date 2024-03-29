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

variable "app_domain_name" {
  type        = string
  description = "Application Domain Name"
}

variable "cidr" {
  description = "The CIDR block for the VPC."
  default     = "10.0.0.0/16"
}

variable "subnet_count" {
  description = "The number of subnets to create."
  type        = map(number)
  default     = {
    public  = 2
    private = 2
  }
}

variable "availability_zones" {
  description = "The availability zones to use."
  type        = list(string)
}

variable "sa_password_kms_key_id" {
  type = string
  description = "SQL Server SA Password KMS Key ID"
}

variable "account_id" {
  type = string
  description = "AWS Account ID"
}

variable "backend_default_image_tag" {
  type = string
  description = "Backend Default Image Tag"
}

variable "frontend_client_default_image_tag" {
  type = string
  description = "Frontend Client Default Image Tag"
}

variable "frontend_delivery_default_image_tag" {
  type = string
  description = "Frontend Delivery Default Image Tag"
}

variable "frontend_shop_default_image_tag" {
  type = string
  description = "Frontend Shop Default Image Tag"
}