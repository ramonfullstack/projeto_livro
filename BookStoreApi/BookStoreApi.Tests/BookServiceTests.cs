using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreApi.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IMongoCollection<Book>> _mockCollection;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            var mockBookstoreDatabaseSettings = new Mock<IOptions<BookStoreDatabaseSettings>>();
            mockBookstoreDatabaseSettings.Setup(x => x.Value).Returns(new BookStoreDatabaseSettings
            {
                BooksCollectionName = "Books",
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "BookStore"
            });

            _mockCollection = new Mock<IMongoCollection<Book>>();
            
            var mockMongoDatabase = new Mock<IMongoDatabase>();
            mockMongoDatabase.Setup(x => x.GetCollection<Book>("Books", null))
                .Returns(_mockCollection.Object);
            
            var mockMongoClient = new Mock<IMongoClient>();
            mockMongoClient.Setup(x => x.GetDatabase("BookStore", null))
                .Returns(mockMongoDatabase.Object);

            _bookService = new BookService(mockBookstoreDatabaseSettings.Object)
            {
                // Use reflection to set the private _booksCollection field
                GetType().GetField("_booksCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(_bookService, _mockCollection.Object)
            };
        }

        [Fact]
        public async Task GetAsync_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = "1", Name = "Test Book 1", SKU = "TB001", Description = "Test Description 1", Price = 19.99m },
                new Book { Id = "2", Name = "Test Book 2", SKU = "TB002", Description = "Test Description 2", Price = 29.99m }
            };

            var mockAsyncCursor = new Mock<IAsyncCursor<Book>>();
            mockAsyncCursor.Setup(x => x.Current).Returns(books);
            mockAsyncCursor
                .SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<Book>>(),
                It.IsAny<FindOptions<Book, Book>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _bookService.GetAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Test Book 1", result[0].Name);
            Assert.Equal("Test Book 2", result[1].Name);
        }

        [Fact]
        public async Task GetAsync_WithId_ReturnsBook()
        {
            // Arrange
            var bookId = "1";
            var book = new Book { Id = bookId, Name = "Test Book 1", SKU = "TB001", Description = "Test Description 1", Price = 19.99m };

            var mockAsyncCursor = new Mock<IAsyncCursor<Book>>();
            mockAsyncCursor.Setup(x => x.Current).Returns(new List<Book> { book });
            mockAsyncCursor
                .SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<Book>>(),
                It.IsAny<FindOptions<Book, Book>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _bookService.GetAsync(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal("Test Book 1", result.Name);
        }

        [Fact]
        public async Task CreateAsync_CallsInsertOne()
        {
            // Arrange
            var book = new Book { Name = "New Book", SKU = "NB001", Description = "New Description", Price = 39.99m };

            // Act
            await _bookService.CreateAsync(book);

            // Assert
            _mockCollection.Verify(x => x.InsertOneAsync(
                It.Is<Book>(b => b.Name == "New Book"),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsReplaceOne()
        {
            // Arrange
            var bookId = "1";
            var book = new Book { Id = bookId, Name = "Updated Book", SKU = "UB001", Description = "Updated Description", Price = 49.99m };

            // Act
            await _bookService.UpdateAsync(bookId, book);

            // Assert
            _mockCollection.Verify(x => x.ReplaceOneAsync(
                It.IsAny<FilterDefinition<Book>>(),
                It.Is<Book>(b => b.Name == "Updated Book"),
                It.IsAny<ReplaceOptions>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_CallsDeleteOne()
        {
            // Arrange
            var bookId = "1";

            // Act
            await _bookService.RemoveAsync(bookId);

            // Assert
            _mockCollection.Verify(x => x.DeleteOneAsync(
                It.IsAny<FilterDefinition<Book>>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}