resource "aws_db_instance" "sqlserver" {
    allocated_storage       = 20
    engine                  = "sqlserver-ex"
    engine_version          = "15.00.4236.7.v1"
    instance_class          = "db.t3.micro"

    db_name                 = "${var.app_name}-${var.app_environment}-sqlserver"
    username                = "sa"
    password                = var.sa_password

    vpc_security_group_ids  = [aws_security_group.sqlserver_sg.id]
    skip_final_snapshot     = true
    publicly_accessible     = true

    tags = {
        Name        = "${var.app_name}-sqlserver"
        Environment = var.app_environment
    }
}