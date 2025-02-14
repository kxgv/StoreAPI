# StoreAPI

Por falta de tiempo, no de compromiso:  
- los tests y flujo de edición no han sido realizados. 

Disculpas de antemano. 

## Info
Se ha usado:

- SQL SERVER EXPRESS
- EntityFramework
- Repository
- Auth BCrypt
- Migraciones

## Angular

Para Angular se ha habilitado el puerto http://localhost:4200",
API en -> "http://localhost:5018",

Se ha deshabilitado el SSL y HTTPS ya que Angular da problemas.

## Base de datos

server localhost\SQLEXPRESS
databaseName: DEV
connectionString: "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DEV;TrustServerCertificate=True;Integrated Security=True;"

## Migrations

Para aplicar las migraciones

```bash
dotnet ef database update --project StoreAPI.Infraestructure --startup-project StoreAPI.WebApi
```

Borrar en caso de que algo salga mal

```bash
dotnet ef migrations remove --project StoreAPI.Infraestructure --startup-project StoreAPI.WebApi
```
