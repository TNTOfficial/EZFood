export interface ApiResponse<T> {
  data?: T;
  error?: string;
  isSuccess: boolean;
}
