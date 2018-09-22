import { Component, OnInit } from '@angular/core';

import {PatientsService} from '../services/patients.service';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

import {Patient} from '../models/patient.model';


@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  private patients: Patient[];
  private message: string = "Loading";
  constructor(private patientService: PatientsService) { }

  onError (message: string) {
    this.message = `Unable to retrieve patient information: '${message}'`;
  }

  ngOnInit() {
    this.patientService
      .query()
      .subscribe(
        (res: HttpResponse<Patient[]>) => {
          this.patients = res.body
        },
        (res: HttpErrorResponse) => {
          this.onError(res.message)
        }
      );
  }

}
