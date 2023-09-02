# Xyicon-Assignment

Completed implementing the coding challenge provided by Xyicon. Below are the features/libraries used for the completion of the coding challenge.

-   Solution is structured using Clean Architecture principles
-   Minimal APIs
-   MediatR
-   EF Core
-   Fluent Validation
-   AutoMapper
-   Hangfire
-   Xunit
-   Moq
-   Shouldly

### Requirements Implemented:
| **Requirement**                           | **Completion Status** |
|-------------------------------------------|-----------------------|
| Create endpoint POST /flexibledata/create | 100%                  |
| Get endpoint GET /flexibledata/get/{?id}  | 100%                  |
| Create an asynchronous post-processing system  | 100%                  |
| Get endpoint GET /flexibledata/count/{?key}  | 100%                  |
| Add a column to the "Statistics" table to store the list of unique values | 100% |
| In case of an error, all end-points should return a 400 Bad Request response | 100% |
| Dockerize the application | 100% |

### Steps on running the application:

#### Pre-requisite: Docker Desktop needs to be installed.
1. Clone the repository
2. Open the cloned folder using File Explorer and navigate to /FlexibleData folder
3. The docker-compose.yml file is located in this folder
4. Launch a command prompt from the current directory
5. Run the following commands in the command prompt in-order
   - docker-compose build
   - docker-compose up
6. Above commands will create the API image and the containers for hosting the API and SQL Server in docker
7. The API will be available via http://localhost:8080 URL

#### Note: Postman collection is also available in the repository

### Data set used for create method
I assumed that assets in a building will be stored as key value pairs via the API. Below is a sample data set used in the POST API method.

```
{
    "data": {
        "Telephone": "TP1234516",
        "Chair": "CHR12345",
        "Desk": "DSK2123"
    }
}
```

The keys which in this case are "Telephone", "Chair" and "Desk". The values are the asset tracking numbers.