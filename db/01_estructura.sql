-- Resto: estructura de la base de datos (SQL Server)
-- Ejecutar completo. Si la base ya existe, no la vuelve a crear.

IF DB_ID('RESTO_DB') IS NULL
    CREATE DATABASE RESTO_DB;
GO

USE RESTO_DB;
GO

-- Necesario para el índice filtrado de Pedidos; algunas herramientas lo traen apagado.
SET QUOTED_IDENTIFIER ON;
GO

-- Valores numéricos usados en la aplicación:
--   Usuarios.Perfil: 1 = Gerente, 2 = Mesero
--   Pedidos.Estado:  1 = Abierto, 2 = Cerrado
-- Las bajas son lógicas (columna Activo); no se borra lo que ya tiene historia.

CREATE TABLE TiposItem (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL,
    Activo      BIT         NOT NULL DEFAULT 1
);
GO

CREATE TABLE Items (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      VARCHAR(100)  NOT NULL,
    Descripcion VARCHAR(250)  NULL,
    Precio      DECIMAL(10,2) NOT NULL CHECK (Precio >= 0),
    Stock       INT           NOT NULL DEFAULT 0 CHECK (Stock >= 0),
    IdTipoItem  INT           NOT NULL,
    Activo      BIT           NOT NULL DEFAULT 1,
    CONSTRAINT FK_Items_TiposItem FOREIGN KEY (IdTipoItem) REFERENCES TiposItem(Id)
);
GO

CREATE TABLE Mesas (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Numero    INT NOT NULL UNIQUE,
    Capacidad INT NOT NULL CHECK (Capacidad > 0),
    Activo    BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Usuarios (
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    Nombre   VARCHAR(50)  NOT NULL,
    Apellido VARCHAR(50)  NOT NULL,
    Email    VARCHAR(100) NOT NULL UNIQUE,
    Clave    VARCHAR(100) NOT NULL,
    Perfil   INT          NOT NULL CHECK (Perfil IN (1, 2)),
    Activo   BIT          NOT NULL DEFAULT 1
);
GO

-- Una fila por mesa y día. Reasignar una mesa modifica IdMesero de esa fila.
CREATE TABLE AsignacionesMesa (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Fecha     DATE NOT NULL,
    IdMesa    INT  NOT NULL,
    IdMesero  INT  NOT NULL,
    CONSTRAINT UQ_AsignacionesMesa_Fecha_Mesa UNIQUE (Fecha, IdMesa),
    CONSTRAINT FK_AsignacionesMesa_Mesas    FOREIGN KEY (IdMesa)   REFERENCES Mesas(Id),
    CONSTRAINT FK_AsignacionesMesa_Usuarios FOREIGN KEY (IdMesero) REFERENCES Usuarios(Id)
);
GO

-- IdMesero guarda quién atendió el pedido, aunque la mesa se reasigne después.
CREATE TABLE Pedidos (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdMesa         INT           NOT NULL,
    IdMesero       INT           NOT NULL,
    FechaApertura  DATETIME      NOT NULL DEFAULT GETDATE(),
    FechaCierre    DATETIME      NULL,
    Estado         INT           NOT NULL DEFAULT 1 CHECK (Estado IN (1, 2)),
    Total          DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (Total >= 0),
    CONSTRAINT FK_Pedidos_Mesas    FOREIGN KEY (IdMesa)   REFERENCES Mesas(Id),
    CONSTRAINT FK_Pedidos_Usuarios FOREIGN KEY (IdMesero) REFERENCES Usuarios(Id)
);
GO

-- Una mesa no puede tener más de un pedido abierto (Estado = 1) a la vez.
CREATE UNIQUE INDEX UX_Pedidos_MesaAbierta ON Pedidos (IdMesa) WHERE Estado = 1;
GO

-- Un ítem aparece una sola vez por pedido: si se pide de nuevo, se suma la cantidad.
-- PrecioUnitario guarda el precio del ítem al momento de cargarlo.
CREATE TABLE DetallesPedido (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido       INT           NOT NULL,
    IdItem         INT           NOT NULL,
    Cantidad       INT           NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(10,2) NOT NULL CHECK (PrecioUnitario >= 0),
    CONSTRAINT UQ_DetallesPedido_Pedido_Item UNIQUE (IdPedido, IdItem),
    CONSTRAINT FK_DetallesPedido_Pedidos FOREIGN KEY (IdPedido) REFERENCES Pedidos(Id),
    CONSTRAINT FK_DetallesPedido_Items   FOREIGN KEY (IdItem)   REFERENCES Items(Id)
);
GO
