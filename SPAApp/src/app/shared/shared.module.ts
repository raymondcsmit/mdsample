import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MValidationDirective } from './directive/validationDirective';
@NgModule({
  imports: [FormsModule, CommonModule, NgbModule], 
    declarations: [MValidationDirective
			],    
    exports: [MValidationDirective
  ],
  
})
export class SharedModule { }
