import { Position } from "../enums/position";

export interface NetworkNodeForUserDetail {
  id: string;
  parentId?: string;
  position: Position;
  level: number;
  createdAt: Date;
}
