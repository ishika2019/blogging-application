import { Injectable, OnInit } from '@angular/core';
import { AddBlogpostComponent } from '../add-blogpost/add-blogpost.component';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { observeNotification } from 'rxjs/internal/Notification';
import { HttpClient } from '@angular/common/http';
import { AddBlogPost } from '../models/add-blog-post.model';
import { environment } from 'src/environments/environment.development';
import { updatedBlogPostRequested } from '../models/update-blog-post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService  {

  constructor(private http : HttpClient) { 
  }
  
    getBlogPostById(id: string):Observable<BlogPost>
  {
   return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/BlogPost/${id}`);    
  }
  getBlogPostByUrlHandle(urlHandle: string):Observable<BlogPost>
  {
   return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/BlogPost/${urlHandle}`);    
  }
  
    
    createBlogPost(data : AddBlogPost ):Observable<BlogPost>{
         return this.http.post<BlogPost>(`${environment.apiBaseUrl}/api/BlogPost`,data);
    
  }

  getAll():Observable<BlogPost[]>{
    var b=this.http.get<BlogPost[]>(`${environment.apiBaseUrl}/api/BlogPost`);
    console.log(b);
    return b;
  }

  updateBlogPost(id:string,updatedblogpost: updatedBlogPostRequested):Observable<BlogPost>
  {
    return this.http.put<BlogPost>(`${environment.apiBaseUrl}/api/BlogPost/${id}`,updatedblogpost);
  }

  deleteBlogPost(id:string):Observable<BlogPost>
  {
    return this.http.delete<BlogPost>(`${environment.apiBaseUrl}/api/BlogPost/${id}`);
  }

  
}
