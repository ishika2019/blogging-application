import { NgModule } from '@angular/core';
import { RouterModule, Routes, withEnabledBlockingInitialNavigation } from '@angular/router';
import { CategoryListComponent } from './Features/Category/category-list/category-list.component';
import { AddCategoryComponent } from './Features/Category/add-category/add-category.component';
import { EditCategoryComponent } from './Features/Category/edit-category/edit-category.component';
import { BlogpostListComponent } from './Features/blog-post/blogpost-list/blogpost-list.component';
import { AddBlogpostComponent } from './Features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogPostComponent } from './Features/blog-post/edit-blog-post/edit-blog-post.component';
import { HomeComponent } from './public/home/home.component';
import { BlogDetailsComponent } from './public/blog-details/blog-details.component';
import { LoginComponent } from './Features/auth/login/login.component';

const routes: Routes = [
  {
      path:'',
      component:HomeComponent
  },
  {
    path:'login',
    component:LoginComponent
},
  { 
      path:'blog/:url',
      component:BlogDetailsComponent
  },
  {
    path:'admin/categories',
    component:CategoryListComponent
  },
  {
    path:'admin/categories/add',
    component:AddCategoryComponent
  },
  {
    path:'admin/categories/:id',
    component:EditCategoryComponent
  },
  {
    path:'admin/blogposts',
    component:BlogpostListComponent
  },
  {
    path:'admin/blogposts/add',
    component:AddBlogpostComponent
  },
  {
    path:'admin/blogposts/:id',
    component:EditBlogPostComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
