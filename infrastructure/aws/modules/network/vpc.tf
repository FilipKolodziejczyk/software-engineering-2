resource "aws_vpc" "default_vpc" {
  cidr_block                       = var.cidr
  enable_dns_hostnames             = true
  enable_dns_support               = true
  assign_generated_ipv6_cidr_block = true

  tags = {
    Name        = "${var.app_name}-vpc"
    Environment = var.app_environment
  }
}

output "vpc_id" {
  value = aws_vpc.default_vpc.id
}