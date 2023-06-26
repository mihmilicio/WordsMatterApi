Rodar Brokers de messageria

-
docker pull rabbitmq:3-management
docker run --rm -it -p 8080:15672 -p 5672:5672 rabbitmq:3-management

docker pull nats
docker run -d --name nats-main -p 4222:4222 -p 6222:6222 -p 8222:8222 nats



JAva necessário instalado 
https://download.oracle.com/java/20/latest/jdk-20_windows-x64_bin.msi

Maven instalado  e no path corretamente
https://maven.apache.org/download.cgi
