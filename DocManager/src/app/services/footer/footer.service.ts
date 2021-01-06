import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs"; 
import { Http, Response, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Footer } from "../../entities/footer/footer";
import { Text } from "../../entities/text/text";

@Injectable()
export class FooterService extends CatchService {

    private _httpService : Http;
    private WEB_API_URL : string = 'api/footers';
    private options : RequestOptions;

    constructor(_httpService: Http){
        super();
        this._httpService = _httpService;
        this.message = "Footer error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    GetFootersText(footerId: string): Observable<Text> {
        return this._httpService.get(this.WEB_API_URL + '/' + footerId + '/texts', this.options)
        .pipe(
            map((response : Response) => <Text> response.json()),
            tap(data => console.log('El text del footer obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    update(footerId:string, footer:Footer): Observable<Response>{
        return this._httpService.put(this.WEB_API_URL + '/' + footerId + '/', footer, this.options)
        .pipe(
            tap(data => console.log('Se modificó el footer : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    AddTextToFooter(footerId: string, text: Text): Observable<Text>{
        return this._httpService.post(this.WEB_API_URL + '/' + footerId + '/texts', text, this.options)
        .pipe(
            map((response : Response) => <Text> response.json()),
            tap(data => console.log('Se agregó al footer el text : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    delete(footerId: string) : Observable<Response>{
        return this._httpService.delete(this.WEB_API_URL + '/' + footerId + '/', this.options)
        .pipe(
            tap(data => console.log('Se eliminó el footer : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}