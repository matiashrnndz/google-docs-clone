import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { User } from '../../entities/user/user';
import { Session } from '../../entities/session/session';
import { Http, Response } from '@angular/http'
import { Observable, throwError } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';
import { AppConfig } from '../../app-config';
import { CatchService } from '../catch/catch.service';
import { debug } from 'util';

@Injectable()
export class AuthService extends CatchService {

    private WEB_API_URL: string = 'api/login';

    constructor(
        private router: Router,
        private httpService: Http
    ) {
        super();
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.message = "Error Login";
    }

    login(user: User): Observable<Session> {
        return this.httpService.post(this.WEB_API_URL, user)
            .pipe(
                map((response: Response) => <Session>response.json()),
                catchError(this.handleError)
            );
    }
}
