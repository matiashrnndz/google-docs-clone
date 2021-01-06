import { Injectable } from "@angular/core";
import { Observable } from "rxjs"; 
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Coordinate } from "../../entities/coordinate/coordinate";

@Injectable()
export class GraphDocumentCreationService extends CatchService {

    private WEB_API_URL : string = 'api/documentcreationgraphic';
    private options : RequestOptions;

    constructor(
        private httpService: Http
    ){
        super();
        this.message = "Graph Document Creation error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    get(startingDate: string, lastestDate: string): Observable<Array<Coordinate>> {
        this.options.headers.delete('Starting-Date');
        this.options.headers.delete('Lastest-Date');
        this.options.headers.append('Starting-Date', startingDate);
        this.options.headers.append('Lastest-Date', lastestDate);
        return this.httpService.get(this.WEB_API_URL, this.options)
        .pipe(
            map((response : Response) => <Array<Coordinate>> response.json()),
            tap(data => console.log('Las coordenadas obtenidas fueron : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

}