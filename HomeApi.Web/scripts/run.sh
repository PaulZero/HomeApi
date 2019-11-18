#!/bin/bash

# Change to project directory (this must be updated if you move this script)
cd ..

# Pull the latest changes
git pull

# Build and run the application
dotnet build
dotnet run
