resource "aws_vpc_endpoint" "s3_endpoint" {
    vpc_id = aws_vpc.default_vpc.id
    service_name = "com.amazonaws.${var.aws_region}.s3"
}

resource "aws_vpc_endpoint_route_table_association" "s3_endpoint_route_table_association" {
    route_table_id = aws_route_table.public.id
    vpc_endpoint_id = aws_vpc_endpoint.s3_endpoint.id
}