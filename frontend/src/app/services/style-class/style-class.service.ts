import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { StyleClass } from "../../entities/style-class/style-class";

@Injectable()
export class StyleClassService extends CatchService {

    private WEB_API_URL: string = 'api/styleclasses';
    private options: RequestOptions;

    constructor(
        private httpService: Http
    ) {
        super();
        this.message = "Style Class error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    getAll(): Observable<Array<StyleClass>> {
        return this.httpService.get(this.WEB_API_URL, this.options)
            .pipe(
                map((response: Response) => <Array<StyleClass>> response.json()),
                tap(data => console.log('Style Classes : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    get(styleClassName: string): Observable<StyleClass> {
        return this.httpService.get(this.WEB_API_URL + '/' + styleClassName + '/', this.options)
        .pipe(
            map((response : Response) => <StyleClass> response.json()),
            tap(data => console.log('El dato obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}