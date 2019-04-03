import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { BasicModule } from '../Basic/basic.module';
import { NextOfKinModule } from '../NextOfKin/nextofkin.module';
import { GpDetailModule } from '../GpDetail/gpdetail.module';
import { RouterModule } from '@angular/router';
import { SearchComponent } from './Search';
@NgModule({
  imports: [FormsModule, CommonModule, BasicModule, NextOfKinModule, GpDetailModule
  ],
    declarations: [
      SearchComponent
			],    
    exports: [
      SearchComponent
  ],
  
})
export class SearchModule { }
