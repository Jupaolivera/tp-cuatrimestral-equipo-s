-- Resto: pedidos cerrados de ejemplo (de hoy y de los dos días anteriores) para probar los reportes.
-- Ejecutar después de 02_datos_prueba.sql.

USE RESTO_DB;
GO

-- Necesario para insertar en Pedidos (índice filtrado); algunas herramientas lo traen apagado.
SET QUOTED_IDENTIFIER ON;
GO

DECLARE @hoy      DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
DECLARE @ayer     DATETIME = DATEADD(DAY, -1, @hoy);
DECLARE @anteayer DATETIME = DATEADD(DAY, -2, @hoy);

DECLARE @mesa1 INT = (SELECT Id FROM Mesas WHERE Numero = 1);
DECLARE @mesa2 INT = (SELECT Id FROM Mesas WHERE Numero = 2);
DECLARE @mesa3 INT = (SELECT Id FROM Mesas WHERE Numero = 3);
DECLARE @mesa4 INT = (SELECT Id FROM Mesas WHERE Numero = 4);

DECLARE @mesero1 INT = (SELECT Id FROM Usuarios WHERE Email = 'mesero1@resto.com');
DECLARE @mesero2 INT = (SELECT Id FROM Usuarios WHERE Email = 'mesero2@resto.com');

DECLARE @p INT;

-- Mesa 1, mesero1, hoy
INSERT INTO Pedidos (IdMesa, IdMesero, FechaApertura, FechaCierre, Estado, Total)
VALUES (@mesa1, @mesero1, DATEADD(HOUR, 13, @hoy), DATEADD(HOUR, 14, @hoy), 2, 12000);
SET @p = SCOPE_IDENTITY();
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 2, Precio FROM Items WHERE Nombre = 'Milanesa con papas';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 2, Precio FROM Items WHERE Nombre = 'Gaseosa';

-- Mesa 2, mesero1, hoy
INSERT INTO Pedidos (IdMesa, IdMesero, FechaApertura, FechaCierre, Estado, Total)
VALUES (@mesa2, @mesero1, DATEADD(HOUR, 14, @hoy), DATEADD(HOUR, 15, @hoy), 2, 5400);
SET @p = SCOPE_IDENTITY();
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 1, Precio FROM Items WHERE Nombre = 'Ravioles';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 1, Precio FROM Items WHERE Nombre = 'Agua mineral';

-- Mesa 3, mesero2, hoy
INSERT INTO Pedidos (IdMesa, IdMesero, FechaApertura, FechaCierre, Estado, Total)
VALUES (@mesa3, @mesero2, DATEADD(HOUR, 20, @hoy), DATEADD(HOUR, 22, @hoy), 2, 16700);
SET @p = SCOPE_IDENTITY();
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 1, Precio FROM Items WHERE Nombre = 'Milanesa con papas';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 1, Precio FROM Items WHERE Nombre = 'Ravioles';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 2, Precio FROM Items WHERE Nombre = 'Flan casero';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 2, Precio FROM Items WHERE Nombre = 'Gaseosa';

-- Mesa 1, mesero1, ayer
INSERT INTO Pedidos (IdMesa, IdMesero, FechaApertura, FechaCierre, Estado, Total)
VALUES (@mesa1, @mesero1, DATEADD(HOUR, 12, @ayer), DATEADD(HOUR, 13, @ayer), 2, 10800);
SET @p = SCOPE_IDENTITY();
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 2, Precio FROM Items WHERE Nombre = 'Ravioles';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 2, Precio FROM Items WHERE Nombre = 'Agua mineral';

-- Mesa 4, mesero2, ayer
INSERT INTO Pedidos (IdMesa, IdMesero, FechaApertura, FechaCierre, Estado, Total)
VALUES (@mesa4, @mesero2, DATEADD(HOUR, 21, @ayer), DATEADD(HOUR, 22, @ayer), 2, 6000);
SET @p = SCOPE_IDENTITY();
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 1, Precio FROM Items WHERE Nombre = 'Milanesa con papas';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 1, Precio FROM Items WHERE Nombre = 'Gaseosa';

-- Mesa 3, mesero2, hace dos días
INSERT INTO Pedidos (IdMesa, IdMesero, FechaApertura, FechaCierre, Estado, Total)
VALUES (@mesa3, @mesero2, DATEADD(HOUR, 13, @anteayer), DATEADD(HOUR, 15, @anteayer), 2, 20100);
SET @p = SCOPE_IDENTITY();
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 3, Precio FROM Items WHERE Nombre = 'Ravioles';
INSERT INTO DetallesPedido (IdPedido, IdItem, Cantidad, PrecioUnitario)
    SELECT @p, Id, 3, Precio FROM Items WHERE Nombre = 'Flan casero';
GO
