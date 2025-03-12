import BookForm from '../../components/BookForm';

export default function NewBook() {
  return (
    <div className="max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold text-gray-900 mb-6">Add New Book</h1>
      <BookForm />
    </div>
  );
}