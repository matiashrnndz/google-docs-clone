import { Injectable } from "@angular/core";
import { Observable } from "rxjs"; 
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Document } from "../../entities/document/document";

@Injectable()
export class TopService extends CatchService {

    private WEB_API_URL : string = 'api/tops';
    private options : RequestOptions;

    constructor(
        private httpService: Http
    ){
        super();
        this.message = "Tops error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    getTop3Documents(): Observable<Array<Document>> {
        return this.httpService.get(this.WEB_API_URL + '/' + 'top3documents', this.options)
        .pipe(
            map((response : Response) => <Array<Document>> response.json()),
            tap(data => console.log('El top 3 de documentos fueron : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

}