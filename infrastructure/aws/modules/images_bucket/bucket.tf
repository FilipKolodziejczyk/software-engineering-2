resource "aws_s3_bucket" "images_bucket" {
  bucket = "${var.app_name}-${var.app_environment}-images-bucket"

  tags = {
    Name        = "${var.app_name}-images-bucket"
    Environment = var.app_environment
  }
}

resource "aws_s3_bucket_ownership_controls" "images_bucket_ownership_controls" {
  bucket = aws_s3_bucket.images_bucket.id

  rule {
    object_ownership = "BucketOwnerPreferred"
  }
}

resource "aws_s3_bucket_public_access_block" "images_bucket_public_access_block" {
  bucket = aws_s3_bucket.images_bucket.id

  block_public_acls       = false
  block_public_policy     = false
  ignore_public_acls      = false
  restrict_public_buckets = false
}

resource "aws_s3_bucket_acl" "images_bucket_acl" {
  bucket = aws_s3_bucket.images_bucket.id
  acl = "public-read"

  depends_on = [ 
    aws_s3_bucket_public_access_block.images_bucket_public_access_block,
    aws_s3_bucket_ownership_controls.images_bucket_ownership_controls,
   ]
}