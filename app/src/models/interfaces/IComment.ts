export interface IComment {
  id?: number,
  authorId: number,
  author: object,
  timestamp: Date,
  content: string,
  replies: IComment[]
}
