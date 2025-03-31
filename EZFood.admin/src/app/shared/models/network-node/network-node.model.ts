import { User } from "../user/user.model";

export interface NetworkNode {
  id: string;
  userId: string;
  parentId?: string;
  //position: 'Left' | 'Right';
  position: number; // 0 for Left, 1 for Right
  level: number;
  createdAt: string;
  user: User;
  children: NetworkNode[];
}
