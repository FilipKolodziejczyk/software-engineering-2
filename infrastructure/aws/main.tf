module "network" {
  source = "./modules/network"

  aws_region          = var.aws_region
  app_name            = var.app_name
  app_environment     = var.app_environment
  cidr                = var.cidr
  subnet_count        = var.subnet_count
  availability_zones  = var.availability_zones
}

module "ecs" {
  source = "./modules/ecs"

  app_name        = var.app_name
  app_environment = var.app_environment
}

module "sqlserver" {
  source = "./modules/sqlserver"

  sa_password_kms_key_id  = var.sa_password_kms_key_id
  app_name                = var.app_name
  app_environment         = var.app_environment
  vpc_id                  = module.network.vpc_id
  subnet_ids              = module.network.private_subnet_ids
}