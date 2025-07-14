#!/bin/bash

SCRIPT_DIR="/srv/psi8/backup-scripts"
LOG_DIR="$SCRIPT_DIR/logs"
CRONTAB="0 * * * *"

# Create logs directory if not exists
mkdir -p "$LOG_DIR"

# Get all .sh files in the directory
shopt -s nullglob
scripts=("$SCRIPT_DIR"/*.sh)

for script in "${scripts[@]}"; do
  chmod +x "$script"

  # Get script name without path and extension
  script_name=$(basename "$script" .sh)

  # Set log path for this script
  log_file="$LOG_DIR/${script_name}.log"

  # Define cron entry
  cron_entry="$CRONTAB $script >> $log_file 2>&1"

  # Check if this specific script is already scheduled
  crontab -l 2>/dev/null | grep -F "$script" >/dev/null
  if [ $? -ne 0 ]; then
    (crontab -l 2>/dev/null; echo "$cron_entry") | crontab -
    echo "Cron job added for $script"
  else
    echo "Cron job already exists for $script"
  fi
done
