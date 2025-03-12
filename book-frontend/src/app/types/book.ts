export interface Book {
  id: string;
  name: string;
  sku: string;
  description: string;
  price: number;
}

export interface BookFormData {
  name: string;
  sku: string;
  description: string;
  price: number;
}
