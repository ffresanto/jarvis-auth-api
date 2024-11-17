# JarvisAuth – User, Application, and Permission Manager in ASP.NET 8

JarvisAuth is an API designed to simplify the management of users, applications, and permissions. I developed this project to study software architecture patterns and other development practices in .NET. In the future, I plan to add new features as I progress in my learning roadmap and explore new technologies and best practices.

---

###### The architecture of this project was designed to be both functional and scalable, making it a potential reference even for real-world applications. If you find the project interesting, please leave a star :star:

## Technologies / Components Implemented

- ASP.NET 8 WebApi
- HealthCheck
- Clean Architecture
- SQLite
- Unit Testing (with xUnit)
- JWT Token
- Refresh Token
- Docker (with Dockerfile and Compose)
- ElasticSearch
- Kibana

## Architecture Overview

The project is architected in 5 layers:

- **Presentation**: Interface layer responsible for exposing the Web API and receiving user requests.
- **Application**: Application layer tasked with orchestrating use cases and managing logic between the domain and infrastructure layers.
- **Core**: Shared layer containing reusable classes that do not depend on other layers.
- **Domain**: Business layer where the fundamental rules and entities of the application reside.
- **Infrastructure**: Infrastructure layer responsible for interacting with external resources, such as databases and other services.

All following the principles of Clean Architecture and SOLID to ensure better organization, scalability, and best coding practices.

