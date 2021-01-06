import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { User } from "../../entities/user/user";
import { FriendRequest } from "../../entities/friend-request/friend-request";

@Injectable()
export class FriendService extends CatchService {

    private WEB_API_URL: string = 'api/friends';
    private options: RequestOptions;

    constructor(
        private httpService: Http
    ) {
        super();
        this.message = "Friend error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    getRequests(userEmail: string): Observable<Array<User>> {
        return this.httpService.get(this.WEB_API_URL + '/' + userEmail + '/listrequests', this.options)
            .pipe(
                map((response: Response) => <Array<User>>response.json()),
                tap(data => console.log('Friend requests : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    getFriends(userEmail: string): Observable<Array<User>> {
        return this.httpService.get(this.WEB_API_URL + '/' + userEmail + '/list', this.options)
            .pipe(
                map((response: Response) => <Array<User>>response.json()),
                tap(data => console.log('Friends : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    sendRequest(userEmail: string): Observable<FriendRequest> {
        return this.httpService.post(this.WEB_API_URL + '/' + userEmail + '/sendrequest', null, this.options)
            .pipe(
                map((response: Response) => <FriendRequest> response.json()),
                tap(data => console.log('Friend request sent status : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    respondRequest(userEmail: string, answer: boolean): Observable<FriendRequest> {
        this.options.headers.append('Content-Type', 'application/json');
        return this.httpService.post(this.WEB_API_URL + '/' + userEmail + '/respondrequest', answer, this.options)
            .pipe(
                map((response: Response) => <FriendRequest> response.json()),
                tap(data => console.log('friend request was responded as : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }
}