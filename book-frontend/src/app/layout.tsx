import './globals.css';
import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import Link from 'next/link';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
  title: 'Book Store',
  description: 'A modern book store application',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <div className="min-h-screen flex flex-col">
          {/* Header */}
          <header className="bg-white shadow-md">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
              <div className="flex justify-between h-16 items-center">
                <div className="flex-shrink-0">
                  <Link href="/" className="text-2xl font-bold text-primary-600 hover:text-primary-700">
                    BookStore
                  </Link>
                </div>
                <nav className="flex items-center space-x-8">
                  <Link href="/" className="text-gray-700 hover:text-primary-600 font-medium">
                    Home
                  </Link>
                  <Link href="/books/new" className="text-gray-700 hover:text-primary-600 font-medium">
                    Add Book
                  </Link>
                  <Link 
                    href="/books/new" 
                    className="bg-primary-600 hover:bg-primary-700 text-white px-4 py-2 rounded-md font-medium transition-colors"
                  >
                    New Book
                  </Link>
                </nav>
              </div>
            </div>
          </header>

          {/* Main content */}
          <main className="flex-grow">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
              {children}
            </div>
          </main>

          {/* Footer */}
          <footer className="bg-gray-50 border-t border-gray-200">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
              <div className="flex flex-col md:flex-row justify-between items-center">
                <div className="mb-4 md:mb-0">
                  <p className="text-gray-500">© {new Date().getFullYear()} BookStore. All rights reserved.</p>
                </div>
                <div className="flex space-x-6">
                  <a href="#" className="text-gray-500 hover:text-gray-700">
                    Terms
                  </a>
                  <a href="#" className="text-gray-500 hover:text-gray-700">
                    Privacy
                  </a>
                  <a href="#" className="text-gray-500 hover:text-gray-700">
                    Contact
                  </a>
                </div>
              </div>
            </div>
          </footer>
        </div>
      </body>
    </html>
  );
}