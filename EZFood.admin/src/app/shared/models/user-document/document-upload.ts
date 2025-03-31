import { DocumentType } from "../enums/document-type";

export interface DocumentUpload {
  userId: string;
  documentType: DocumentType;
  file: File;
  panNumber?: string;
  aadharNumber?: string;
}
