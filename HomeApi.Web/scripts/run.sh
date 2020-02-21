#!/bin/bash

nohup dotnet /var/www/homeapi/HomeApi.Web.dll &

sudo service nginx restart
