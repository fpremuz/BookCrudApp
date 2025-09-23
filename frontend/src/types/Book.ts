export interface Book {
  id: number;
  title: string;
  author: string;
  pages: number;
}

export interface CreateBookRequest {
  title: string;
  author: string;
  pages: number;
}

export interface UpdateBookRequest {
  id: number;
  title: string;
  author: string;
  pages: number;
}
