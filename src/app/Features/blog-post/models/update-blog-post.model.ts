export interface updatedBlogPostRequested{
    title:string;
    shortDescription:string;
    content:string;
    featuredImage:string;
    urlHandle:string;
    author:string;
    publishDate:Date;
    isVisible:boolean;
    categories: string[];
}