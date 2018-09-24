import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MaterialModule} from './material.module';
import {FlexLayoutModule} from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { PatientsComponent } from './patients/patients.component';
import {RouteModule} from './route/route.module';

import { AppRoutingModule } from './app-routing.module';

import {genderPipe} from './pipes/gender.pipe';
import {agePipe} from './pipes/age.pipe';
import { HomeComponent } from './home/home.component';
import { PatientDetailsComponent } from './patient-details/patient-details.component';

@NgModule({
  declarations: [
    AppComponent,
    PatientsComponent,
    genderPipe,
    agePipe,
    HomeComponent,
    PatientDetailsComponent
  ],
  imports: [
    BrowserModule, 
    RouteModule,
    AppRoutingModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    FlexLayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
