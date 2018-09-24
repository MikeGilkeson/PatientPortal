import { Component, OnInit } from '@angular/core';

import {PatientsService} from '../services/patients.service';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';


import {Patient} from '../models/patient.model';
import {DataSource} from '@angular/cdk/table';
import {Observable} from 'rxjs/Observable';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  patients: Patient[];
  private message: string = "Loading";
  constructor(private patientService: PatientsService,
    private router: Router) { }
  dataSource = new PostDataSource(this.patientService);
  displayedColumns = ['lastName', 'firstName', 'lastVisit', 'nextVisit'];

  onError (message: string) {
    this.message = `Unable to retrieve patient information: '${message}'`;
  }

  showDetails(row) {
    this.router.navigate(['/patients', row.id]);
  }
  
  ngOnInit() {
    // this.patientService
    //   .query()
    //   .subscribe(
    //     (res: HttpResponse<Patient[]>) => {
    //       this.patients = res.body
    //     },
    //     (res: HttpErrorResponse) => {
    //       this.onError(res.message)
    //     }
    //   );
  }
}
export class PostDataSource extends DataSource<any> {
  constructor(private patientService: PatientsService) {
    super();
  }

  connect(): Observable<Patient[]> {
    return this.patientService.queryPatients();
  }

  disconnect() {
  }
}
