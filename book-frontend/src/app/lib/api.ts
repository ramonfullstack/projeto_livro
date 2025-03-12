import { Book, BookFormData } from "../types/book";

const API_URL = "http://127.0.0.1:5213/api"; // Update with your actual API URL

export async function fetchBooks(): Promise<Book[]> {
  try {
    const response = await fetch(`${API_URL}/books`, {
      cache: "no-store", // For SSR
    });
    if (!response.ok) {
      throw new Error("Failed to fetch books");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching books:", error);
    return [];
  }
}

export async function fetchBookById(id: string): Promise<Book | null> {
  try {
    console.log(`Fetching book with ID: ${id}`);

    if (!id) {
      console.error("Invalid book ID: ID is empty or undefined");
      return null;
    }

    const url = `${API_URL}/books/${id}`;
    console.log(`Request URL: ${url}`);

    const response = await fetch(url, {
      next: { revalidate: 60 },
      cache: "no-store",
    });

    if (!response.ok) {
      const errorText = await response.text();
      console.error(`API error (${response.status}): ${errorText}`);
      throw new Error(
        `Failed to fetch book: ${response.status} ${response.statusText}`
      );
    }

    const data = await response.json();
    console.log("Book data received:", data);
    return data;
  } catch (error) {
    console.error(`Error fetching book with id ${id}:`, error);
    return null;
  }
}

export async function createBook(bookData: BookFormData): Promise<Book | null> {
  try {
    const response = await fetch(`${API_URL}/books`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(bookData),
    });
    if (!response.ok) {
      throw new Error("Failed to create book");
    }
    return await response.json();
  } catch (error) {
    console.error("Error creating book:", error);
    return null;
  }
}

export async function updateBook(
  id: string,
  bookData: BookFormData
): Promise<boolean> {
  try {
    const response = await fetch(`${API_URL}/books/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
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
      method: "DELETE",
    });
    return response.ok;
  } catch (error) {
    console.error(`Error deleting book with id ${id}:`, error);
    return false;
  }
}
