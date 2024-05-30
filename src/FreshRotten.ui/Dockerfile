# Obtener proyecto
FROM node:20.13-alpine

  WORKDIR /app

  # Copiar el proyecto
  # e instalar dependencias
  COPY fresh-rotten/ .
  RUN npm i -g @quasar/cli
  RUN npm install

  # generar compilado de la aplicacion
  RUN quasar build

# Publicar aplicacion en apache
FROM ubuntu:20.04
  
  # Instalar paquetes
  RUN apt-get update
  RUN apt-get install wget -y
  RUN apt-get install --reinstall systemd -y
  RUN apt-get install apache2 -y

  # Modulos Requeridos
  RUN a2enmod rewrite
  RUN a2enmod proxy
  RUN a2enmod proxy_http
  RUN a2enmod headers
  RUN a2enmod ssl

  # Restar Server
  RUN service apache2 restart

  # Copia en la carpeta apache
  COPY --from=0 /app/dist/spa /var/www/html/

EXPOSE 54813:80
CMD apachectl -DFOREGROUND