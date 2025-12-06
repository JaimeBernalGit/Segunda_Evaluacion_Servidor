Configuración de Base de Datos Local (SQL Server)

Para levantar una base de datos SQL Server en Docker, ejecuta:

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

🚀 Instrucciones para Inicializar la API

📥 Descargar la imagen de Docker
Accede a:
https://hub.docker.com/repositories/blancaclaire

y descarga la imagen blancaclaire/cursos-api.

▶️ Levantar los contenedores
En la carpeta del proyecto, ejecuta:

docker-compose up -d


🧪 Verificar que ambos contenedores están levantados

docker ps


Debes ver:

El contenedor de la API

El contenedor de la base de datos

🛠️ Abrir tu herramienta de base de datos favorita
(SQL Server Management Studio, DBeaver, HeidiSQL, etc.)

🔧 Crear una nueva conexión con los siguientes datos:

Servidor: localhost,2038
Usuario: sa
Contraseña: TuPasswordFuerte123!
Trust Server Certificate: True


📄 Ejecutar los scripts SQL

createDbSqlServer.sql

insertsDbSqlServer.sql (opcional: inicialización de datos)

🌐 Abrir Swagger en el navegador
http://localhost:8302/swagger
