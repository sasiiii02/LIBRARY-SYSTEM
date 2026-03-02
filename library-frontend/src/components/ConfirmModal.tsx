// File: components/ConfirmModal.tsx
// Reusable modal that asks user to confirm before deleting a book

import { AlertCircle } from "lucide-react";

interface ConfirmModalProps {
  isOpen: boolean;
  bookTitle: string;
  onConfirm: () => void;
  onCancel: () => void;
}

const ConfirmModal = ({ isOpen, bookTitle, onConfirm, onCancel }: ConfirmModalProps) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-sm">
      <div className="bg-white rounded-2xl shadow-xl w-full max-w-sm mx-4 p-6">
        
        <div className="w-11 h-11 bg-red-100 rounded-xl flex items-center justify-center mb-4">
          <AlertCircle className="w-5 h-5 text-red-500" />
        </div>

        <h3 className="text-base font-semibold text-gray-900 mb-1">Delete Book</h3>
        <p className="text-sm text-gray-500 mb-6">
          Are you sure you want to delete{" "}
          <span className="font-medium text-gray-700">"{bookTitle}"</span>?
          This action cannot be undone.
        </p>

        <div className="flex gap-2 justify-end">
          <button
            onClick={onCancel}
            className="px-4 py-2 text-sm font-medium text-gray-600 bg-gray-100 hover:bg-gray-200 rounded-xl transition-colors duration-150"
          >
            Cancel
          </button>
          <button
            onClick={onConfirm}
            className="px-4 py-2 text-sm font-medium text-white bg-red-500 hover:bg-red-600 rounded-xl transition-colors duration-150"
          >
            Yes, Delete
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmModal;