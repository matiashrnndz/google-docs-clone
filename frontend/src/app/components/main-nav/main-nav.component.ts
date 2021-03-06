import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Router } from '@angular/router';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );
    
  constructor(
      private breakpointObserver: BreakpointObserver,
      private router: Router
    ) {}
  
    onLogout() {
        localStorage.removeItem("token");
        localStorage.removeItem("name");
        localStorage.removeItem("lastname");

        this.router.navigate(['/login']);
    }

    isAdmin() : boolean {
        return true;
    }

    onManageUsers() {
        this.router.navigate(['/manageusers']);
    }
  }
