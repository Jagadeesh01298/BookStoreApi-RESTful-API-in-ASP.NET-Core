# Book Store RESTful API

## Project Overview

This project is a RESTful API built using ASP.NET Core Web API for an online book store.  
It allows users to perform CRUD operations on books and authors.

## Technologies Used

- ASP.NET Core Web API
- C#
- Entity Framework Core
- In-Memory Database
- Swagger
- Postman
- Fiddler

## Features

- Add, view, update, and delete books
- Add, view, update, and delete authors
- Get all books by a specific author
- Basic validation
- Proper HTTP status codes
- RESTful routing

## API Endpoints

### Books

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/books` | Get all books |
| GET | `/api/books/{id}` | Get book by ID |
| POST | `/api/books` | Add new book |
| PUT | `/api/books/{id}` | Update book |
| DELETE | `/api/books/{id}` | Delete book |

### Authors

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/authors` | Get all authors |
| GET | `/api/authors/{id}` | Get author by ID |
| POST | `/api/authors` | Add new author |
| PUT | `/api/authors/{id}` | Update author |
| DELETE | `/api/authors/{id}` | Delete author |
| GET | `/api/authors/{authorId}/books` | Get books by author |

## Sample Book JSON

```json
{
  "title": "The Alchemist",
  "publicationYear": 1988,
  "authorId": 1
}
