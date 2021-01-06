import { Component } from '@angular/core';

@Component({
  selector: 'app-home-layout',
  template: `
    <app-main-nav></app-main-nav>
    <router-outlet></router-outlet>
  `,
  styles: []
})
export class HomeLayoutComponent {}
