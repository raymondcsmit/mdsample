import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { PatientComponent } from './Patient';
import { PatientListComponent } from './list/Patientlist';
import { PatientFormComponent } from './form/Patientfrm';
import { BasicModule } from '../Basic/basic.module';
import { NextOfKinModule } from '../NextOfKin/nextofkin.module';
import { GpDetailModule } from '../GpDetail/gpdetail.module';
import { RouterModule } from '@angular/router';
import { SearchModule } from '../SearchModule/Search.module';
import { SharedModule } from '../../shared/shared.module';
@NgModule({
  imports: [FormsModule, CommonModule, BasicModule, NextOfKinModule, GpDetailModule, SearchModule,SharedModule,
     RouterModule.forRoot([
      { path: '', component: PatientComponent, pathMatch: 'full' },
    ])

  ],
    declarations: [
      PatientFormComponent,
      PatientListComponent,
     PatientComponent
			],    
    exports: [
      PatientFormComponent,
      PatientListComponent,
      PatientComponent
  ],
  
})
export class PatientModule { }
