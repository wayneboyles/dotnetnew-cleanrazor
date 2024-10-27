# ASP.NET Core Web App Template using Clean Architecture

[![Build status](https://ci.appveyor.com/api/projects/status/afex9kql7uvimofo?svg=true)](https://ci.appveyor.com/project/wayneboyles/dotnetnew-cleanrazor)
![NuGet Downloads](https://img.shields.io/nuget/dt/Boyles.CleanAspNetCore.Template?color=blue)
![NuGet Version](https://img.shields.io/nuget/v/Boyles.CleanAspNetCore.Template?style=flat&color=blue)
![GitHub License](https://img.shields.io/github/license/wayneboyles/dotnetnew-cleanrazor?style=flat&color=red)

## What is this for?

When creating a new ASP.NET web application, I find myself setting up the same things each time and wasting development time. I've packaged my "base" solution into a template to make getting started easier and faster.

## Installation

This template has been created with `dotnet new` in mind. To Install the template, open a PowerShell session and enter the following command:

```
dotnet new install Boyles.AspNetCoreClean.Template
```

This will install the latest version from the NuGet repository.

## Usage

The template will be installed with two short names for use: `cleanwebapp` and `cleanrazor`. Either can be used to generate a new solution.

To create a new solution using the default values and project name of `MyProject`, run the following command:

```
dotnet new cleanwebapp -n "MyProject"
```

### Options

The template comes with a number of configurable options. To view all of the available options, run the following command:

```
dotnet new cleanwebapp --help
```

`--logging | -log` - Either `None` or `Serilog`. Default is `Serilog`.

`--no-restore` - When specified, project references will not be automatically restored.

`--database | -db` - Either `LocalDB` (SQLite), `SQL` (MSSQL Server) or `PGSQL` (PostgreSQL). Default is `SQL`.

---

## Changelog

### v1.0.4

- Added Projects
- Added EF Core Db Context
- Added EF Core Db Context Factory
- Added Generic Repository
- Added Serilog Logging

### v1.0.7

- Added Automapper
- Added choice of Database (LocalDB, SQL Server & PostgreSQL)
- Made the Key property of Entities generic

### v1.1.0 (Breaking Changes)

- Moved Entities to their own Namespace
- Introduced new entity types:
  - AuditedEntity (Replaces AuditableEntity)
  - SoftDeleteEntity
  - SoftDeleteAuditedEntity
- Fixed bugs in the EF Core DbContext
- Added the FluentResults package
- Added configuration options to VS New Project Wizard
- Fixed bugs with the IRepository implementation
