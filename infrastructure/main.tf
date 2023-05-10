terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "2.70.0"
    }
  }

  backend "s3" {
    bucket = "terraform-state-bucket"
    key    = "state/terraform_state.tfstate"
    region = "eu-north-1"
  }
}

provider "aws" {}

module "network" {
  source = "./modules/network"

  app_name        = var.app_name
  app_environment = var.app_environment
  cidr            = var.cidr
}