import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { GpDetailFormComponent } from './form/GpDetailfrm';
import { GpDetailListComponent } from './list/GpDetaillist';
import { GpDetailComponent } from './GpDetail';
import { GpDetailListFormComponent } from './listfrm/GpDetaillistfrm';
@NgModule({
  imports: [FormsModule, CommonModule],
    declarations: [
      GpDetailFormComponent,
      GpDetailListComponent,
      GpDetailListFormComponent,
      GpDetailComponent
			],    
    exports: [
      GpDetailFormComponent,
      GpDetailListComponent,
      GpDetailListFormComponent,
      GpDetailComponent
  ],
  
})
export class GpDetailModule { }
