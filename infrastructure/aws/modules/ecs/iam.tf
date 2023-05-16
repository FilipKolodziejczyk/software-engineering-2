data "aws_iam_policy_document" "ecs_agent" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ec2.amazonaws.com"]
    }
  }
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ecs-tasks.amazonaws.com"]
    }
  }
}

resource "aws_iam_role_policy" "s3_access" {
  name = "${var.app_name}-${var.app_environment}-s3-access"
  role = aws_iam_role.ecs_agent.name

  policy = jsonencode({
    "Version": "2012-10-17",
    "Statement": [
      {
        "Sid": "S3Find",
        "Effect": "Allow",
        "Action": [
          "s3:ListAllMyBuckets",
          "s3:GetBucketLocation",
        ],
        "Resource": [
          "arn:aws:s3:::*"
        ]
      },
      {
        "Sid": "S3List",
        "Effect": "Allow",
        "Action": [
          "s3:ListBucket",
          
        ],
        "Resource": [
          "arn:aws:s3:::${var.app_name}-${var.app_environment}-env-files"
        ]
      },
      {
        "Sid": "S3Access",
        "Effect": "Allow",
        "Action": [
          "s3:GetObject"
        ],
        "Resource": [
          "arn:aws:s3:::${var.app_name}-${var.app_environment}-env-files/*"
        ]
      }
    ]
  })
}

resource "aws_iam_role_policy" "get_secret" {
  name = "${var.app_name}-${var.app_environment}-get-secret"
  role = aws_iam_role.ecs_agent.name

  policy = jsonencode({
    "Version": "2012-10-17",
    "Statement": [
      {
        "Sid": "GetSecret",
        "Effect": "Allow",
        "Action": [
          "secretsmanager:GetSecretValue"
        ],
        "Resource": [
          "arn:aws:secretsmanager:${var.aws_region}:${var.account_id}:secret:*"
        ]
      },
      {
        "Sid": "GetKMSSecret",
        "Effect": "Allow",
        "Action": [
          "kms:*"
        ],
        "Resource": [
          "arn:aws:kms:${var.aws_region}:${var.account_id}:key/${var.sa_password_kms_key_id}"
        ]
      }
    ]
  })
}

resource "aws_iam_role_policy" "logging" {
  name = "${var.app_name}-${var.app_environment}-logging"
  role = aws_iam_role.ecs_agent.name

  policy = jsonencode({
    "Version": "2012-10-17",
    "Statement": [
      {
        "Sid": "CloudWatchLogs",
        "Effect": "Allow",
        "Action": [
          "logs:CreateLogStream",
          "logs:PutLogEvents"
        ],
        "Resource": [
          "arn:aws:logs:${var.aws_region}:${var.account_id}:log-group:/aws/ecs/*"
        ]
      }
    ]
  })
}
  
resource "aws_iam_role" "ecs_agent" {
  name               = "${var.app_name}-${var.app_environment}-ecs-agent"
  assume_role_policy = data.aws_iam_policy_document.ecs_agent.json

  tags = {
    Name        = "${var.app_name}-ecs-iam-role"
    Environment = var.app_environment
  }
}

resource "aws_iam_role_policy_attachment" "ecs_agent" {
  role = aws_iam_role.ecs_agent.name

  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonEC2ContainerServiceforEC2Role"
}

resource "aws_iam_instance_profile" "ecs_agent" {
  name = "${var.app_name}-${var.app_environment}-ecs-agent"
  role = aws_iam_role.ecs_agent.name

  tags = {
    Name        = "${var.app_name}-ecs-iam-instance-profile"
    Environment = var.app_environment
  }
}

output "ecs_agent_role_arn" {
  value = aws_iam_role.ecs_agent.arn
}
