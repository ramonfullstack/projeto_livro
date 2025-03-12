import Link from 'next/link';

export default function Header() {
  return (
    <header className="bg-white shadow">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16 items-center">
          <Link href="/" className="text-xl font-bold text-indigo-600">
            Book Store
          </Link>
          <nav className="flex space-x-4">
            <Link href="/" className="text-gray-700 hover:text-indigo-600">
              Home
            </Link>
            <Link href="/books/new" className="text-gray-700 hover:text-indigo-600">
              Add Book
            </Link>
          </nav>
        </div>
      </div>
    </header>
  );
}