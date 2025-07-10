#!/bin/bash



# Exit immediately if any command fails
set -e

# Create the directory if it doesn't exist
if [ ! -d /srv/psi8 ]; then
  sudo mkdir -p /srv/psi8
  echo "/srv/psi8 created."
else
  echo "/srv/psi8 already exists."
fi

# Copy docker-compose.yml and .env from /vagrant (shared folder) to /srv/psi8
sudo cp /vagrant/core/compose.yml /srv/psi8/
sudo cp /vagrant/core/.env /srv/psi8/
sudo cp -r /vagrant/core/rabbitmq /srv/psi8
sudo cp /vagrant/core/setup-cron.sh /srv/psi8/
sudo cp -r /vagrant/core/backup-scripts /srv/psi8

# Navigate to the target directory
cd /srv/psi8

# Run docker compose
sudo docker compose -f compose.yml up -d
echo "Docker Compose started."

# Install postgresql-client
sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo tee /etc/apt/trusted.gpg.d/postgresql.asc
sudo apt update
sudo apt install postgresql-client-17 -y
pg_dump --version

# Setup auto backup database
sudo chmod +x setup-cron.sh
./setup-cron.sh
echo "Setup auto backup database completed"