![image](https://github.com/user-attachments/assets/fa8e1b16-8dfb-41e0-9160-ef1de148ab2e)
  
## Database Diagram Overview

I used SQLite as the database to simplify application execution, making it easier for anyone who wants to explore the project in more depth.

![jarvis_auth](https://github.com/user-attachments/assets/f96056d2-cb60-4a1c-acc9-a15485cf6e2b)

## Overview of Endpoints in Swagger

![screencapture-localhost-5274-swagger-index-html-2024-11-16-21_05_38](https://github.com/user-attachments/assets/0acd5995-4020-41c5-b27d-b05050f73aab)

## Overview of the Kibana Dashboard with ElasticSearch

The Kibana dashboard provides a visual representation of the logs and metrics collected by Elasticsearch. It is configured to help monitor the application's behavior, track requests, and debug issues effectively.

![image](https://github.com/user-attachments/assets/00326a70-5c14-46d4-9ba3-d4d6b3146047)

# Getting Started

The JarvisAuth project can be run on any operating system, **as long as Docker is installed on your environment.** ([Get Docker Installation Guide](https://docs.docker.com/get-docker/))

Clone the JarvisAuth repository, navigate to the root folder where the **docker-compose.yml** file is located, and follow the instructions below:

### If you only want to run the JarvisAuth application in your Docker environment:

```
docker-compose up
```
After completing the container creation in Docker, the application will be available at the following addresses:

- **JarvisAuth API:** ```http://localhost:8081/swagger/index.html```
- **Kibana Dashboard:** ```http://localhost:5601/```

### If you want to run the JarvisAuth application in Visual Studio:

Make sure you have the following prerequisites installed:

- Visual Studio 2022 (Recommended)
- .NET SDK 8

1. Open the **JarvisAuth.sln** file in Visual Studio.
2. Right-click on the JarvisAuth.API project and select "Set as Startup Project."
3. Press **F5** or click **Run** to start the application.

## How to Use the API's Initial Endpoints:

### 1. How to Register a User

**Endpoint:**  
`POST /api/user`

**Example Request:**
```json
{ 
  "name": "Franccesco Felipe",
  "email": "franccesco@gmail.com",
  "password": "1234567"
}
```

**Example Response:**
```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully.",
  "data": {
    "userId": "ece8d256-5cf8-4ce9-86b7-5d8bd2fa5566"
  },
  "errors": []
}
```

### 2. How to Authenticate in the Application

**Endpoint:**  
`POST /api/auth/login`

**Example Request:**
```json
{ 
  "name": "Franccesco Felipe",
  "email": "franccesco@gmail.com",
  "password": "1234567"
}
```
**Example Response:**
```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully.",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4ZDBmNjE0Ny1hMWIwLTRjZjYtYjQ0YS03ZTBkYTE4ZjgzZGUiLCJ1c2VySWQiOiJlY2U4ZDI1Ni01Y2Y4LTRjZTktODZiNy01ZDhiZDJmYTU1NjYiLCJJc0FkbWluIjoiVHJ1ZSIsInVzZXJFbWFpbCI6ImZyYW5jY2VzY29AZ21haWwuY29tIiwiQXBwbGljYXRpb24iOiIiLCJQZXJtaXNzaW9ucyI6IltdIiwiZXhwIjoxNzMxODAzNDU2LCJpc3MiOiJKYXJ2aXNBdXRoLkFQSSIsImF1ZCI6ImxvY2FsaG9zdCJ9.gw97AL6yc9-LhcMeguvcBjL_t8_0IlGvszrrQcb0ez8",
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxNGUyY2FjOS1mY2NlLTQzZWItOGQxOC1mYTkwZGYwZmIwODMiLCJ1c2VySWQiOiJlY2U4ZDI1Ni01Y2Y4LTRjZTktODZiNy01ZDhiZDJmYTU1NjYiLCJpc0FkbWluIjoiVHJ1ZSIsIkFwcGxpY2F0aW9uIjoiIiwiUGVybWlzc2lvbnMiOiJbXSIsImV4cCI6MTczMjQwNDY1NiwiaXNzIjoiSmFydmlzQXV0aC5BUEkiLCJhdWQiOiJsb2NhbGhvc3QifQ.CLzZJYfBc3mgJH92P1yjZiDbvfhKBk2X5aKSXHqMjxc"
  },
  "errors": []
}
```

With the returned token, authenticate using Bearer. If you're using Swagger, simply paste the token in the **Authorize** button.

### 3. How to Register an Application

**Endpoint:**  
`POST /api/application`

**Example Request:**
```json
{
  "name": "EasyApp"
}
```

**Example Response:**
```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully.",
  "data": {
    "applicationId": "c3723c1b-f874-4af7-8584-24039929cc48"
  },
  "errors": []
}
```

### 4. How to Create User Permissions for an Application

**Endpoint:**  
`POST /api/application/permission`

**Example Request:**
```json
{
  "applicationId": "c3723c1b-f874-4af7-8584-24039929cc48",
  "name": "Manager"
}
```

**Example Response:**
```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully.",
  "data": {
    "applicationPermissionId": "4c7b297a-73f0-4500-ad8a-fc2deb960a09"
  },
  "errors": []
}
```

### 5. How to Link a User to an Application

**Endpoint:**  
`POST /api/user/associate/application`

**Example Request:**
```json
{
  "userId": "ece8d256-5cf8-4ce9-86b7-5d8bd2fa5566",
  "applicationId": "c3723c1b-f874-4af7-8584-24039929cc48"
}
```

**Example Response:**
```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully.",
  "data": {
    "info": "Record saved successfully."
  },
  "errors": []
}
```

### 6. How to Assign a Permission to a User

**Endpoint:**  
`POST /api/user/associate/permission`

**Example Request:**
```json
{
  "userId": "ece8d256-5cf8-4ce9-86b7-5d8bd2fa5566",
  "applicationPermissionId": "4c7b297a-73f0-4500-ad8a-fc2deb960a09"
}
```

**Example Response:**
```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully.",
  "data": {
    "info": "Record saved successfully."
  },
  "errors": []
}
```

## appsettings.json Configuration

The `appsettings.json` file contains essential settings for the operation of the API. Below is a detailed explanation of each section and how it affects the application behavior.

### Structure

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "ConnectionStrings": {
        "Sqlite": "Data Source=jarvis.db"
    },
    "JwtSettings": {
        "Key": "fdfb6ab3278754abcd69833ae2d110c0",
        "Issuer": "JarvisAuth.API",
        "Audience": "localhost",
        "ExpiresInMinutes": 60,
        "RefreshTokenExpiresInDays": 7
    },
    "ElasticSearchUrl": "http://elasticsearch:9200"
}
```

### Configuration Details

#### Logging
Defines the log levels used by the application.  
- **LogLevel**: Specifies the level of detail for logs.  
  - `Default`: Default logging level for the entire application (`Information` in this case).  
  - `Microsoft.AspNetCore`: Specific logging level for ASP.NET Core framework logs (`Warning` in this case).

#### ConnectionStrings
Contains settings for database connections.  
- **Sqlite**: Defines the connection string for the SQLite database. In this case, the database will be stored in the `jarvis.db` file.

#### JwtSettings
Settings related to JSON Web Token (JWT) authentication.  
- **Key**: Secret key used to sign and validate JWT tokens. **Ensure this key is not exposed in public environments**.  
- **Issuer**: Identifies the issuer of the JWT tokens (in this case, `JarvisAuth.API`).  
- **Audience**: Specifies the intended audience for the JWT tokens (in this case, `localhost`).  
- **ExpiresInMinutes**: Access token expiration time in minutes (in this case, 60 minutes).  
- **RefreshTokenExpiresInDays**: Refresh token expiration time in days (in this case, 7 days).

#### ElasticSearchUrl
Specifies the URL of the ElasticSearch service used by the application for data indexing and search.  
- **ElasticSearchUrl**: ElasticSearch service address (in this case, `http://elasticsearch:9200`).


