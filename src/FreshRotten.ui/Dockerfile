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

# Publicar aplicacion en ngix
FROM nginx:alpine

COPY ./conf/nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=0 /app/dist/spa /usr/share/nginx/html

EXPOSE 80

CMD [ "nginx", "-g", "daemon off;" ]