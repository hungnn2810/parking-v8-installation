#!/bin/bash

# Exit immediately if any command fails
set -e

# === Check and Install Docker ===
if ! command -v docker &> /dev/null; then
  echo "Docker not found. Installing Docker..."
  
  sudo apt-get update
  sudo apt-get install -y apt-transport-https ca-certificates curl software-properties-common gnupg lsb-release

  curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
  sudo add-apt-repository \
    "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
    $(lsb_release -cs) \
    stable"

  sudo apt-get update
  sudo apt-get install -y docker-ce

  # Set Docker log limits globally
  sudo tee /etc/docker/daemon.json > /dev/null <<EOF
{
  "log-driver": "json-file",
  "log-opts": {
    "max-size": "10m",
    "max-file": "3"
  }
}
EOF

  sudo systemctl daemon-reload
  sudo systemctl enable docker
  sudo systemctl restart docker

  echo "✅ Docker installed successfully."
else
  echo "✅ Docker is already installed: $(docker --version)"
fi

# === Check and Install Docker Compose v2 ===
if ! docker compose version &> /dev/null; then
  echo "Docker Compose v2 not found. Installing..."
  DOCKER_CONFIG=${DOCKER_CONFIG:-$HOME/.docker}
  mkdir -p $DOCKER_CONFIG/cli-plugins
  curl -SL https://github.com/docker/compose/releases/download/v2.24.7/docker-compose-linux-x86_64 -o $DOCKER_CONFIG/cli-plugins/docker-compose
  chmod +x $DOCKER_CONFIG/cli-plugins/docker-compose
  echo "✅ Docker Compose installed successfully."
else
  echo "✅ Docker Compose is already installed: $(docker compose version)"
fi

# Create the directory if it doesn't exist
if [ ! -d /srv/psi8 ]; then
  sudo mkdir -p /srv/psi8
  echo "/srv/psi8 created."
else
  echo "/srv/psi8 already exists."
fi

# Copy compose.local.yml and .env from /vagrant (shared folder) to /srv/psi8
sudo cp /vagrant/core/compose.local.yml /srv/psi8/
sudo cp /vagrant/core/.env /srv/psi8/
sudo cp -r /vagrant/core/rabbitmq /srv/psi8
sudo cp /vagrant/core/setup-cron.sh /srv/psi8/
sudo cp -r /vagrant/core/backup-scripts /srv/psi8

# Navigate to the target directory
cd /srv/psi8

# Run docker compose
sudo docker compose -f compose.local.yml up -d
echo "Docker Compose started."

# Check if postgresql-client-17 is already installed
if pg_dump --version 2>/dev/null | grep -q "17."; then
    echo "✅ PostgreSQL Client 17 is already installed: $(pg_dump --version)"
else
    echo "❌ PostgreSQL Client 17 not found. Installing..."

    # Add PostgreSQL APT repository if not already present
    if [ ! -f /etc/apt/sources.list.d/pgdg.list ]; then
        echo "➕ Adding PostgreSQL APT repository..."
        echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" | sudo tee /etc/apt/sources.list.d/pgdg.list
        wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo tee /etc/apt/trusted.gpg.d/postgresql.asc >/dev/null
        sudo apt update
    else
        echo "ℹ️ PostgreSQL APT repository already present. Skipping..."
    fi

    # Install PostgreSQL client 17
    sudo apt install postgresql-client-17 -y
fi

# Verify installation
pg_dump --version

# Setup auto backup database
sudo chmod +x setup-cron.sh
./setup-cron.sh
echo "✅ Setup auto backup database completed"