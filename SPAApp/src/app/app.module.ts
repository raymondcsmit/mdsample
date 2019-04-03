import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BasicService } from './services/Basic.service';
import { GpDetailService } from './services/GpDetail.service';
import { NextOfKinService } from './services/NextOfKin.service';
import { PatientService } from './services/Patient.service';
import { BasicModule } from './Modules/Basic/basic.module';
import { GpDetailModule } from './Modules/GpDetail/gpdetail.module';
import { NextOfKinModule } from './Modules/NextOfKin/nextofkin.module';
import { PatientModule } from './Modules/Patient/patient.module';
import { RequestInterceptor } from './services/RequestInterceptor';
import { SearchModule } from './Modules/SearchModule/Search.module';

const routes: Routes = [
  { path: '', loadChildren: () => PatientModule },
];


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
  ],
  
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BasicModule,
    GpDetailModule,
    NextOfKinModule,
    PatientModule,
    SearchModule,
    RouterModule.forRoot(routes)
  ],
  providers: [BasicService, GpDetailService, NextOfKinService, PatientService, 
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true
    },],
  bootstrap: [AppComponent]
})
export class AppModule { }
