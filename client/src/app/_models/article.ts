import { UserComment } from "./usercomment";

export interface Article {
  title: string,
  urlIdentity: string,
  authorPublicName: string,
  contents: string,
  dateCreated: Date,
  dateEdited: Date,
  tags?: string[],
  comments: UserComment[]
}
