# **Back URL Short**

Este proyecto es un sistema de acortamiento de URLs desarrollado con .NET Core. Utiliza una base de datos PostgreSQL alojada en **Neon** para almacenar las URLs originales y sus códigos cortos generados automáticamente.

---

## **Características**

- 🎯 **Acortar URLs**: Registra URLs originales y genera un enlace corto único.
- 🚀 **Redirección de URLs**: Redirige al usuario a la URL original utilizando el código corto.
- 📦 **Base de datos confiable**: PostgreSQL como sistema de almacenamiento, alojado en Neon.

---

## **Endpoints disponibles**

### **1. Registrar una URL corta (POST)**

Utiliza el siguiente endpoint para acortar una URL:

**URL:**  
`POST https://api.davidgomezmartin.tech/api/url/shorten`

**Ejemplo de comando `curl`:**
```bash
curl -X POST https://api.davidgomezmartin.tech/api/url/shorten \
     -H "Content-Type: application/json" \
     -d '{"originalUrl": "https://www.youtube.com"}'
```

- **Request body (JSON):**
  ```json
  {
    "originalUrl": "https://www.youtube.com"
  }
  ```
  Aquí, reemplaza `"https://www.youtube.com"` por la URL que deseas acortar.

- **Respuesta exitosa (200 OK):**
  ```json
  {
    "shortUrl": "https://api.davidgomezmartin.tech/u/abc123"
  }
  ```
  El `shortUrl` es el enlace generado que actúa como alias para la URL original.

---

### **2. Redirigir a la URL original (GET)**

Utiliza un código corto para redirigir al usuario a la URL original:

**Formato del endpoint:**  
`GET https://api.davidgomezmartin.tech/api/url/u/{shortCode}`

- Sustituye `{shortCode}` por el código generado al registrar la URL.

**Ejemplo de comando `curl`:**
```bash
curl -X GET https://api.davidgomezmartin.tech/api/url/u/abc123
```

#### Respuestas posibles:

| Código de estado | Significado                          | Respuesta (en caso de error)             |
|-------------------|--------------------------------------|------------------------------------------|
| `302 Found`       | Redirección exitosa a la URL original. | N/A                                      |
| `404 Not Found`   | El código corto no existe.           | `{ "error": "La URL acortada no existe." }` |

---

## **Docker**

También puedes correr todo el proyecto con **Docker**, lo cual facilita el despliegue y ejecución del servicio sin necesidad de instalar dependencias de manera manual.

### **Dependencias necesarias:**
- Docker
- Docker Compose

---

### **Cómo ejecutar en Docker**

1. Asegúrate de que tengas instalado [Docker](https://www.docker.com/) y [Docker Compose](https://docs.docker.com/compose/).

2. Construye la imagen del proyecto:
   ```bash
   docker build -t back-url-short .
   ```

3. Ejecuta el contenedor con el siguiente comando:
   ```bash
   docker run -d -p 5000:5000 --name back-url-short back-url-short
   ```

4. Una vez que el contenedor esté corriendo, el servidor estará disponible en `http://localhost:5000`.

---

### **Ejemplo completo con la API en Docker**

#### Registrar una URL:

```bash
curl -X POST http://localhost:5000/api/url/shorten \
     -H "Content-Type: application/json" \
     -d '{"originalUrl": "https://www.youtube.com"}'
```

#### Redirigir usando un código acortado:

```bash
curl -X GET http://localhost:5000/api/url/u/aaa
```

---

## **Base de datos**

El proyecto utiliza una base de datos PostgreSQL para gestionar las URLs originales y los códigos cortos. También puedes configurar y ejecutar PostgreSQL como un contenedor de Docker junto con el proyecto.

### **Ejemplo de configuración en `docker-compose.yml`**

```yaml
version: "3.8"

services:
  app:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:5000"
    depends_on:
      - db

  db:
    image: postgres:latest
    container_name: postgres_urlshortener
    environment:
      POSTGRES_USER: admin
      POSTGRES_PASSWORD: password
      POSTGRES_DB: urlshortener
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

Con este archivo:

1. Ejecuta el comando:
   ```bash
   docker-compose up -d
   ```

2. Ahora, tanto el backend como la base de datos estarán ejecutándose en contenedores de Docker.

---

## **Estructura del Proyecto**

El código fuente está organizado de la siguiente manera:

- **Controladores:**
  Manejan las solicitudes HTTP, como el registro de URLs y las redirecciones.
    - `UrlController`:
        - `POST /api/url/shorten`
        - `GET /api/url/u/{shortCode}`

- **Modelos:**
  Representan las entidades necesarias para la aplicación:
    - `UrlRequest`: Modelo para recibir la URL proporcionada por el usuario.
    - `Url`: Modelo para almacenar las URLs y códigos en la base de datos.

- **Base de datos:**
  Gestión de almacenamiento persistente utilizando **Entity Framework Core** para interactuar con PostgreSQL.

---

## **Pruebas**

Puedes realizar pruebas de los endpoints utilizando herramientas como **Postman** o ejecutando los comandos `curl` proporcionados anteriormente.

---

## **Tecnologías utilizadas**

- **Framework**: ASP.NET Core
- **Lenguaje**: C#
- **Base de datos**: PostgreSQL (Neon)
- **ORM**: Entity Framework Core

---

### **Contribuir**

Si deseas contribuir a este proyecto, ¡no dudes en abrir un PR o enviar issues! 🙂