import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router
    ) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        let isLogged = localStorage.getItem("token") != null;
        if (!isLogged) {
            this.router.navigate(['/login']);
            return false;
        };
        return true;
    }
}
