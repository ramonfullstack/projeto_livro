import BookCard from './components/BookCard';
import { fetchBooks } from './lib/api';

export default async function Home() {
  const books = await fetchBooks();

  return (
    <div>
      <div className="mb-6 flex justify-between items-center">
        <h1 className="text-3xl font-bold text-gray-900">All Books</h1>
      </div>

      {books.length === 0 ? (
        <div className="text-center py-10">
          <p className="text-gray-500 mb-4">No books found.</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {books.map(book => (
            <BookCard key={book.id} book={book} />
          ))}
        </div>
      )}
    </div>
  );
}