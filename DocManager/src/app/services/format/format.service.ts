import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Format } from '../../entities/format/format';

@Injectable()
export class FormatService extends CatchService {

    private WEB_API_URL: string = 'api/formats';
    private options: RequestOptions;

    constructor(
        private httpService: Http
    ) {
        super();
        this.message = "Format error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    getAll(): Observable<Array<Format>> {
        return this.httpService.get(this.WEB_API_URL, this.options)
            .pipe(
                map((response: Response) => <Array<Format>> response.json()),
                tap(data => console.log('Formats : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    get(formatName: string): Observable<Format> {
        return this.httpService.get(this.WEB_API_URL + '/' + formatName + '/', this.options)
        .pipe(
            map((response : Response) => <Format> response.json()),
            tap(data => console.log('El formato obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}