import { Book, BookFormData } from '../types/book';

const API_URL = 'http://localhost:5213/api'; // Update with your actual API URL

export async function fetchBooks(): Promise<Book[]> {
  try {
    const response = await fetch(`${API_URL}/books`, { 
      cache: 'no-store' // For SSR
    });
    if (!response.ok) {
      throw new Error('Failed to fetch books');
    }
    return await response.json();
  } catch (error) {
    console.error('Error fetching books:', error);
    return [];
  }
}

export async function fetchBookById(id: string): Promise<Book | null> {
  try {
    const response = await fetch(`${API_URL}/books/${id}`, {
      next: { revalidate: 60 } // For ISR (Incremental Static Regeneration)
    });
    if (!response.ok) {
      throw new Error('Failed to fetch book');
    }
    return await response.json();
  } catch (error) {
    console.error(`Error fetching book with id ${id}:`, error);
    return null;
  }
}

export async function createBook(bookData: BookFormData): Promise<Book | null> {
  try {
    const response = await fetch(`${API_URL}/books`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(bookData),
    });
    if (!response.ok) {
      throw new Error('Failed to create book');
    }
    return await response.json();
  } catch (error) {
    console.error('Error creating book:', error);
    return null;
  }
}

export async function updateBook(id: string, bookData: BookFormData): Promise<boolean> {
  try {
    const response = await fetch(`${API_URL}/books/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ ...bookData }),
    });
    return response.ok;
  } catch (error) {
    console.error(`Error updating book with id ${id}:`, error);
    return false;
  }
}

export async function deleteBook(id: string): Promise<boolean> {
  try {
    const response = await fetch(`${API_URL}/books/${id}`, {
      method: 'DELETE',
    });
    return response.ok;
  } catch (error) {
    console.error(`Error deleting book with id ${id}:`, error);
    return false;
  }
}