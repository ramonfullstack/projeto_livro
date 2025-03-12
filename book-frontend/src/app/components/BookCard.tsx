'use client';

import Link from 'next/link';
import { Book } from '../types/book';
import { deleteBook } from '../lib/api';
import { useRouter } from 'next/navigation';

interface BookCardProps {
  book: Book;
}

export default function BookCard({ book }: BookCardProps) {
  const router = useRouter();

  const handleDelete = async () => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      const success = await deleteBook(book.id);
      if (success) {
        router.refresh(); // Refresh the current page
      } else {
        alert('Failed to delete the book. Please try again.');
      }
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow duration-300">
      <div className="p-6">
        <h2 className="text-xl font-bold text-gray-800 mb-2">{book.name}</h2>
        <p className="text-sm text-gray-500 mb-2">SKU: {book.sku}</p>
        <p className="text-gray-700 mb-4 line-clamp-2">{book.description}</p>
        <p className="text-lg font-bold text-indigo-600 mb-4">${book.price.toFixed(2)}</p>
        
        <div className="flex justify-between">
          <Link 
            href={`/books/${book.id}`}
            className="text-indigo-600 hover:text-indigo-800 font-medium"
          >
            View Details
          </Link>
          <div className="space-x-2">
            <Link 
              href={`/books/edit/${book.id}`}
              className="text-blue-600 hover:text-blue-800 font-medium"
            >
              Edit
            </Link>
            <button 
              onClick={handleDelete}
              className="text-red-600 hover:text-red-800 font-medium"
            >
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}