import { Component, OnInit, ViewChild } from '@angular/core';
import { ImageService } from './image.service';
import { Observable } from 'rxjs';
import { BlogImage } from '../../Models/blog-image.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent implements OnInit {


  image$?:Observable<BlogImage[]>;

  @ViewChild('form',{static:false})ImageUploadForm?:NgForm;

  constructor(private imageService:ImageService) {
   
  }
  ngOnInit(): void {
    this.getImages();
  }
  private file?:File;
  fileName: string='';
  title: string='';
  onFileUploadChange(event:Event):void
  {
        
   const element=event.currentTarget as HTMLInputElement;
   this.file=element.files?.[0];


  } 
  UploadImage():void{
      if(this.file && this.fileName!=='' && this.title!=='')
      {
        this.imageService.uploadImage(this.file,this.fileName,this.title).subscribe(
          {
            next:(response)=>
            {
              console.log(response);

              this.getImages();
              this.ImageUploadForm?.resetForm();
            }
          }
        )
      }
  }
  private getImages()
  {
    this.image$=this.imageService.getAll();
  }

  selectImage(image:BlogImage):void{
      this.imageService.selectImage(image);
  }
  


}