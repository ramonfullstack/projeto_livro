# projeto_livroI'll create a comprehensive README for your project that explains how to run both the .NET API and the Next.js application.

```markdown:d:\Repositorios Github\projeto_livro\README.md
# Book Store Project

This project consists of two parts:
- A .NET API backend (BookStoreApi)
- A Next.js frontend application (book-frontend)

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18 or later)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)
- [MongoDB](https://www.mongodb.com/try/download/community)

## Running the .NET API

1. Navigate to the API project directory:

```bash
cd d:\Repositorios Github\projeto_livro\BookStoreApi
```

2. Restore the required packages:

```bash
dotnet restore
```

3. Build the project:

```bash
dotnet build
```

4. Run the API:

```bash
dotnet run --project BookStoreApi
```

The API will start running at `http://localhost:5213`.

### API Endpoints

- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get a specific book by ID
- `POST /api/books` - Create a new book
- `PUT /api/books/{id}` - Update an existing book
- `DELETE /api/books/{id}` - Delete a book

## Running the Next.js Frontend

1. Navigate to the frontend project directory:

```bash
cd d:\Repositorios Github\projeto_livro\book-frontend
```

2. Install the required dependencies:

```bash
npm install
```

3. Run the development server:

```bash
npm run dev
```

The frontend application will start running at `http://localhost:3000`.

## Testing

### Testing the API

1. Navigate to the API test project directory:

```bash
cd d:\Repositorios Github\projeto_livro\BookStoreApi.Tests
```

2. Run the tests:

```bash
dotnet test
```

### Testing the Frontend

1. Navigate to the frontend project directory:

```bash
cd d:\Repositorios Github\projeto_livro\book-frontend
```

2. Run the tests:

```bash
npm test
```

## Project Structure

### Backend (.NET API)

- `BookStoreApi/Controllers/` - API controllers
- `BookStoreApi/Models/` - Data models
- `BookStoreApi/Services/` - Business logic and data access

### Frontend (Next.js)

- `book-frontend/src/app/` - Next.js application
- `book-frontend/src/app/lib/` - API client and utilities
- `book-frontend/src/app/books/` - Book-related pages
- `book-frontend/src/app/types/` - TypeScript type definitions

## Database Configuration

The API connects to a MongoDB database. Make sure MongoDB is running locally or update the connection string in `appsettings.json` to point to your MongoDB instance.
```

This README provides clear instructions for running both parts of your application, testing them, and understanding the project structure.