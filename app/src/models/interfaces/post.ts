import {IComment} from "./IComment";

export interface IPost {
  id: number,
  authorId: number,
  title: string,
  body: string,
  snippet: string,
  author: object,
  timestamp: Date,
  comments: IComment[]
}
