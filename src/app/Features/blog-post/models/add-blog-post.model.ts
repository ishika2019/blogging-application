export interface AddBlogPost{
    Title:string;
    ShortDescription:string;
    Content:string;
    FeaturedImage:string;
    UrlHandle:string;
    Author:string;
    PublishDate:Date;
    IsVisible:boolean;
    categories:string[];

}