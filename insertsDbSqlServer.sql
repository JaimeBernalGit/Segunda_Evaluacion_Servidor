-- USE GestionCursosDB;
-- GO
-- INSERT INTO Usuario (nombre, nombre_usuario, password, email, estado)
-- VALUES 
-- ('Ana García', 'ana_g', 'pass1234', 'ana@email.com', 'activo'),
-- ('Pedro Martínez', 'pedro_dev', 'claveSegura', 'pedro@email.com', 'activo'),
-- ('Lucía Fernández', 'lucia_art', 'arte2024', 'lucia@email.com', 'suspendido');

-- GO

-- INSERT INTO Curso (titulo, descripcion, categoria, nivel, precio)
-- VALUES 
-- ('Python para Principiantes', 'Domina los fundamentos de la programación con Python.', 'Programación', 'Básico', 15.50),
-- ('Fotografía Profesional', 'Aprende iluminación y composición avanzada.', 'Arte', 'Avanzado', 45.00),
-- ('Marketing Digital 360', 'Estrategias de SEO, SEM y Redes Sociales.', 'Negocios', 'Intermedio', 29.99);

-- GO

-- INSERT INTO Leccion (curso_id, titulo, contenido_url, duracion_min, descripcion)
-- VALUES 
-- (1, 'Instalación de Python', 'video_url_1', 10, 'Configurando el entorno en Windows y Mac'),
-- (1, 'Variables y Tipos de Datos', 'video_url_2', 25, 'Entendiendo int, string y float'),
-- (1, 'Bucles y Condicionales', 'video_url_3', 30, 'Uso de If, For y While');
-- GO

-- INSERT INTO Leccion (curso_id, titulo, contenido_url, duracion_min, descripcion)
-- VALUES 
-- (2, 'La regla de los tercios', 'video_url_4', 15, 'Composición básica'),
-- (2, 'Iluminación de estudio', 'video_url_5', 45, 'Configuración de softbox y luces');

-- GO
-- INSERT INTO Leccion (curso_id, titulo, contenido_url, duracion_min, descripcion)
-- VALUES 
-- (3, 'Introducción al SEO', 'video_url_6', 20, 'Posicionamiento orgánico');



-- INSERT INTO Pago (usuario_id, curso_id, cantidad, nombre_titular, numero_titular, cvv)
-- VALUES (1, 1, 15.50, 'Ana García', '4000123456789010', '123');


-- INSERT INTO Pago (usuario_id, curso_id, cantidad, nombre_titular, numero_titular, cvv)
-- VALUES (1, 3, 29.99, 'Ana García', '4000123456789010', '123');


-- INSERT INTO Pago (usuario_id, curso_id, cantidad, nombre_titular, numero_titular, cvv)
-- VALUES (2, 2, 45.00, 'Pedro Martínez', '5000987654321098', '999');



-- INSERT INTO Inscripcion (usuario_id, curso_id, progreso_porcentaje, estado)
-- VALUES (1, 1, 100.00, 'completado');

-- INSERT INTO Inscripcion (usuario_id, curso_id, progreso_porcentaje, estado)
-- VALUES (1, 3, 15.50, 'cursando');


-- INSERT INTO Inscripcion (usuario_id, curso_id, progreso_porcentaje, estado)
-- VALUES (2, 2, 50.00, 'cursando');



-- INSERT INTO Resena (usuario_id, curso_id, calificacion, comentario)
-- VALUES (1, 1, 5, 'Excelente curso, muy bien explicado para principiantes.');


-- INSERT INTO Resena (usuario_id, curso_id, calificacion, comentario)
-- VALUES (2, 2, 3, 'Buen contenido, pero el audio de los videos es bajo.');
