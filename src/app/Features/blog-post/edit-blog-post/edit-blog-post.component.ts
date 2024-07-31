import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../Service/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { Category } from '../../Category/models/Category.model';
import { CategoryService } from '../../Category/services/category.service';
import { updatedBlogPostRequested } from '../models/update-blog-post.model';
import { flush } from '@angular/core/testing';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blog-post',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.css']
})
export class EditBlogPostComponent implements OnInit,OnDestroy{

  id:string | null = null;
  routeSubscription?:Subscription;
  model?:BlogPost
  getByIdSubscription?:Subscription;
  isImageSelectorVisible:boolean=false;
  category$?:Observable<Category[]>;
  selectedCategory?:string[];
  constructor(private route:ActivatedRoute,private blogservice:BlogPostService,
    private categoryservice:CategoryService,private router:Router,
    private imageservice:ImageService) {

    
  }
 
  ngOnInit(): void {
    this.category$=this.categoryservice.getcategory();

    this.routeSubscription=this.route.paramMap.subscribe(
      {
        next:(params)=>
        {
          this.id=params.get('id');
          if(this.id!=null){
          this.getByIdSubscription=this.blogservice.getBlogPostById(this.id).subscribe(
            {
              next:(response)=>
              {
                this.model=response;
                this.selectedCategory=response.categories.map(x=>x.id);
              }
            }
          );
          }

          this.imageservice.onSelectImage().subscribe(
            {
              next:(response)=>
              {
                    if(this.model!=null)
                    {
                      this.model.featuredImage=response.url;
                      this.isImageSelectorVisible=false;
                    }
              }
            }
          )
          
        }
         
      }

      
    )
  }
  onFormSubmit():void{
    if(this.model && this.id)
    {
      var updatedblogpost : updatedBlogPostRequested={
         author:this.model.author,
         content:this.model.content,
         shortDescription:this.model.shortDescription,
         featuredImage:this.model.featuredImage,
         isVisible:this.model.isVisible,
         publishDate:this.model.publishDate,
         title:this.model.title,
         urlHandle:this.model.urlHandle,
         categories:this.selectedCategory??[]
      };
    
    this.blogservice.updateBlogPost(this.id,updatedblogpost).subscribe(
      {
        next:(response)=>
        {
           this.router.navigateByUrl('admin/blogposts');
        }
      }
    );
    }


  }

  onDelete():void{
      if(this.id!=null){
    this.blogservice.deleteBlogPost(this.id).subscribe(
      {
        next:(response)=>
        {
           this.router.navigateByUrl('admin/blogposts');
        }
      }
    );
      }


  }
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.getByIdSubscription?.unsubscribe();
  }
  openImageSelector():void{
        this.isImageSelectorVisible=true;
  }
  closeImageSelector():void{
    this.isImageSelectorVisible=false;
  }

 

}
