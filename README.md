# tp-cuatrimestral-equipo-s

TP Cuatrimestral de Programación III (UTN FRGP, TUP) — Equipo S.
Aplicación web **Resto**: gestión de pedidos y mesas de un restaurante.

## Tecnología

- C# / .NET Framework 4.8
- ASP.NET Web Forms
- ADO.NET (sin Entity Framework)
- SQL Server (LocalDB)

## Estructura

```
TPCuatrimestral_equipo-s.sln     Solución de Visual Studio
db/                              Scripts de la base de datos
dominio/                         Clases del modelo
negocio/                         Acceso a datos y lógica de negocio
web-app/                         Páginas web
```

## Cómo correrlo

1. Clonar el repositorio y abrir `TPCuatrimestral_equipo-s.sln` en Visual Studio.
   Hace falta la carga de trabajo de ASP.NET y desarrollo web y el componente
   "Plantillas de proyecto y de elemento de .NET Framework".
2. Crear la base de datos ejecutando, en este orden, los scripts de `db/`:
   1. `01_estructura.sql`
   2. `02_datos_prueba.sql`
   3. `03_pedidos_prueba.sql` (opcional): pedidos cerrados de ejemplo para probar
      los reportes. Ejecutarlo una sola vez, porque si no duplica los pedidos.

   Se puede hacer desde SQL Server Management Studio o por consola:

   ```
   sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i db\01_estructura.sql
   sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i db\02_datos_prueba.sql
   sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i db\03_pedidos_prueba.sql
   ```

3. Dejar `web-app` como proyecto de inicio y ejecutar con F5.

La cadena de conexión está en `web-app/Web.config` (nombre `RestoDB`) y apunta a
`(localdb)\MSSQLLocalDB`, base `RESTO_DB`.

## Datos de prueba

Usuarios cargados por `02_datos_prueba.sql` (clave provisoria `1234`):

| Email               | Nombre          | Perfil  |
|---------------------|-----------------|---------|
| gerente@resto.com   | Laura Ramos     | Gerente |
| mesero1@resto.com   | Marcos Pereyra  | Mesero  |
| mesero2@resto.com   | Julieta Acosta  | Mesero  |

Si la base ya estaba cargada con los nombres anteriores (Gerente Prueba, Mesero Uno
y Mesero Dos), no hace falta volver a crearla: alcanza con ejecutar esto sobre
`RESTO_DB`.

```sql
UPDATE Usuarios SET Nombre='Laura',   Apellido='Ramos'   WHERE Email='gerente@resto.com';
UPDATE Usuarios SET Nombre='Marcos',  Apellido='Pereyra' WHERE Email='mesero1@resto.com';
UPDATE Usuarios SET Nombre='Julieta', Apellido='Acosta'  WHERE Email='mesero2@resto.com';
```

## Base de datos

- Cada cambio en la estructura va en un script numerado nuevo dentro de `db/`
  (`04_...sql`, `05_...sql`), sin editar los anteriores.
- Las bajas son lógicas (columna `Activo`).
