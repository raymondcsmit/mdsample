import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { BasicFormComponent } from './form/Basicfrm';
import { BasicListComponent } from './list/Basiclist';
import { BasicComponent } from './Basic';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  imports: [FormsModule, CommonModule, NgbModule], 
    declarations: [
      BasicFormComponent,
      BasicListComponent,
      BasicComponent
			],    
    exports: [
      BasicFormComponent,
      BasicListComponent,
      BasicComponent
  ],
  
})
export class BasicModule { }
