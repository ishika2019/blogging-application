import { Component, OnInit } from '@angular/core';
import { BlogPost } from '../models/blog-post.model';
import { Observable } from 'rxjs';
import { BlogPostService } from '../Service/blog-post.service';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {

 blogposts$?:Observable<BlogPost[]>;
 
 constructor( private blogpostservice: BlogPostService) {
  
  
 }
  ngOnInit(): void {
    
     this.blogposts$=this.blogpostservice.getAll();
     console.log(this.blogposts$);
  }

}
