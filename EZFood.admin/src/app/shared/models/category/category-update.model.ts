export interface CategoryUpdate {
  name: string;
  description?: string;
  parentId?: string;
  status: boolean;
  imageUrl?: string;
  file: File | null;
}
