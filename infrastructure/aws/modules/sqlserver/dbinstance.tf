resource "aws_db_subnet_group" "sqlserver-subnet-group" {
    name       = "${var.app_name}-sqlserver-subnet-group"
    subnet_ids = var.subnet_ids

    tags = {
        Name        = "${var.app_name}-sqlserver-subnet-group"
        Environment = var.app_environment
    }
}

resource "aws_db_instance" "sqlserver" {
    allocated_storage       = 20
    engine                  = "sqlserver-ex"
    engine_version          = "14.00.3451.2.v1"
    instance_class          = "db.t2.micro"

    username                      = "sa"
    manage_master_user_password   = true
    master_user_secret_kms_key_id = var.sa_password_kms_key_id

    db_subnet_group_name    = aws_db_subnet_group.sqlserver-subnet-group.name
    vpc_security_group_ids  = [aws_security_group.sqlserver_sg.id]
    skip_final_snapshot     = true
    publicly_accessible     = true

    tags = {
        Name        = "${var.app_name}-sqlserver"
        Environment = var.app_environment
    }
}