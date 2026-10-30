-- Resto: datos de prueba mínimos para poder desarrollar y probar.
-- Ejecutar después de 01_estructura.sql.

USE RESTO_DB;
GO

INSERT INTO TiposItem (Descripcion) VALUES ('Plato'), ('Bebida'), ('Postre');
GO

INSERT INTO Items (Nombre, Descripcion, Precio, Stock, IdTipoItem) VALUES
    ('Milanesa con papas', 'Milanesa de carne con papas fritas', 4500, 50, 1),
    ('Ravioles', 'Ravioles de verdura con salsa a elección', 4200, 40, 1),
    ('Gaseosa', 'Botella de 500 ml', 1500, 100, 2),
    ('Agua mineral', 'Botella de 500 ml', 1200, 100, 2),
    ('Flan casero', 'Con dulce de leche y crema', 2500, 30, 3);
GO

INSERT INTO Mesas (Numero, Capacidad) VALUES (1, 4), (2, 4), (3, 6), (4, 2), (5, 8);
GO

-- Perfil: 1 = Gerente, 2 = Mesero. Las claves son provisorias, solo para desarrollo.
INSERT INTO Usuarios (Nombre, Apellido, Email, Clave, Perfil) VALUES
    ('Gerente', 'Prueba', 'gerente@resto.com', '1234', 1),
    ('Mesero', 'Uno', 'mesero1@resto.com', '1234', 2),
    ('Mesero', 'Dos', 'mesero2@resto.com', '1234', 2);
GO

-- Asignación de ejemplo para el día en que se ejecuta el script.
INSERT INTO AsignacionesMesa (Fecha, IdMesa, IdMesero) VALUES
    (CAST(GETDATE() AS DATE), 1, 2),
    (CAST(GETDATE() AS DATE), 2, 2),
    (CAST(GETDATE() AS DATE), 3, 3),
    (CAST(GETDATE() AS DATE), 4, 3);
GO
