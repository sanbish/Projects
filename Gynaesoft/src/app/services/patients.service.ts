import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import 'rxjs/add/operator/toPromise';
import { PatientList } from '../models/PatientList.model'

@Injectable()
export class PatientsService {

  private serviceUrl = "http://localhost:4200/assets/data/patient.json";

  constructor(private http: HttpClient) { }

  //data:PatientList[]
  data1: string
  public getPatient() {
    this.http.get(this.serviceUrl)
    //.subscribe((patients:PatientList[]) => { this.patients = this.patients});
    //.subscribe((data: any[]) => { console.log(data)});
    .subscribe((data:Response) => { console.log(data)});
  }

  ngOnInit(){
     //this.getPatient();
  }
}

    //.map(res:Response  => res.json());
    //.catch(this.handleErrorObservable);

    /*private extractData(res: Response) {
	let body = res.json();
        return body;
    }
    
    private handleErrorObservable (error: Response | any) {
	console.error(error.message || error);
	return Observable.throw(error.message);
    } */

      /*.map(res => res.json())
        .subscribe(data => this.data = data,
                    err => console.log(err),
                    () => console.log('Completed'));*/
        //./data/patient.json
        //.map((res:any) => res.json());
 

     /*   getPatient(): Observable<PatientList[]> {
        return this.http.get<PatientList[]>(this.serviceUrl);
    }*/

