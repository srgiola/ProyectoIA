# Proyecto Inteligencia Artificial 2024

Este proyecto tiene como objetivo desarrollar un motor de clasificación basado en inteligencia artificial usando "Naive Bayes".

## Requisitos

- Docker Desktop para Windows

## Instalación

Asegúrate de tener Docker Desktop instalado y en ejecución en tu máquina. Puedes descargar Docker Desktop desde [aquí](https://www.docker.com/products/docker-desktop).

### Motor de Clasificación

Para construir y ejecutar el motor de clasificación, sigue estos pasos:

1. Construir la imagen de Docker para el motor de clasificación:
    ```bash
    docker build -t motor -f Dockerfile.clasificador .
    ```

    Este comando crea una imagen de Docker llamada `motor` utilizando el archivo `Dockerfile.clasificador` en el directorio actual.

2. Ejecutar el contenedor del motor de clasificación:
    ```bash
    docker run -it -v ${PWD}/clasificador:/app motor /bin/sh
    ```

    Este comando ejecuta un contenedor interactivo basado en la imagen `motor`, monta el directorio local `clasificador` en `/app` dentro del contenedor, y abre una sesión de shell en el contenedor.

### API con Flask

Para construir y ejecutar la API de Flask, sigue estos pasos:

1. Construir la imagen de Docker para la API de Flask:
    ```bash
    docker build -t backend -f Dockerfile.backend .
    ```

    Este comando crea una imagen de Docker llamada `backend` utilizando el archivo `Dockerfile.backend` en el directorio actual.

2. Ejecutar el contenedor de la API en modo interactivo:
    ```bash
    docker run -it -p 5000:5000 -v ${PWD}/api:/app backend /bin/sh
    ```

    Este comando ejecuta un contenedor interactivo basado en la imagen `backend`, monta el directorio local `api` en `/app` dentro del contenedor, mapea el puerto 5000 del contenedor al puerto 5000 de la máquina host, y abre una sesión de shell en el contenedor.

3. Ejecutar el contenedor de la API en modo desatendido:
    ```bash
    docker run -d -p 5000:5000 backend
    ```

    Este comando ejecuta un contenedor basado en la imagen `backend` en segundo plano (modo desatendido), mapeando el puerto 5000 del contenedor al puerto 5000 de la máquina host.

### Base de Datos MySQL

Para construir y ejecutar el contenedor de MySQL, sigue estos pasos:

1. Construir la imagen de Docker para MySQL:
    ```bash
    docker build -t mysql -f Dockerfile.mysql .
    ```

    Este comando crea una imagen de Docker llamada `mysql` utilizando el archivo `Dockerfile.mysql` en el directorio actual.

2. Ejecutar el contenedor de MySQL en modo desatendido:
    ```bash
    docker run -d -p 3306:3306 --name mysql-container mysql
    ```

    Este comando ejecuta un contenedor basado en la imagen `mysql` en segundo plano (modo desatendido), mapeando el puerto 3306 del contenedor al puerto 3306 de la máquina host y nombrando el contenedor como `mysql-container`.

3. Acceder al contenedor de MySQL:
    ```bash
    docker exec -it mysql-container mysql -u root -p
    ```

    Este comando abre una sesión interactiva de MySQL en el contenedor `mysql-container`, permitiendo el acceso a la base de datos como el usuario root.

Con estos pasos, tendrás la configuración completa para ejecutar el motor de clasificación, la API de Flask y la base de datos MySQL usando contenedores Docker.
