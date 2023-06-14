output "maintainer_access_key_id" {
  value = aws_iam_access_key.images_maintainer.id
}

output "maintainer_secret_access_key" {
  value = nonsensitive(aws_iam_access_key.images_maintainer.secret)
}