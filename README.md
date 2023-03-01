# Stix

## Requirements
.NET 7  
MongoDB

## Running MongoDB
The easiest way to get started is to run MongoDB in a docker container:
```
docker pull mongo:latest
docker run -d -p 27017:27017 --name=Stix mongo:latest
```

The connection parameters can be configured in the appsettings.Development.json file.

## Running the API
Run through the IDE or the command line:
```
dotnet run --project Stix
```

Swagger can be found at http://localhost:5217/swagger/index.html

## Authentication
The API uses JWT for authentication and authorization.
To generate JWTs for the API when testing use [dotnet user-jwts](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-7.0&tabs=windows)

The API supports two different roles. Admin and User. Only the Admin is allowed to perform write operations in the API.

Navigate into the Stix Project and then tokens can be generated like so:
```
dotnet user-jwts create --name Admin --claim role=Admin
dotnet user-jwts create --name User --claim role=User
```
And then used in API calls or through the swagger UI page. 