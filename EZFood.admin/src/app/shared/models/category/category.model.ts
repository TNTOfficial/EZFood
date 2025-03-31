

export interface Category {
  id: string;
  name: string;
  description?: string;
  parentId?: string;
  status: boolean;
  imageUrl?: string;
  file?: File;
  children: Category[];
}
