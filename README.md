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

### Proyecto Web

Para construir y ejecutar la API de Flask, sigue estos pasos:

1. Construir las imagenes de docker:
    ```bash
    docker compose up
    ```

2. Eliminar contenedores e images:
    ```bash
    docker-compose down --rmi all
    ```

## Visualizar el sitio
[Rotten Tomatoes IA](http://localhost:54813/#/) 

[API](http://localhost:8080/swagger/index.html)