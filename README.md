# Stix
A Web API for storing Stix II vulnerabilities.

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

Example POST request:

POST /vulnerabilities
```json
{
  "type": "vulnerability",
  "spec_version": "2.1",
  "id": "vulnerability--717cb1c5-eab3-4330-8340-e4858055aa8a",
  "created": "2015-05-15T09:12:16.432Z",
  "modified": "2015-05-15T09:12:16.432Z",
  "name": "CVE-2010-3333",
  "description": "test",
  "confidence": 100,
  "external_references": [
    {
      "source_name": "cve",
      "external_id": "CVE-2010-3333"
    }
  ]
}
```


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

## Validation
When creating or updating a Vulnerability object, the input is validated using a json schema.
The validation is run as part of the `VulnerabilityValidationFilter` on POST and PUT endpoints.

## Error Handling
The `HttpResponseExceptionFilter` is active for all endpoints and will catch any exceptions 
and map them to an appropriate HTTP responses. 

## Unit Tests
The project contains a few unit tests in the Stix.Test project. Run them through the IDE or with:
```
dotnet test Stix.Test
```

## Integration Tests
The project also contains a few integration tests. These require a running MongoDB. Run them through the IDE or with:
```
dotnet test Stix.IntegrationTest
```
