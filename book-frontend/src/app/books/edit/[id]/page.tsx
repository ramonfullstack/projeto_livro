import Link from 'next/link';
import { notFound } from 'next/navigation';
import { fetchBookById } from '../../../lib/api';

export default async function EditBook({ params }: { params: { id: string } }) {
  const book = await fetchBookById(params.id);
  
  if (!book) {
    notFound();
  }

  return (
    <div className="max-w-4xl mx-auto">
      <Link 
        href="/"
        className="inline-flex items-center text-blue-600 hover:text-blue-800 mb-6"
      >
        ← Back to Books
      </Link>
      
      <div className="bg-white shadow-sm rounded-lg overflow-hidden border border-gray-200">
        <div className="px-6 py-5 border-b border-gray-200 bg-gray-50">
          <h1 className="text-2xl font-bold text-gray-900">Edit Book: {book.name}</h1>
        </div>
        
        <div className="px-6 py-5">
          <form className="space-y-6">
          <div>
              <label htmlFor="name" className="block text-sm font-medium text-gray-700 mb-1">
                Book Name
              </label>
              <input
                type="text"
                id="name"
                name="name"
                defaultValue={book.name}
                className="form-input text-gray-900"
                required
              />
            </div>
            
            <div>
              <label htmlFor="sku" className="block text-sm font-medium text-gray-700 mb-1">
                SKU
              </label>
              <input
                type="text"
                id="sku"
                name="sku"
                defaultValue={book.sku}
                className="form-input text-gray-900"
                required
              />
            </div>
            
            <div>
              <label htmlFor="description" className="block text-sm font-medium text-gray-700 mb-1">
                Description
              </label>
              <textarea
                id="description"
                name="description"
                rows={4}
                defaultValue={book.description}
                className="form-input text-gray-900"
              />
            </div>
            
            <div>
              <label htmlFor="price" className="block text-sm font-medium text-gray-700 mb-1">
                Price
              </label>
              <input
                type="number"
                id="price"
                name="price"
                defaultValue={book.price}
                step="0.01"
                min="0"
                className="form-input text-gray-900"
                required
              />
            </div>
            
            <div className="flex justify-end space-x-3">
              <Link
                href={`/books/${book.id}`}
                className="btn btn-outline"
              >
                Cancel
              </Link>
              <button
                type="submit"
                className="btn btn-primary"
              >
                Save Changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}