import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs"; 
import { Http, Response, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Header } from "../../entities/header/header";
import { Text } from "../../entities/text/text";

@Injectable()
export class HeaderService extends CatchService {

    private _httpService : Http;
    private WEB_API_URL : string = 'api/headers';
    private options : RequestOptions;

    constructor(_httpService: Http){
        super();
        this._httpService = _httpService;
        this.message = "Header error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    GetHeadersText(headerId: string): Observable<Text> {
        return this._httpService.get(this.WEB_API_URL + '/' + headerId + '/texts', this.options)
        .pipe(
            map((response : Response) => <Text> response.json()),
            tap(data => console.log('El text del header obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    update(headerId:string, header:Header): Observable<Response>{
        return this._httpService.put(this.WEB_API_URL + '/' + headerId + '/', header, this.options)
        .pipe(
            tap(data => console.log('Se modificó el header : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    AddTextToHeader(headerId: string, text: Text): Observable<Text>{
        return this._httpService.post(this.WEB_API_URL + '/' + headerId + '/texts', text, this.options)
        .pipe(
            map((response : Response) => <Text> response.json()),
            tap(data => console.log('Se agregó al header el text : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    delete(headerId: string) : Observable<Response>{
        return this._httpService.delete(this.WEB_API_URL + '/' + headerId + '/', this.options)
        .pipe(
            tap(data => console.log('Se eliminó el header : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}