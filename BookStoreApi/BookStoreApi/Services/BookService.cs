using BookStoreApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public async Task<List<Book>> GetAsync() =>
            await _books.Find(book => true).ToListAsync();

        public async Task<Book> GetAsync(string id) =>
            await _books.Find(book => book.Id == id).FirstOrDefaultAsync();

        public async Task<Book> CreateAsync(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task UpdateAsync(string id, Book bookIn) =>
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);

        public async Task RemoveAsync(string id) =>
            await _books.DeleteOneAsync(book => book.Id == id);
    }
}