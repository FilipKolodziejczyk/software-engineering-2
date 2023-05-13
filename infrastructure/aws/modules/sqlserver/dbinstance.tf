resource "aws_db_subnet_group" "sqlserver_db_subnet_group" {
    subnet_ids  = [var.vpc_subnet_id]
}

resource "aws_db_instance" "sqlserver" {
    allocated_storage       = 20
    engine                  = "sqlserver-ex"
    engine_version          = "15.00.4236.7.v1"
    instance_class          = "db.t3.micro"

    db_name                 = "${var.app_name}-${var.app_environment}-sqlserver"
    username                = "sa"
    password                = var.sa_password

    db_subnet_group_name    = aws_db_subnet_group.sqlserver_db_subnet_group.name
    vpc_security_group_ids  = [aws_security_group.sqlserver_sg.id]
    skip_final_snapshot     = true
    publicly_accessible     = true

    tags = {
        Name        = "${var.app_name}-sqlserver"
        Environment = var.app_environment
    }
}