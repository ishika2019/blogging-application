import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Features/auth/models/user.model';
import { AuthService } from 'src/app/Features/auth/service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
 user?:User;
 
  constructor(private auth:AuthService,private router:Router) {
   
  
  }
  ngOnInit(): void {
   this.auth.user().subscribe(
    {
      next:(response)=>
      {
        this.user=response;
      }
    }
   );

   this.user=this.auth.getUser();
   console.log(this.user);
  }
  onLogout():void{

    this.auth.logout();
    this.router.navigateByUrl('/');
    
  }


}
