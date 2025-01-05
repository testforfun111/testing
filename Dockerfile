# Dockerfile
FROM docker:latest

# Cài đặt pip
RUN apk add --no-cache py3-pip

# Cài đặt docker-compose
RUN /bin/sh -c pip install docker-compose