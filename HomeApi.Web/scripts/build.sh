#!/bin/bash

# Stop the service if it's running
sudo service kestrel-homeapi stop

# Change to project directory (this must be updated if you move this script)
cd $HOME/repos/HomeApi/HomeApi.Web

# Pull the latest changes
git pull

# Build and run the application
dotnet clean
dotnet publish -c Release -r linux-arm64 --self-contained true -o /var/www/homeapi

if [ -d "/var/www/homeapi/scripts" ]; then
    rm -rf /var/www/homeapi/scripts
fi

mkdir /var/www/homeapi/scripts
cp scripts/run.sh /var/www/homeapi/scripts/run.sh

sudo service kestrel-homeapi start
