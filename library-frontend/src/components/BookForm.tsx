// File: components/BookForm.tsx
// Form component for adding new books to the library
// Includes validation, loading states, and success/error messages

import { useState } from "react";
import { createBook } from "../api/booksApi";
import { useToast } from "../context/ToastContext";
import { Plus, Loader, AlertCircle } from "lucide-react";

// TypeScript interface for validation errors
interface FormErrors {
  title?: string;
  author?: string;
}

const BookForm = ({ refresh }: { refresh: () => void }) => {
  const { showToast } = useToast();

  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [description, setDescription] = useState("");

  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<FormErrors>({});

  const validate = (): boolean => {
    const newErrors: FormErrors = {};
    if (!title.trim()) newErrors.title = "Title is required.";
    if (!author.trim()) newErrors.author = "Author is required.";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validate()) return;

    setIsLoading(true);
    try {
      await createBook({ 
        title: title.trim(), 
        author: author.trim(), 
        description: description.trim() 
      });
      
      showToast("Book added successfully!", "success");
      
      setTitle("");
      setAuthor("");
      setDescription("");
      setErrors({});
      refresh();
    } catch {
      showToast("Failed to add book. Please try again.", "error");
    } finally {
      setIsLoading(false);
    }
  };

  const inputClass = (hasError?: string) =>
    `w-full bg-gray-50 border rounded-xl px-4 py-2.5 text-sm text-gray-800 placeholder-gray-400
     focus:outline-none focus:bg-white focus:ring-2 transition
     ${hasError
       ? "border-red-400 focus:border-red-400 focus:ring-red-100"
       : "border-gray-200 focus:border-indigo-400 focus:ring-indigo-100"
     }`;

  return (
    <div className="bg-white rounded-2xl border border-gray-200 shadow-sm overflow-hidden">
      <div className="px-6 py-4 border-b border-gray-100 flex items-center gap-2">
        <div className="w-2 h-2 rounded-full bg-indigo-500" />
        <h2 className="text-sm font-semibold text-gray-700">Add New Book</h2>
      </div>

      <form onSubmit={handleSubmit} noValidate className="px-6 py-5 flex flex-col gap-4">
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          
          <div className="flex flex-col gap-1.5">
            <label className="text-xs font-medium text-gray-500">
              Title <span className="text-red-400">*</span>
            </label>
            <input
              className={inputClass(errors.title)}
              placeholder="e.g. The Pragmatic Programmer"
              value={title}
              onChange={(e) => {
                setTitle(e.target.value);
                if (errors.title) setErrors((prev) => ({ ...prev, title: undefined }));
              }}
            />
            {errors.title && (
              <p className="text-xs text-red-500 flex items-center gap-1">
                <AlertCircle className="w-3 h-3" />
                {errors.title}
              </p>
            )}
          </div>

          <div className="flex flex-col gap-1.5">
            <label className="text-xs font-medium text-gray-500">
              Author <span className="text-red-400">*</span>
            </label>
            <input
              className={inputClass(errors.author)}
              placeholder="e.g. Andrew Hunt"
              value={author}
              onChange={(e) => {
                setAuthor(e.target.value);
                if (errors.author) setErrors((prev) => ({ ...prev, author: undefined }));
              }}
            />
            {errors.author && (
              <p className="text-xs text-red-500 flex items-center gap-1">
                <AlertCircle className="w-3 h-3" />
                {errors.author}
              </p>
            )}
          </div>
        </div>

        <div className="flex flex-col gap-1.5">
          <label className="text-xs font-medium text-gray-500">Description</label>
          <textarea
            className={inputClass()}
            placeholder="Brief description... (optional)"
            rows={3}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
        </div>

        <div className="flex justify-end pt-1">
          <button
            type="submit"
            disabled={isLoading}
            className="inline-flex items-center gap-2 bg-indigo-600 hover:bg-indigo-700 disabled:opacity-60 disabled:cursor-not-allowed
                       text-white text-sm font-medium px-5 py-2.5 rounded-xl transition-colors duration-150"
          >
            {isLoading ? (
              <>
                <Loader className="w-4 h-4 animate-spin" />
                Adding...
              </>
            ) : (
              <>
                <Plus className="w-4 h-4" />
                Add Book
              </>
            )}
          </button>
        </div>
      </form>
    </div>
  );
};

export default BookForm;