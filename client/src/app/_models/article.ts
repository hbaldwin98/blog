import { UserComment } from "./usercomment";

export interface Article {
  title: string,
  urlIdentity: string,
  authorPublicName: string,
  contents: string,
  headline: string,
  dateCreated: Date,
  dateEdited: Date,
  tags?: string[],
  comments: UserComment[]
}
