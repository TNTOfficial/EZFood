import { Position } from "../enums/position";

export interface NetworkNode {
  id: string;
  userId: string;
  parentId?: string;
  position: Position;
  level: number;
}
