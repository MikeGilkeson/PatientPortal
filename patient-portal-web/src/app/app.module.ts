import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { PatientsComponent } from './patients/patients.component';
import {RouteModule} from './route/route.module';

import { AppRoutingModule } from './app-routing.module';

import {genderPipe} from './pipes/gender.pipe';
import {agePipe} from './pipes/age.pipe';

@NgModule({
  declarations: [
    AppComponent,
    PatientsComponent,
    genderPipe,
    agePipe
  ],
  imports: [
    BrowserModule, 
    RouteModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
