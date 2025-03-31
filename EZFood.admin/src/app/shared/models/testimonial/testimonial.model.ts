export interface Testimonial {
  id: string;
  name: string;
  description?: string;
  designation?: string;
  imageUrl: string;
  linkName?: string;
  link?: string;
  status: boolean;
  createdAt: string;
  updatedAt?: string;
}
