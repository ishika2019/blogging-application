import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Subscription, retry } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/Category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit,OnDestroy{

  id:string | null=null;
  paramsSubscription?:Subscription;
  EditSubscription?:Subscription;
  category?:Category;
  constructor(private route:ActivatedRoute,private categoryService:CategoryService,private router:Router) {
  
    
  }
  ngOnInit(): void {
    this.paramsSubscription= this.route.paramMap.subscribe(
      {
        next:(param)=>
        {
          this.id=param.get('id');
          if(this.id)
          {
            this.categoryService.getCategoryById(this.id).subscribe(
              {
                next:(response)=>{
                  this.category=response;
                }
              }
            )
          }
        }
    
      }
     )
  }
  
  onFormSubmit():void{
    const updateCategory:UpdateCategoryRequest={
          name:this.category?.name??' ',
          urlHandle: this.category?.urlHandle?? ' '
    };
    if(this.id)
    {
      this.EditSubscription=this.categoryService.updateCategory(this.id,updateCategory).subscribe(
        {
          next:(response)=>{
            return this.router.navigateByUrl('/admin/categories');
          }
        }
      )
    }

  }
  onDelete():void{
    if(this.id){
     this.categoryService.deleteCategory(this.id).subscribe(
      {
        next:(response)=>
        {
          return this.router.navigateByUrl('/admin/categories');
        }
      }
     );
    }
  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.EditSubscription?.unsubscribe();
   }

}
