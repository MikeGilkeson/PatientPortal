import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { switchMap, map } from 'rxjs/operators';

import {PatientsService} from '../services/patients.service';
import {Patient} from '../models/patient.model';

@Component({
  selector: 'app-patient-details',
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.css']
})
export class PatientDetailsComponent implements OnInit {
  private errors: string;
  patient: Patient;
  constructor(
    private activatedRoute: ActivatedRoute,
    private service: PatientsService
  ) { }


  ngOnInit() {
    this
      .activatedRoute
      .params
      .pipe(
        map(p => p['id']),
        switchMap(id => this.service.find(id))
      )
      .subscribe(
        answer => {
          this.patient = answer.body;
          this.errors = '';
        },
        err => {
          this.errors = `Error loading - ${err}`;
        }
      );
  }

}
