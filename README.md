# Stix

## Requirements
.NET 7


## Setup


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