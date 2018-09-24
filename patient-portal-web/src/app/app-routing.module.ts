import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import {PatientsComponent } from './patients/patients.component';
import { HomeComponent } from './home/home.component';
import {PatientDetailsComponent} from './patient-details/patient-details.component';

const routes: Routes = [
  { path: '',  component: HomeComponent },
  { path: 'patients', component: PatientsComponent },
  { path: 'patients/:id', component: PatientDetailsComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule
  ]
})

export class AppRoutingModule { }
