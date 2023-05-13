module "network" {
  source = "./modules/network"

  aws_region      = var.aws_region
  app_name        = var.app_name
  app_environment = var.app_environment
  cidr            = var.cidr
}