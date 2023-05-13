resource "aws_internet_gateway" "default_igw" {
  vpc_id = aws_vpc.default_vpc.id
  
  tags = {
    Name        = "${var.app_name}-igw"
    Environment = var.app_environment
  }
}

resource "aws_subnet" "public" {
  vpc_id                  = aws_vpc.default_vpc.id
  count                   = var.subnet_count.public
  cidr_block              = cidrsubnet(var.cidr, 8, count.index)
  map_public_ip_on_launch = true

  tags  = {
    Name        = "${var.app_name}-public-subnet-${count.index}"
    Environment = var.app_environment
  }
}

resource "aws_subnet" "private" {
  vpc_id            = aws_vpc.default_vpc.id
  count             = var.subnet_count.private
  cidr_block        = cidrsubnet(var.cidr, 8, count.index + var.subnet_count.public)
  availability_zone = element(var.availability_zones, count.index % length(var.availability_zones))

  tags  = {
    Name        = "${var.app_name}-private-subnet-${count.index}"
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
  count          = var.subnet_count.public
  subnet_id      = aws_subnet.public[count.index].id
  route_table_id = aws_route_table.public.id
}

output "public_subnet_ids" {
  value = aws_subnet.public.*.id
}

output "private_subnet_ids" {
  value = aws_subnet.private.*.id
}