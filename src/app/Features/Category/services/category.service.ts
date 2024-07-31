import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable, retry } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/Category.model';
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { CookieOptions, CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient,private cookieservice:CookieService) { }

  addCategory(model : AddCategoryRequest): Observable<void>{

        return this.http.post<void>(`${environment.apiBaseUrl}/api/Categories`,model);
  };

  getcategory(): Observable<Category[]>{
    
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/Categories?addAuth=true`);

  }
  getCategoryById(id:string): Observable<Category>{
    return this.http.get<Category>(`${environment.apiBaseUrl}/api/Categories/${id}`);
  }
  updateCategory(id:string,updatecategoryrequest:UpdateCategoryRequest):Observable<Category>{
    return this.http.put<Category>(`${environment.apiBaseUrl}/api/Categories/${id}`,updatecategoryrequest);
  }

   deleteCategory(id:string):Observable<void>
   {

    console.log(this.cookieservice.get('Authorization'));
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/Categories/${id}`);
}
  
}
