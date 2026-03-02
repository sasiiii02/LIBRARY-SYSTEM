// File: components/BookList.tsx
// Displays all books from the database
// Handles search, edit, delete, and shows loading states

import { useEffect, useState } from "react";
import { getBooks, deleteBook, updateBook } from "../api/booksApi";
import { useToast } from "../context/ToastContext";
import ConfirmModal from "./ConfirmModal";
import type { Book } from "../types/Book";
import { 
  Search, 
  X, 
  Pencil, 
  Trash2, 
  Loader,
  BookOpen,
  AlertCircle
} from "lucide-react";

interface EditErrors {
  title?: string;
  author?: string;
}

const BookList = () => {
  const { showToast } = useToast();

  const [books, setBooks] = useState<Book[]>([]);
  const [isFetching, setIsFetching] = useState(true);
  const [search, setSearch] = useState("");

  const [editingId, setEditingId] = useState<number | null>(null);
  const [editData, setEditData] = useState({ title: "", author: "", description: "" });
  const [editErrors, setEditErrors] = useState<EditErrors>({});
  const [isSaving, setIsSaving] = useState(false);

  const [deleteTarget, setDeleteTarget] = useState<Book | null>(null);
  const [isDeleting, setIsDeleting] = useState(false);

  const fetchBooks = async () => {
    setIsFetching(true);
    try {
      const data = await getBooks();
      setBooks(data);
    } catch {
      showToast("Failed to load books.", "error");
    } finally {
      setIsFetching(false);
    }
  };

  useEffect(() => { fetchBooks(); }, []);

  const filteredBooks = books.filter(
    (b) =>
      b.title.toLowerCase().includes(search.toLowerCase()) ||
      b.author.toLowerCase().includes(search.toLowerCase())
  );

  const handleDeleteClick = (book: Book) => setDeleteTarget(book);

  const handleDeleteConfirm = async () => {
    if (!deleteTarget) return;
    setIsDeleting(true);
    try {
      await deleteBook(deleteTarget.id);
      showToast(`"${deleteTarget.title}" deleted.`, "success");
      setDeleteTarget(null);
      fetchBooks();
    } catch {
      showToast("Failed to delete book.", "error");
    } finally {
      setIsDeleting(false);
    }
  };

  const startEdit = (book: Book) => {
    setEditingId(book.id);
    setEditData({ 
      title: book.title, 
      author: book.author, 
      description: book.description 
    });
    setEditErrors({});
  };

  const validateEdit = (): boolean => {
    const errs: EditErrors = {};
    if (!editData.title.trim()) errs.title = "Title is required.";
    if (!editData.author.trim()) errs.author = "Author is required.";
    setEditErrors(errs);
    return Object.keys(errs).length === 0;
  };

  const handleUpdate = async (id: number) => {
    if (!validateEdit()) return;
    
    setIsSaving(true);
    try {
      await updateBook(id, {
        title: editData.title.trim(),
        author: editData.author.trim(),
        description: editData.description.trim(),
      });
      showToast("Book updated successfully!", "success");
      setEditingId(null);
      fetchBooks();
    } catch {
      showToast("Failed to update book.", "error");
    } finally {
      setIsSaving(false);
    }
  };

  const inputClass = (hasError?: string) =>
    `w-full bg-white border rounded-xl px-4 py-2.5 text-sm text-gray-800
     focus:outline-none focus:ring-2 transition
     ${hasError
       ? "border-red-400 focus:border-red-400 focus:ring-red-100"
       : "border-gray-200 focus:border-indigo-400 focus:ring-indigo-100"
     }`;

  if (isFetching) {
    return (
      <div className="flex flex-col gap-2">
        {[1, 2, 3].map((i) => (
          <div key={i} className="bg-white rounded-2xl border border-gray-200 p-5 animate-pulse">
            <div className="flex items-start gap-4">
              <div className="w-10 h-10 bg-gray-100 rounded-xl shrink-0" />
              <div className="flex-1 space-y-2">
                <div className="h-3.5 bg-gray-100 rounded w-1/3" />
                <div className="h-3 bg-gray-100 rounded w-1/5" />
                <div className="h-3 bg-gray-100 rounded w-2/3" />
              </div>
            </div>
          </div>
        ))}
      </div>
    );
  }

  return (
    <>
      <ConfirmModal
        isOpen={!!deleteTarget}
        bookTitle={deleteTarget?.title ?? ""}
        onConfirm={handleDeleteConfirm}
        onCancel={() => setDeleteTarget(null)}
      />

      <div className="flex flex-col gap-3">
        <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3 px-1">
          
          <div className="flex items-center gap-2">
            <h2 className="text-sm font-semibold text-gray-700">Collection</h2>
            <span className="text-xs text-gray-400 bg-gray-100 px-2.5 py-1 rounded-full font-medium">
              {filteredBooks.length} {filteredBooks.length === 1 ? "book" : "books"}
              {search && books.length !== filteredBooks.length && (
                <span className="text-gray-400"> of {books.length}</span>
              )}
            </span>
          </div>

          <div className="relative">
            <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-gray-400" />
            <input
              className="pl-9 pr-4 py-2 bg-white border border-gray-200 rounded-xl text-sm text-gray-700 placeholder-gray-400
                         focus:outline-none focus:border-indigo-400 focus:ring-2 focus:ring-indigo-100 transition w-full sm:w-56"
              placeholder="Search by title or author..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
            {search && (
              <button
                onClick={() => setSearch("")}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
              >
                <X className="w-3.5 h-3.5" />
              </button>
            )}
          </div>
        </div>

        {filteredBooks.length === 0 && (
          <div className="bg-white rounded-2xl border border-dashed border-gray-300 flex flex-col items-center justify-center py-16 text-center">
            <div className="w-12 h-12 bg-gray-100 rounded-2xl flex items-center justify-center mb-4">
              <BookOpen className="w-6 h-6 text-gray-400" />
            </div>
            {search ? (
              <>
                <p className="text-sm font-medium text-gray-600">No results for "{search}"</p>
                <p className="text-xs text-gray-400 mt-1">Try a different title or author.</p>
              </>
            ) : (
              <>
                <p className="text-sm font-medium text-gray-600">No books yet</p>
                <p className="text-xs text-gray-400 mt-1">Add your first book using the form above.</p>
              </>
            )}
          </div>
        )}

        {filteredBooks.map((book) => (
          <div key={book.id} className="bg-white rounded-2xl border border-gray-200 shadow-sm overflow-hidden">

            {editingId === book.id ? (
              <div className="p-5">
                <div className="flex items-center gap-2 mb-4">
                  <div className="w-2 h-2 rounded-full bg-amber-400" />
                  <span className="text-xs font-semibold text-gray-500 uppercase tracking-wide">Editing</span>
                </div>

                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 mb-3">
                  <div className="flex flex-col gap-1.5">
                    <label className="text-xs font-medium text-gray-500">
                      Title <span className="text-red-400">*</span>
                    </label>
                    <input
                      className={inputClass(editErrors.title)}
                      value={editData.title}
                      onChange={(e) => {
                        setEditData({ ...editData, title: e.target.value });
                        if (editErrors.title) setEditErrors((p) => ({ ...p, title: undefined }));
                      }}
                    />
                    {editErrors.title && (
                      <p className="text-xs text-red-500 flex items-center gap-1">
                        <AlertCircle className="w-3 h-3" />
                        {editErrors.title}
                      </p>
                    )}
                  </div>

                  <div className="flex flex-col gap-1.5">
                    <label className="text-xs font-medium text-gray-500">
                      Author <span className="text-red-400">*</span>
                    </label>
                    <input
                      className={inputClass(editErrors.author)}
                      value={editData.author}
                      onChange={(e) => {
                        setEditData({ ...editData, author: e.target.value });
                        if (editErrors.author) setEditErrors((p) => ({ ...p, author: undefined }));
                      }}
                    />
                    {editErrors.author && (
                      <p className="text-xs text-red-500 flex items-center gap-1">
                        <AlertCircle className="w-3 h-3" />
                        {editErrors.author}
                      </p>
                    )}
                  </div>
                </div>

                <div className="flex flex-col gap-1.5 mb-4">
                  <label className="text-xs font-medium text-gray-500">Description</label>
                  <textarea
                    className={inputClass()}
                    rows={3}
                    value={editData.description}
                    onChange={(e) => setEditData({ ...editData, description: e.target.value })}
                  />
                </div>

                <div className="flex gap-2 justify-end">
                  <button
                    onClick={() => setEditingId(null)}
                    disabled={isSaving}
                    className="px-4 py-2 text-sm font-medium text-gray-600 bg-gray-100 hover:bg-gray-200 rounded-xl transition-colors duration-150 disabled:opacity-50"
                  >
                    Cancel
                  </button>
                  <button
                    onClick={() => handleUpdate(book.id)}
                    disabled={isSaving}
                    className="inline-flex items-center gap-2 px-4 py-2 text-sm font-medium text-white bg-emerald-600 hover:bg-emerald-700 rounded-xl transition-colors duration-150 disabled:opacity-60"
                  >
                    {isSaving ? (
                      <>
                        <Loader className="w-4 h-4 animate-spin" />
                        Saving...
                      </>
                    ) : "Save Changes"}
                  </button>
                </div>
              </div>

            ) : (
              <div className="flex items-start gap-4 p-5">
                <div className="w-10 h-10 rounded-xl bg-indigo-50 flex items-center justify-center shrink-0 mt-0.5">
                  <BookOpen className="w-5 h-5 text-indigo-400" />
                </div>

                <div className="flex-1 min-w-0">
                  <h3 className="text-sm font-semibold text-gray-900 truncate">{book.title}</h3>
                  <p className="text-xs text-indigo-500 font-medium mt-0.5">{book.author}</p>
                  {book.description && (
                    <p className="text-xs text-gray-500 mt-1.5 leading-relaxed line-clamp-2">
                      {book.description}
                    </p>
                  )}
                </div>

                <div className="flex items-center gap-1 shrink-0">
                  <button
                    onClick={() => startEdit(book)}
                    className="p-2 text-gray-400 hover:text-indigo-600 hover:bg-indigo-50 rounded-lg transition-colors duration-150"
                    title="Edit book"
                  >
                    <Pencil className="w-4 h-4" />
                  </button>
                  <button
                    onClick={() => handleDeleteClick(book)}
                    disabled={isDeleting}
                    className="p-2 text-gray-400 hover:text-red-500 hover:bg-red-50 rounded-lg transition-colors duration-150 disabled:opacity-50"
                    title="Delete book"
                  >
                    <Trash2 className="w-4 h-4" />
                  </button>
                </div>
              </div>
            )}
          </div>
        ))}
      </div>
    </>
  );
};

export default BookList;