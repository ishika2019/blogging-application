import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { BlogPostService } from 'src/app/Features/blog-post/Service/blog-post.service';
import { BlogPost } from 'src/app/Features/blog-post/models/blog-post.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit{

   blogpost$?:Observable<BlogPost>;
  constructor(private route:ActivatedRoute,private blogservice:BlogPostService) {

    
  }

  url:string |null=null;
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>
      {
        this.url=params.get('url');
      }
    }
      
      );
      if(this.url)
      {
        this.blogpost$=this.blogservice.getBlogPostByUrlHandle(this.url);
      }
    
  }

}
