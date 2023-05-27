resource "aws_acm_certificate" "ssl_cert" {
  domain_name = var.app_domain_name
  validation_method = "EMAIL"

  tags = {
    Name        = "${var.app_name}-ssl-cert"
    Environment = var.app_environment
  }

  lifecycle {
    prevent_destroy = true
  }
}

resource "aws_route53_zone" "zone" {
  name = var.app_domain_name

  tags = {
    Name        = "${var.app_name}-dns-zone"
    Environment = var.app_environment
  }
}

output "ssl_cert_arn" {
  value = aws_acm_certificate.ssl_cert.arn
}

output "dns_zone_id" {
  value = aws_route53_zone.zone.zone_id
}