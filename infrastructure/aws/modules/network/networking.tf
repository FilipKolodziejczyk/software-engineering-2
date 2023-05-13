resource "aws_internet_gateway" "default_igw" {
  vpc_id = aws_vpc.default_vpc.id
  
  tags = {
    Name        = "${var.app_name}-igw"
    Environment = var.app_environment
  }
}

resource "aws_subnet" "public" {
  vpc_id      = aws_vpc.default_vpc.id
  cidr_block  = var.cidr

  tags  = {
    Name        = "${var.app_name}-public-subnet"
    Environment = var.app_environment
  }
}

resource "aws_route_table" "public" {
  vpc_id = aws_vpc.default_vpc.id

  tags = {
    Name        = "${var.app_name}-routing-table-public"
    Environment = var.app_environment
  }
}

resource "aws_route" "public" {
  route_table_id         = aws_route_table.public.id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id             = aws_internet_gateway.default_igw.id
}

resource "aws_route_table_association" "public" {
  subnet_id      = aws_subnet.public.id
  route_table_id = aws_route_table.public.id
}