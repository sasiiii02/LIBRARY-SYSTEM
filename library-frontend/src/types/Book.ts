// File: types/Book.ts
// TypeScript interface defining the structure of a Book object
// This matches what our backend API returns

export interface Book {
  id: number;          // Unique identifier (database primary key)
  title: string;       // Book title
  author: string;      // Author name
  description: string; // Book description
}