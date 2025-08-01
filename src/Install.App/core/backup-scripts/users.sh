#!/bin/bash
export PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin"

# Get current date in YYYY-MM-DD format
date=$(date '+%Y-%m-%d')

# Database name
db='users'

# Backup file name
backup_file="/srv/psi8/postgresql/backups/${db}.bak"

# Run the pg_dump command with password and options
PGPASSWORD="Pass1234!" /usr/bin/pg_dump \
  --host 127.0.0.1 \
  --port 5432 \
  --username postgres \
  --format custom \
  --blobs \
  --verbose \
  --file "$backup_file" \
  "$db"
