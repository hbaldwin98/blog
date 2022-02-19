import { UserComment } from "./usercomment";

export interface Article {
  title: string,
  urlIdentity: string,
  authorName: string,
  contents: string,
  dateCreated: Date,
  dateEdited: Date,
  tags?: string[],
  comments: UserComment[]
}
