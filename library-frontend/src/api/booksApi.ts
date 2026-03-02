// api/booksApi.ts
// Handles all API calls to the backend for book operations

import axios from "axios";
import type { Book } from "../types/Book";

// Backend API URL - make sure this matches your running backend
const API_URL = "https://localhost:7226/api/books";

// Fetch all books from database
export const getBooks = async (): Promise<Book[]> => {
  const response = await axios.get(API_URL);
  return response.data;
};

// Add a new book
export const createBook = async (book: Omit<Book, "id">) => {
  return axios.post(API_URL, book);
};

// Update an existing book by id
export const updateBook = async (id: number, book: Omit<Book, "id">) => {
  return axios.put(`${API_URL}/${id}`, book);
};

// Delete a book by id
export const deleteBook = async (id: number) => {
  return axios.delete(`${API_URL}/${id}`);
};