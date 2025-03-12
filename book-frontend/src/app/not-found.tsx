import Link from 'next/link';

export default function NotFound() {
  return (
    <div className="text-center py-10">
      <h1 className="text-2xl font-bold text-gray-900 mb-4">Not Found</h1>
      <p className="text-gray-500 mb-6">The resource you&apos;re looking for doesn&apos;t exist or has been removed.</p>
      <Link 
        href="/"
        className="inline-block bg-indigo-600 text-white px-4 py-2 rounded hover:bg-indigo-700"
      >
        Back to Home
      </Link>
    </div>
  );
}