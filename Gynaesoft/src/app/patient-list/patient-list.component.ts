import { Component } from '@angular/core';
import { GridOptions } from "ag-grid";

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.scss']
})

export class PatientListComponent{
	private gridOptions: GridOptions;
    private data;   
    SelectedPageSize = 1;
	
  constructor() {
    
    this.gridOptions = <GridOptions>{};
    this.createRowData();
    this.gridOptions.columnDefs = [
      { headerName: "Identifier", field: "identifier", width: 120,cellClass:"text-danger" },
      { headerName: "Last Name", field: "lastname", width: 120,headerClass:"text-danger" },
      { headerName: "First Name", field: "firstname", width: 120 },
      { headerName: "National Number", field: "nationalnumber", width: 120 },
      { headerName: "Date of Birth", field: "dateofbirth", width: 120 },
      { headerName: "Home Phone", field: "homephone", width: 120 },
      { headerName: "Mobile", field: "mobile", width: 120 },
      { headerName: "eMail", field: "email", width: 120 },
      { headerName: "Last Consultation", field: "lastconsultation", width: 120 },
      { headerName: "Marked",  width: 100, checkboxSelection: true }
    ];    
    this.gridOptions.rowData = this.data;
   }



 private createRowData() {
    const rowData: any[] = [];
    this.data = JSON.parse('[{"identifier":"10047","lastname":"Finstone","firstname":"fred","nationalnumber":"XXX-XX-5885","dateofbirth":"20/09/1979","homephone":"932-248-9755","mobile":"934-578-1457","email":"fred@finstone,in","lastconsultation":"21/03/2017","marked":"true"},{"identifier":"10048","lastname":"Doe","firstname":"John","nationalnumber":"XXX-XX-7909","dateofbirth":"20/09/1979","homephone":"322-489-9755","mobile":"+ 34578 457","email":"jdoe@patients.com","lastconsultation":"15/03/2017","marked":"true"},{"identifier":"10049","lastname":"Doe","firstname":"Jane","nationalnumber":"XXX-XX-9095","dateofbirth":"20/09/1979","homephone":"329-248-9755","mobile":"+ 34578 457","email":"janed@patients.com","lastconsultation":"07/11/2017","marked":"true"},{"identifier":"10048","lastname":"Doe","firstname":"Jeff","nationalnumber":"XXX-XX-8709","dateofbirth":"20/09/1979","homephone":"322-489-9255","mobile":"+ 34578 457","email":"jeff.doe@patients.com","lastconsultation":"15/03/2017","marked":"true"},{"identifier":"10049","lastname":"Doe","firstname":"Jill","nationalnumber":"XXX-XX-1095","dateofbirth":"20/09/1979","homephone":"329-248-9755","mobile":"+ 34578 457","email":"jilld@patients.com","lastconsultation":"07/11/2017","marked":"true"},{"identifier":"10047","lastname":"Finstone","firstname":"fred","nationalnumber":"XXX-XX-5885","dateofbirth":"20/09/1979","homephone":"932-248-9755","mobile":"934-578-1457","email":"fred@finstone,in","lastconsultation":"21/03/2017","marked":"true"},{"identifier":"10048","lastname":"Doe","firstname":"John","nationalnumber":"XXX-XX-7909","dateofbirth":"20/09/1979","homephone":"322-489-9755","mobile":"+ 34578 457","email":"jdoe@patients.com","lastconsultation":"15/03/2017","marked":"true"},{"identifier":"10049","lastname":"Doe","firstname":"Jane","nationalnumber":"XXX-XX-9095","dateofbirth":"20/09/1979","homephone":"329-248-9755","mobile":"+ 34578 457","email":"janed@patients.com","lastconsultation":"07/11/2017","marked":"true"},{"identifier":"10048","lastname":"Doe","firstname":"Jeff","nationalnumber":"XXX-XX-8709","dateofbirth":"20/09/1979","homephone":"322-489-9255","mobile":"+ 34578 457","email":"jeff.doe@patients.com","lastconsultation":"15/03/2017","marked":"true"}]');    
  }


  public onQuickFilterChanged($event) {
    this.gridOptions.api.setQuickFilter($event.target.value);
 } 
  public onPageSizeChanged(PageSize) {
      //var value = document.getElementById("page-size").value;
      //this.gridOptions.api.paginationSetPageSize(Number(value));
      this.gridOptions.api.paginationSetPageSize(Number(PageSize));
  }

  
}
