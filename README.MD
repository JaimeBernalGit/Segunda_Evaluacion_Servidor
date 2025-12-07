📚 API de Gestión de Cursos
Esta API permite gestionar cursos, usuarios, inscripciones, lecciones y reseñas. Está construida con .NET 8 y SQL Server.

Puedes ejecutar el proyecto de dos formas: usando Docker (recomendado para despliegue rápido) o en Entorno Local (recomendado para desarrollo/debugging).

🚀 Opción A: Ejecución con Docker (Recomendado)
Si tienes Docker instalado, esta es la forma más rápida de probar la aplicación.

1. Levantar los servicios
En la carpeta raíz del proyecto (donde está el archivo docker-compose.yaml), ejecuta:

Bash

docker-compose up -d --build
Esto descargará la imagen, levantará la base de datos SQL Server y la API automáticamente.

2. Verificar estado
Comprueba que los contenedores están funcionando:

Bash

docker ps
Deberías ver dos contenedores activos: cursos_api_container y sqlserver_practica.

3. Configuración de Base de Datos
Abre tu gestor de base de datos favorito (SQL Server Management Studio, DBeaver, Azure Data Studio).

Conéctate con estos datos:

Servidor: localhost,2038

Usuario: sa

Contraseña: TuPasswordFuerte123!

Trust Server Certificate: True

Ejecuta los scripts SQL en este orden:

createDbSqlServer.sql (Crea tablas y relaciones)

insertsDbSqlServer.sql (Datos de prueba)

4. Acceder a la API
Abre tu navegador en: 👉 http://localhost:8302/swagger

💻 Opción B: Ejecución Local (Sin Docker)
Usa esta opción si no tienes Docker o quieres depurar el código desde Visual Studio / VS Code.

Prerrequisitos
.NET 8 SDK instalado.

SQL Server instalado localmente (Express, Developer o LocalDB).

1. Configurar Base de Datos Local
Abre tu gestor de SQL Server y conéctate a tu instancia local (ej. (localdb)\MSSQLLocalDB o .\SQLEXPRESS).

Ejecuta el script createDbSqlServer.sql para crear la base de datos GestionCursosDB.

(Opcional) Ejecuta insertsDbSqlServer.sql para tener datos de ejemplo.

2. Configurar la Conexión
Abre el archivo appsettings.json y asegúrate de que la cadena de conexión apunte a tu SQL Server local.

Ejemplo para SQL Express:

JSON

"ConnectionStrings": {
  "GestionCursosDB": "Server=.\\SQLEXPRESS;Database=GestionCursosDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
3. Configurar Clave de Administrador (ApiKey)
Para poder borrar o modificar datos sensibles, define tu clave de seguridad en el appsettings.json:

JSON

"AdminSettings": {
    "ApiKey": "TuClaveSecretaLocal"
}
4. Ejecutar la Aplicación
Desde la terminal en la carpeta del proyecto:

Bash

dotnet run
O presiona el botón de Play ▶️ en Visual Studio.

5. Acceder a la API
Por defecto, al correr en local, Swagger estará disponible en: 👉 http://localhost:5074/swagger (El puerto puede variar según tu archivo launchSettings.json, revisa la consola al iniciar).

🔐 Seguridad y Pruebas
Algunos endpoints (como DELETE) están protegidos.

Header requerido: X-Admin-Key

Valor: El que hayas configurado en AdminSettings:ApiKey (en local) o en el docker-compose.yaml (en Docker).
