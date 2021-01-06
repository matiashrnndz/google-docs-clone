import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Text } from "../../entities/text/text";

@Injectable()
export class TextService extends CatchService {

    private WEB_API_URL: string = 'api/texts';
    private options: RequestOptions;

    constructor(
        private httpService: Http
    ) {
        super();
        this.message = "Text error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    update(textId: string, text: Text): Observable<Response> {
        return this.httpService.put(this.WEB_API_URL + '/' + textId + '/', text, this.options)
        .pipe(
            tap(data => console.log('Se modificó el texto : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    delete(textId: string) : Observable<Response> {
        return this.httpService.delete(this.WEB_API_URL + '/' + textId + '/', this.options)
        .pipe(
            tap(data => console.log('Se eliminó el texto : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}