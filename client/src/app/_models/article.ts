export interface Article {
  title: string,
  authorName: string,
  contents: string,
  tags?: string[],
  comments?: Comment[]
}
