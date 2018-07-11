import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
//import { MatTableModule } from '@angular/material'
import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
// ag-grid
import { AgGridModule } from "ag-grid-angular";

import { AppComponent } from './app.component';

import { DashboardComponent } from './dashboard/dashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { TableListComponent } from './table-list/table-list.component';
import { TypographyComponent } from './typography/typography.component';
import { IconsComponent } from './icons/icons.component';
import { MapsComponent } from './maps/maps.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { UpgradeComponent } from './upgrade/upgrade.component';
import { PatientListComponent } from './patient-list/patient-list.component';

import { PatientsService } from './services/patients.service'

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    UserProfileComponent,
    TableListComponent,
    TypographyComponent,
    IconsComponent,
    MapsComponent,
    NotificationsComponent,
    UpgradeComponent,
    PatientListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
   // MatTableModule,
    AppRoutingModule,
	AgGridModule.withComponents([])
  ],
    providers: [PatientListComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
