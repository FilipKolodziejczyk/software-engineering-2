resource "aws_cloudwatch_log_group" "log_group" {
  name = "${var.app_name}-${var.app_environment}-log-group"

  tags = {
    Name        = "${var.app_name}-log-group"
    Environment = var.app_environment
  }
}

module "network" {
  source = "./modules/network"

  aws_region         = var.aws_region
  app_name           = var.app_name
  app_environment    = var.app_environment
  app_domain_name    = var.app_domain_name
  cidr               = var.cidr
  subnet_count       = var.subnet_count
  availability_zones = var.availability_zones
}

module "ecs" {
  source = "./modules/ecs"

  app_name               = var.app_name
  app_environment        = var.app_environment
  app_domain_name        = var.app_domain_name
  subnet_ids             = module.network.public_subnet_ids
  vpc_id                 = module.network.vpc_id
  lb_sg_id               = module.network.loadbalancer_sg_id
  account_id             = var.account_id
  aws_region             = var.aws_region
  sa_password_kms_key_id = var.sa_password_kms_key_id
  acm_ssl_cert_arn       = module.network.ssl_cert_arn
  dns_zone_id            = module.network.dns_zone_id
}

module "sqlserver" {
  source = "./modules/sqlserver"

  sa_password_kms_key_id = var.sa_password_kms_key_id
  app_name               = var.app_name
  app_environment        = var.app_environment
  subnet_ids             = module.network.private_subnet_ids
  sg_id                  = module.network.sqlserver_sg_id
}

module "backend" {
  source = "./modules/backend"

  app_name               = var.app_name
  app_environment        = var.app_environment
  subnet_ids             = module.network.public_subnet_ids
  cluster_id             = module.ecs.cluster_id
  repository_url         = module.ecs.backend_repository_url
  ecs_agent_role_arn     = module.ecs.ecs_agent_role_arn
  sqlserver_endpoint     = module.sqlserver.sqlserver_endpoint
  sa_password_kms_key_id = var.sa_password_kms_key_id
  db_password_secret_arn = module.sqlserver.secret_arn
  lb_tg                  = module.ecs.backend_lb_tg
  sg_id                  = module.network.backend_sg_id
  lb_sg_id               = module.network.loadbalancer_sg_id
  default_image_tag      = var.backend_default_image_tag
  aws_region             = var.aws_region
  logs_group_name        = aws_cloudwatch_log_group.log_group.name
}

module "frontend_client" {
  source = "./modules/frontend"
  
  app_name               = var.app_name
  app_environment        = var.app_environment
  subnet_ids             = module.network.public_subnet_ids
  cluster_id             = module.ecs.cluster_id
  repository_url         = module.ecs.frontend_client_repository_url
  ecs_agent_role_arn     = module.ecs.ecs_agent_role_arn
  lb_tg                  = module.ecs.frontend_client_lb_tg
  sg_id                  = module.network.frontend_sg_id
  lb_sg_id               = module.network.loadbalancer_sg_id
  default_image_tag      = var.frontend_client_default_image_tag
  aws_region             = var.aws_region
  logs_group_name        = aws_cloudwatch_log_group.log_group.name
  module_name            = "client"
}

module "frontend_delivery" {
  source = "./modules/frontend"
  
  app_name               = var.app_name
  app_environment        = var.app_environment
  subnet_ids             = module.network.public_subnet_ids
  cluster_id             = module.ecs.cluster_id
  repository_url         = module.ecs.frontend_delivery_repository_url
  ecs_agent_role_arn     = module.ecs.ecs_agent_role_arn
  lb_tg                  = module.ecs.frontend_delivery_lb_tg
  sg_id                  = module.network.frontend_sg_id
  lb_sg_id               = module.network.loadbalancer_sg_id
  default_image_tag      = var.frontend_delivery_default_image_tag
  aws_region             = var.aws_region
  logs_group_name        = aws_cloudwatch_log_group.log_group.name
  module_name            = "delivery"
}

module "frontend_shop" {
  source = "./modules/frontend"
  
  app_name               = var.app_name
  app_environment        = var.app_environment
  subnet_ids             = module.network.public_subnet_ids
  cluster_id             = module.ecs.cluster_id
  repository_url         = module.ecs.frontend_shop_repository_url
  ecs_agent_role_arn     = module.ecs.ecs_agent_role_arn
  lb_tg                  = module.ecs.frontend_shop_lb_tg
  sg_id                  = module.network.frontend_sg_id
  lb_sg_id               = module.network.loadbalancer_sg_id
  default_image_tag      = var.frontend_shop_default_image_tag
  aws_region             = var.aws_region
  logs_group_name        = aws_cloudwatch_log_group.log_group.name
  module_name            = "shop"
}

module "images_bucket" {
  source = "./modules/images_bucket"

  app_name          = var.app_name
  app_environment   = var.app_environment
}