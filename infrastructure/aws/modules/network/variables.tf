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
  type        = string
  description = "The CIDR block for the VPC."
}

variable "subnet_count" {
  description = "The number of subnets to create."
  type        = map(number)
}

variable "availability_zones" {
  description = "The availability zones to use."
  type        = list(string)
}