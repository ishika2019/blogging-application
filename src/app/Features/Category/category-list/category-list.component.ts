import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/Category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categorie$?: Observable<Category[]>;
  constructor(private categoryService: CategoryService) {
    
    
  }
  ngOnInit(): void {
    // this.categoryService.getcategory().subscribe(
    //   {
    //     next:(response)=>
    //     {
    //           this.categorie=response;
    //     }
    //   }
    // );
    this.categorie$=this.categoryService.getcategory();
  }


}
