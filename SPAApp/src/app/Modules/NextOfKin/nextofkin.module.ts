import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { NextOfKinFormComponent } from './form/NextOfKinfrm';
import { NextOfKinListComponent } from './list/NextOfKinlist';
import { NextOfKinComponent } from './NextOfKin';
@NgModule({
  imports: [FormsModule, CommonModule],
    declarations: [
      NextOfKinFormComponent,
      NextOfKinListComponent,
     NextOfKinComponent
			],    
    exports: [
      NextOfKinFormComponent,
      NextOfKinListComponent,
      NextOfKinComponent
  ],
  
})
export class NextOfKinModule { }
