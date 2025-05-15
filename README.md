# Sistema de Solicitudes de Créditos

Este proyecto es una solución completa para la gestión de solicitudes de crédito bancario, dividido en backend y frontend, cumpliendo con los requerimientos técnicos establecidos.

## Estructura del Proyecto

- /aspnet-core → Backend desarrollado en ASP.NET Core 9 (API REST).
- /angular → Frontend SPA desarrollado con Angular 16.

## Requisitos

- .NET 9 SDK
- Node.js 18+
- Angular CLI
- SQL Server Express

## Configuración y Ejecución

### Backend (aspnet-core)

1. Abrir la carpeta del backend (`aspnet-core`).
2. Restaurar dependencias abriendo `ProjectBlack.sln` en Visual Studio.
3. Aplicar migraciones desde la consola NuGet:
   - Seleccionar el proyecto `ProjectBlack.EntityFrameworkCore`.
   - Ejecutar el comando: `Update-Database`
4. Ejecutar el backend desde `ProjectBlack.Web.Host`.

### Frontend (angular)

1. Ingresar a la carpeta `angular`.
2. Ejecutar: `yarn install --force`
3. Ejecutar: `npm start`

## Autenticación

- Basada en JWT.

## Exportación de Datos

- Las solicitudes pueden exportarse a formato PDF o Excel.

## Restaurar Base de Datos (con datos de ejemplo)

1. Abrir SQL Server Management Studio (SSMS).
2. Clic derecho en "Databases" > Restore Database...
3. Seleccionar "Device" > Buscar `ProjectBankDb.bak`.
4. Seguir el asistente para restaurar.

### Usuario por defecto

- Usuario: `admin`
- Contraseña: `123qwe`

## Evaluación

- Separación clara entre backend y frontend.
- Código organizado y comentado.
- Evaluación automática de solicitudes.
- Control de roles: Solicitante y Analista.
- Registro de acciones del sistema.
