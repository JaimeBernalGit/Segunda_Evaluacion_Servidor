CREATE DATABASE GestionCursosDB;
USE GestionCursosDB;
CREATE TABLE Usuario (
    usuario_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    nombre_usuario NVARCHAR(50) NOT NULL UNIQUE, 
    password NVARCHAR(255) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    fecha_registro DATETIME NOT NULL DEFAULT GETDATE(), 
    estado NVARCHAR(20) DEFAULT 'activo'
);

CREATE TABLE Curso (
    curso_id INT IDENTITY(1,1) PRIMARY KEY,
    titulo NVARCHAR(150) NOT NULL,
    descripcion NVARCHAR(MAX), 
    cateria NVARCHAR(50),
    nivel NVARCHAR(20),
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    precio DECIMAL(10, 2) NOT NULL DEFAULT 0.00 CHECK (precio >= 0)
);

CREATE TABLE Leccion (
    leccion_id INT IDENTITY(1,1) PRIMARY KEY,
    curso_id INT NOT NULL,
    titulo NVARCHAR(150) NOT NULL,
    contenido_url NVARCHAR(255),
    duracion_min INT,
    descripcion NVARCHAR(MAX),
    CONSTRAINT FK_Leccion_Curso FOREIGN KEY (curso_id) REFERENCES Curso(curso_id)
);


CREATE TABLE Inscripcion (
    inscripcion_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    curso_id INT NOT NULL,
    fecha_inscripcion DATETIME NOT NULL DEFAULT GETDATE(),
    progreso_porcentaje DECIMAL(5, 2) DEFAULT 0.00 CHECK (progreso_porcentaje BETWEEN 0 AND 100),
    estado NVARCHAR(20) DEFAULT 'cursando',
    
    CONSTRAINT FK_Inscripcion_Usuario FOREIGN KEY (usuario_id) REFERENCES Usuario(usuario_id),
    CONSTRAINT FK_Inscripcion_Curso FOREIGN KEY (curso_id) REFERENCES Curso(curso_id)
);


CREATE TABLE Resena (
    resena_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    curso_id INT NOT NULL,
    calificacion INT NOT NULL CHECK (calificacion >= 1 AND calificacion <= 5),
    comentario NVARCHAR(MAX),
    fecha_publicacion DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_Resena_Usuario FOREIGN KEY (usuario_id) REFERENCES Usuario(usuario_id),
    CONSTRAINT FK_Resena_Curso FOREIGN KEY (curso_id) REFERENCES Curso(curso_id)
);


CREATE TABLE Pago (
    pago_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    curso_id INT NOT NULL,
    cantidad DECIMAL(10, 2) NOT NULL CHECK (cantidad >= 0),
    nombre_titular NVARCHAR(100) NOT NULL,
    numero_titular NVARCHAR(20) NOT NULL,
    cvv NVARCHAR(5) NOT NULL,
    fecha_pago DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_Pago_Usuario FOREIGN KEY (usuario_id) REFERENCES Usuario(usuario_id),
    CONSTRAINT FK_Pago_Curso FOREIGN KEY (curso_id) REFERENCES Curso(curso_id)
);

