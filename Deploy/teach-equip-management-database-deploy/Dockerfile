FROM mcr.microsoft.com/mssql/server:2022-latest

#RUN whoami

# Change active user to root
USER root 

# Create a config directory
RUN mkdir -p /usr/config
WORKDIR /usr/config

# Bundle config source
COPY . /usr/config

ENTRYPOINT ["./entrypoint.sh"]