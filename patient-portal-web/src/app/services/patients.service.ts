import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, } from 'rxjs';

import {Patient} from '../models/patient.model';

type PatientResponseType = HttpResponse<Patient>;
type PatientArrayResponseType = HttpResponse<Patient[]>;


@Injectable({
  providedIn: 'root'
})
export class PatientsService {
  private resourceUrl = "https://localhost:44320/api/patients";//environment.apiBaseUrl + 'api/patients';

  constructor(private http: HttpClient) { }

  find(id: number): Observable<PatientResponseType> {
    return this.http.get<Patient>(`${this.resourceUrl}/${id}`, { observe: 'response' });
  }

  query(req?: any): Observable<PatientArrayResponseType> {
    return this.http.get<Patient[]>(this.resourceUrl, {  observe: 'response' });
  }

  queryPatients(req?: any): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.resourceUrl);
  }
}
