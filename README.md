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

   Se puede hacer desde SQL Server Management Studio o por consola:

   ```
   sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i db\01_estructura.sql
   sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i db\02_datos_prueba.sql
   ```

3. Dejar `web-app` como proyecto de inicio y ejecutar con F5.

La cadena de conexión está en `web-app/Web.config` (nombre `RestoDB`) y apunta a
`(localdb)\MSSQLLocalDB`, base `RESTO_DB`.

## Datos de prueba

Usuarios cargados por `02_datos_prueba.sql` (clave provisoria `1234`):

| Email               | Perfil  |
|---------------------|---------|
| gerente@resto.com   | Gerente |
| mesero1@resto.com   | Mesero  |
| mesero2@resto.com   | Mesero  |

## Base de datos

- Cada cambio en la estructura va en un script numerado nuevo dentro de `db/`
  (`03_...sql`, `04_...sql`), sin editar los anteriores.
- Las bajas son lógicas (columna `Activo`).
