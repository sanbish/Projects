import { Component, OnInit } from '@angular/core';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: 'dashboard', title: 'Dashboard',  icon: 'dashboard', class: '' },
    { path: 'patient-list', title: 'Patients',  icon:'person', class: '' },
    { path: 'table-list', title: 'Consultations', icon: 'content_paste', class: '' },
    { path: 'typography', title: 'Dossier',  icon:'library_books', class: '' },
    { path: 'notifications', title: 'Protocols', icon: 'notifications', class: '' },
    { path: 'icons', title: 'Planning',  icon:'bubble_chart', class: '' },
    { path: 'upgrade', title: 'Tasks', icon: 'unarchive', class: '' },
    { path: 'reports', title: 'Reports', icon: 'content_paste', class: '' },
    { path: 'maps', title: 'Settings',  icon:'location_on', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };
}
