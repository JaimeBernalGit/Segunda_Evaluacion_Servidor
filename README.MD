#Setup local db (Sql server)
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

<h1>Instrucciones para incializar la api<h1>

1. Accede al siguiente link: https://hub.docker.com/repositories/blancaclaire y descargate la imagen: blancaclaire/cursos-api
2. Ejecuta el comando: docker-compose up -d para levantar el contenedor
3. Ejecuta el comando:  docker ps, y comprueba que estan corriendo tus dos contenedores; la api y la base de datos
4. Abre tu herramienta de base de datos favorita (MsqlServer, DBeaver, HeidySql...)
5.Crea una nueva conexión que con estos datos: 
  Servidor:localhost,2038
  Usuario: sa
  Contraseña: TuPasswordFuerte123!
  Trust Server Certificate: True
6. Ejecuta los scripts: createDbSqlServer.sql y insertsDbSqlServer.sql (Opcional: Inicialización de datos)
7. Abre tu navegador y entra en: http://localhost:8302/swagger
