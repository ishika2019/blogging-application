import { Category } from "../../Category/models/Category.model";

export interface BlogPost{
    id:string;
    title:string;
    shortDescription:string;
    content:string;
    featuredImage:string;
    urlHandle:string;
    author:string;
    publishDate:Date;
    isVisible:boolean;
    categories:Category[];

}