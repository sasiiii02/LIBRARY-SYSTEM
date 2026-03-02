// File: App.tsx
// Root component of the application
// Sets up layout, navbar, and wraps everything with ToastProvider

import { useState } from "react";
import BookList from "./components/BookList";
import BookForm from "./components/BookForm";
import { ToastProvider } from "./context/ToastContext";
import { BookOpen } from "lucide-react";

function App() {
  const [refreshKey, setRefreshKey] = useState(0);
  const refresh = () => setRefreshKey((old) => old + 1);

  return (
    <ToastProvider>
      <div className="min-h-screen bg-[#F7F8FA]">

        <nav className="bg-white border-b border-gray-200 sticky top-0 z-20">
          <div className="max-w-4xl mx-auto px-6 h-14 flex items-center justify-between">
            
            <div className="flex items-center gap-2">
              <div className="w-7 h-7 bg-indigo-600 rounded-lg flex items-center justify-center">
                <BookOpen className="w-4 h-4 text-white" />
              </div>
              <span className="font-semibold text-gray-900 text-sm">LibraryOS</span>
            </div>
            
            <span className="text-xs text-gray-400 font-medium">Book Manager</span>
          </div>
        </nav>

        <div className="bg-white border-b border-gray-200">
          <div className="max-w-4xl mx-auto px-6 py-8">
            <h1 className="text-2xl font-bold text-gray-900">Your Library</h1>
            <p className="text-sm text-gray-500 mt-1">Add, edit and manage your book collection.</p>
          </div>
        </div>

        <div className="max-w-4xl mx-auto px-6 py-8 flex flex-col gap-8">
          <BookForm refresh={refresh} />
          <div key={refreshKey}>
            <BookList />
          </div>
        </div>

      </div>
    </ToastProvider>
  );
}

export default App;