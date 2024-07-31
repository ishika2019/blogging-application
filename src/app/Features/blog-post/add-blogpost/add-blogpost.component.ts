import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../Service/blog-post.service';
import { Route, Router } from '@angular/router';
import { CategoryService } from '../../Category/services/category.service';
import { CategoryListComponent } from '../../Category/category-list/category-list.component';
import { Category } from '../../Category/models/Category.model';
import { Observable } from 'rxjs';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit {
  model:AddBlogPost;
  isImageSelectorVisible:boolean=false;
  category$?:Observable<Category[]>;


  constructor( private blogpostservice: BlogPostService, private router:Router ,private categoryservice:CategoryService,
    private imageservice:ImageService) {
    this.model={
      Title:' ',
      ShortDescription:' ',
      Content:' ',
      FeaturedImage:' ',
      PublishDate :new Date(),
    Author:' ',
      IsVisible:true,
      UrlHandle:' ',
      categories:[]
    }
    
  }
  ngOnInit(): void {

     this.category$=this.categoryservice.getcategory();
     this.imageservice.onSelectImage().subscribe(
      {
        next:(response)=>
        {
              if(this.model!=null)
              {
                this.model.FeaturedImage=response.url;
                this.isImageSelectorVisible=false;
              }
        }
      }
    )
       
  }
  onFormSubmit():void{
    console.log("ishika");
    console.log(this.model);
    this.blogpostservice.createBlogPost(this.model).subscribe(
      {
        next:(response)=>
        {
          this.router.navigateByUrl('/admin/blogposts');
        }
      }
    )
  }
  openImageSelector():void{
    this.isImageSelectorVisible=true;
}
closeImageSelector():void{
this.isImageSelectorVisible=false;
}
  

}
