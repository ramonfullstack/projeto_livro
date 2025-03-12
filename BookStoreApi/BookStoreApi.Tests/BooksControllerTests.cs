using BookStoreApi.Controllers;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreApi.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<BookService> _mockBookService;
        private readonly BooksController _controller;
        private readonly List<Book> _books;

        public BooksControllerTests()
        {
            _mockBookService = new Mock<BookService>();
            _controller = new BooksController(_mockBookService.Object);

            _books = new List<Book>
            {
                new Book { Id = "1", Name = "Test Book 1", SKU = "TB001", Description = "Test Description 1", Price = 19.99m },
                new Book { Id = "2", Name = "Test Book 2", SKU = "TB002", Description = "Test Description 2", Price = 29.99m }
            };
        }

        [Fact]
        public async Task Get_ReturnsAllBooks()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetAsync())
                .ReturnsAsync(_books);

            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Book>>>(result);
            var returnValue = Assert.IsType<List<Book>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsBook()
        {
            // Arrange
            var bookId = "1";
            var book = _books[0];
            _mockBookService.Setup(service => service.GetAsync(bookId))
                .ReturnsAsync(book);

            // Act
            var result = await _controller.Get(bookId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var returnValue = Assert.IsType<Book>(actionResult.Value);
            Assert.Equal(bookId, returnValue.Id);
            Assert.Equal(book.Name, returnValue.Name);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var bookId = "999";
            _mockBookService.Setup(service => service.GetAsync(bookId))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Get(bookId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Get_WithEmptyId_ReturnsBadRequest()
        {
            // Arrange
            string bookId = "";

            // Act
            var result = await _controller.Get(bookId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtRoute()
        {
            // Arrange
            var newBook = new Book { Name = "New Book", SKU = "NB001", Description = "New Description", Price = 39.99m };
            var createdBook = new Book { Id = "3", Name = "New Book", SKU = "NB001", Description = "New Description", Price = 39.99m };
            
            _mockBookService.Setup(service => service.CreateAsync(newBook))
                .Callback(() => newBook.Id = "3")
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newBook);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
            Assert.Equal("GetBook", createdAtRouteResult.RouteName);
            Assert.Equal("3", createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var bookId = "1";
            var bookToUpdate = new Book { Id = bookId, Name = "Updated Book", SKU = "UB001", Description = "Updated Description", Price = 49.99m };
            
            _mockBookService.Setup(service => service.GetAsync(bookId))
                .ReturnsAsync(_books[0]);
            
            _mockBookService.Setup(service => service.UpdateAsync(bookId, bookToUpdate))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(bookId, bookToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var bookId = "999";
            var bookToUpdate = new Book { Id = bookId, Name = "Updated Book", SKU = "UB001", Description = "Updated Description", Price = 49.99m };
            
            _mockBookService.Setup(service => service.GetAsync(bookId))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Update(bookId, bookToUpdate);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_WithEmptyId_ReturnsBadRequest()
        {
            // Arrange
            string bookId = "";
            var bookToUpdate = new Book { Name = "Updated Book", SKU = "UB001", Description = "Updated Description", Price = 49.99m };

            // Act
            var result = await _controller.Update(bookId, bookToUpdate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var bookId = "1";
            
            _mockBookService.Setup(service => service.GetAsync(bookId))
                .ReturnsAsync(_books[0]);
            
            _mockBookService.Setup(service => service.RemoveAsync(bookId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(bookId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var bookId = "999";
            
            _mockBookService.Setup(service => service.GetAsync(bookId))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Delete(bookId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}