resource "aws_iam_user" "images_maintainer" {
  name = "${var.app_name}-${var.app_environment}-images-maintainer"
  path = "/"

  tags = {
    Name = "images_maintainer"
  }
}

resource "aws_iam_access_key" "images_maintainer" {
  user = aws_iam_user.images_maintainer.name
}

data "aws_iam_policy_document" "images_maintainer" {
  statement {
    principals {
      type        = "AWS"
      identifiers = [aws_iam_user.images_maintainer.arn]
    }

    actions = [
      "s3:ListBucket",
      "s3:GetObject",
      "s3:PutObject",
      "s3:DeleteObject",
    ]

    resources = [
      "${aws_s3_bucket.images_bucket.arn}",
      "${aws_s3_bucket.images_bucket.arn}/*",
    ]
  }
}

resource "aws_s3_bucket_policy" "images_maintainer" {
  bucket = aws_s3_bucket.images_bucket.id
  policy = data.aws_iam_policy_document.images_maintainer.json
}