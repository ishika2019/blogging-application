import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPostService } from 'src/app/Features/blog-post/Service/blog-post.service';
import { BlogPost } from 'src/app/Features/blog-post/models/blog-post.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
      blogpost$?:Observable<BlogPost[]>;
      constructor(private blogpostservice:BlogPostService) {
      
        
      }
  ngOnInit(): void {
   this.blogpost$=this.blogpostservice.getAll();
  }

}
