![Build Status](https://github.com/ElmahCoreMagic/ElmahCoreMagic/actions/workflows/build.yml/badge.svg)
![Latest Version](https://img.shields.io/nuget/v/ElmahCoreMagic?style=flat-square)
![Version Pre-release](https://img.shields.io/nuget/vpre/ElmahCoreMagic?style=flat-square)
![Last Commit](https://img.shields.io/github/last-commit/ElmahCoreMagic/ElmahCoreMagic?style=flat-square)

![Code Quality](https://img.shields.io/codefactor/grade/github/ElmahCoreMagic/ElmahCoreMagic/develop?style=flat-square)
![Dependencies](https://img.shields.io/librariesio/github/ElmahCoreMagic/ElmahCoreMagic?style=flat-square)

![License](https://img.shields.io/github/license/ElmahCoreMagic/ElmahCoreMagic)
![Contributors](https://img.shields.io/github/contributors/ElmahCoreMagic/ElmahCoreMagic?style=flat-square)

# About

This is a fork of the [ElmahCoreEx](https://github.com/ElmahCoreEx/ElmahCoreEx) project's develop branch which
is a fork of the [ElmahCore](https://github.com/ElmahCore/ElmahCore) project, with changes for PR's that were sitting idle.
It should be an almost slot in replacement for ElmahCore v2.1.2.

My long term goal is to add deletion of log entries.

See [changelog.md](changelog.md) for any changes from [ElmahCoreEx](https://github.com/ElmahCoreEx/ElmahCoreEx) develop branch.

The interfaces and namespaces have been kept the same.

**Note:** At this time there are no NuGet packages for this fork despite this README.md referring to them.

# License

This project is licensed under the terms of the Apache license 2.0.

# Warnings & Dragons

The source code for the front end appears non-existent, in ElmahCore the front end Vue SPA files are all [minified](https://github.com/ElmahCore/ElmahCore/issues/77). Consider this a warning sign for the continuation of the front end without a rewrite WITH SOURCE. Source-maps may have enough content to obtain the code but this has not be investigated.

# Using ElmahCore

ELMAH for Net.Standard 2.0 and .Net 6

Add NuGet package [ElmahCoreMagic](https://www.nuget.org/packages?q=ElmahCoreMagic)

## Simple usage

```csharp
// Startup.cs
services.AddElmah() //in ConfigureServices
// ...
app.UseElmah(); // in Configure, must be positioned after initializing other exception handlers
                // such as UseExceptionHandler and UseDeveloperExceptionPage
```

Default ELMAH endpoint path `~/elmah`.

## Change URL path

```csharp
services.AddElmah(options => options.Path = "you_path_here")
```

## Restrict access to the ELMAH URL

```csharp
services.AddElmah(options =>
{
    options.OnPermissionCheck = context => context.User.Identity.IsAuthenticated;
});
```

```csharp
// startup.cs
app.UseAuthentication();
app.UseAuthorization();
//...
app.UseElmah(); // needs to be positioned after `UseAuthentication` and `UseAuthorization`
```
or the user will be redirected to the sign in screen even if they are authenticated.

## Change Error Log type

You can implement a custom error log adapter, to write logs to alternate locations.

```csharp
public class MyErrorLog: ErrorLog {
    // Implement ErrorLog
}    
```

The ErrorLog adapters available:

- **MemoryErrorLog** – store errors in memory (by default)
- **XmlFileErrorLog** – store errors in XML files.
- **SqlErrorLog** - store errors in MS SQL (add reference to [ElmahCoreMagic.Sql](https://www.nuget.org/packages/ElmahCoreMagic.Sql))
- **MysqlErrorLog** - store errors in MySQL (add reference to [ElmahCoreMagic.MySql](https://www.nuget.org/packages/ElmahCoreMagic.MySql))
- **PgsqlErrorLog** - store errors in PostgreSQL (add reference to [ElmahCoreMagic.Postgresql](https://www.nuget.org/packages/ElmahCoreMagic.Postgresql))

Example to configure for XML:

```csharp
services.AddElmah<XmlFileErrorLog>(options =>
{
  options.LogPath = "~/log"; // OR options.LogPath = "с:\errors";
});
```

Example to configure For MSSQL

```csharp
services.AddElmah<SqlErrorLog>(options =>
{
  options.ConnectionString = "connection_string";
  options.SqlServerDatabaseSchemaName = "Errors"; // Defaults to dbo if not set
  options.SqlServerDatabaseTableName = "ElmahError"; // Defaults to ELMAH_Error if not set
});
```

## Raise exception

To raise a custom exception to log:

```csharp
public IActionResult RaiseCustomExceptionExample()
{
    HttpContext.RaiseError(new InvalidOperationException("Test"));
}
```

## Microsoft.Extensions.Logging support

ElmahCoreMagic support `Microsoft.Extensions.Logging`

## Source Preview

ElmahCoreMagic support source preview.
Add paths to source files example:

```csharp
services.AddElmah(options =>
{
   options.SourcePaths = new []
   {
      @"D:\src\ElmahCore.DemoCore6",
      @"D:\src\ElmahCore.Mvc",
      @"D:\src\ElmahCore"
   };
});
```

## Logging request body

V2.0.5+ ElmahCoreMagic can log the request body.

## Logging SQL request body

v2.0.6+ ElmahCoreMagic can log the SQL request body.

## Logging method parameters

V2.0.6+ ElmahCoreMagic can log method parameters.

```csharp
using ElmahCore;
...

public void TestMethod(string p1, int p2)
{
    // Logging method parameters
    this.LogParams((nameof(p1), p1), (nameof(p2), p2));
    ...
}
```

## Using UseElmahExceptionPage

You can replace `UseDeveloperExceptionPage` to `UseElmahExceptionPage`

```csharp
if (env.IsDevelopment())
{
   // app.UseDeveloperExceptionPage();
   app.UseElmahExceptionPage();
}
```

## Using Notifiers

You can create custom notifiers by implementing `IErrorNotifier` or `IErrorNotifierWithId` interface and add notifier to Elmah options:

```csharp
services.AddElmah<XmlFileErrorLog>(options =>
{
    options.Path = @"errors";
    options.LogPath = "~/logs";
    options.Notifiers.Add(new ErrorMailNotifier("Email",emailOptions));
});
```
Each notifier must have unique name.

## Using Filters

You can use Elmah XML filter configuration in a separate file, create and add custom filters:

```csharp
services.AddElmah<XmlFileErrorLog>(options =>
{
    options.FiltersConfig = "elmah.xml";
    options.Filters.Add(new MyFilter());
})
```
Custom filter must implement `IErrorFilter`.
An XML filter config example:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<elmah>
 <errorFilter>
  <notifiers>
   <notifier name="Email"/>
  </notifiers>
  <test>
   <and>
    <greater binding="HttpStatusCode" value="399" type="Int32" />
    <lesser  binding="HttpStatusCode" value="500" type="Int32" />
   </and>
  </test>
 </errorFilter>
</elmah>
```

see more [here](https://elmah.github.io/a/error-filtering/examples/)

JavaScript filters have not been implemented

Add notifiers to errorFilter node if you do not want to send notifications
Filtered errors will be logged, but will not be sent.
