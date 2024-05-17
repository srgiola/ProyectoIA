# Utiliza la imagen oficial de Node 20 como base
FROM node:20

# Establece el directorio de trabajo en el contenedor
WORKDIR /app

# Copia el archivo package.json y package-lock.json (si existe) al directorio de trabajo
COPY package*.json ./

# Instala las dependencias del proyecto
RUN npm install

# Copia el resto de los archivos del proyecto al directorio de trabajo en el contenedor
COPY . .

# Expone el puerto 3000 para acceder a la aplicación React
EXPOSE 3000

# Instala MySQL Server
USER root
RUN apt-get update && apt-get install -y mysql-server

# Inicia el servicio de MySQL (ajusta según necesidades)
RUN service mysql start
