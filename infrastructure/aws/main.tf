module "network" {
  source = "./modules/network"

  aws_region      = var.aws_region
  app_name        = var.app_name
  app_environment = var.app_environment
  cidr            = var.cidr
}

module "ecs" {
  source = "./modules/ecs"

  app_name        = var.app_name
  app_environment = var.app_environment
}

module "sqlserver" {
  source = "./modules/sqlserver"

  sa_password     = var.sa_password
  app_name        = var.app_name
  app_environment = var.app_environment
  vpc_id          = module.network.vpc_id
}