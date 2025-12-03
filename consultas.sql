USE GestionCursosDB;
GO

SELECT titulo, categoria, nivel, precio
FROM Curso
ORDER BY precio DESC;