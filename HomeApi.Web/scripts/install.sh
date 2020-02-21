#!/bin/bash

if [ -f "/etc/systemd/system/kestrel-homeapi.service" ]; then
    sudo service kestrel-homeapi stop
    sudo systemctl disable kestrel-homeapi
    sudo rm "/etc/systemd/system/kestrel-homeapi.service"
fi

cd /home/ubuntu/repos/HomeApi/HomeApi.Web/scripts

sudo cp "./install-resources/kestrel-homeapi.service" "/etc/systemd/system/kestrel-homeapi.service"
sudo systemctl enable kestrel-homeapi
sudo service kestrel-homeapi start
