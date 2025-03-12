import Link from 'next/link';
import { notFound } from 'next/navigation';
import { fetchBookById } from '../../lib/api';

export default async function BookDetails({ params }: { params: { id: string } }) {
  const book = await fetchBookById(params.id);
  
  if (!book) {
    notFound();
  }

  return (
    <div className="space-y-6">
      <Link 
        href="/"
        className="inline-flex items-center text-primary-600 hover:text-primary-800"
      >
        ← Back to Books
      </Link>
      
      <div className="bg-white shadow-sm rounded-lg overflow-hidden border border-gray-200">
        <div className="px-6 py-5 border-b border-gray-200 bg-gray-50">
          <div className="flex justify-between items-center">
            <h1 className="text-2xl font-bold text-gray-900">{book.name}</h1>
            <div className="flex space-x-3">
              <Link 
                href={`/books/edit/${book.id}`}
                className="bg-primary-600 hover:bg-primary-700 text-white px-4 py-2 rounded-md font-medium transition-colors"
              >
                Edit Book
              </Link>
            </div>
          </div>
          <p className="mt-1 text-sm text-gray-500">SKU: {book.sku}</p>
        </div>
        
        <div className="px-6 py-5">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            <div className="md:col-span-2">
              <h2 className="text-lg font-medium text-gray-900 mb-3">Description</h2>
              <p className="text-gray-700 whitespace-pre-line">{book.description}</p>
            </div>
            
            <div className="bg-gray-50 p-6 rounded-lg border border-gray-200">
              <h2 className="text-lg font-medium text-gray-900 mb-4">Book Details</h2>
              <div className="space-y-3">
                <div>
                  <p className="text-sm text-gray-500">Price</p>
                  <p className="text-2xl font-bold text-primary-600">${book.price.toFixed(2)}</p>
                </div>
                <div>
                  <p className="text-sm text-gray-500">SKU</p>
                  <p className="font-medium">{book.sku}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}