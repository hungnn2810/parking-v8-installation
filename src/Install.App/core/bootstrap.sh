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

# Navigate to the target directory
cd /srv/psi8

# Run docker compose
sudo docker compose -f compose.yml up -d

echo "Docker Compose started."