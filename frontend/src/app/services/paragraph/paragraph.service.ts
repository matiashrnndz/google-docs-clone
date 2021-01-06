import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs"; 
import { Http, Response, RequestOptions, Headers, URLSearchParams } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Paragraph } from "../../entities/paragraph/paragraph";
import { Text } from "../../entities/text/text";

@Injectable()
export class ParagraphService extends CatchService {

    private _httpService : Http;
    private WEB_API_URL : string = 'api/paragraphs';
    private options : RequestOptions;

    constructor(_httpService: Http){
        super();
        this._httpService = _httpService;
        this.message = "Paragraph error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    GetParagraphsText(paragraphId: string): Observable<Array<Text>> {
        return this._httpService.get(this.WEB_API_URL + '/' + paragraphId + '/texts', this.options)
        .pipe(
            map((response : Response) => <Array<Text>> response.json()),
            tap(data => console.log('Los textos del paragraph obtenido fueron : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    update(paragraphId:string, paragraph:Paragraph): Observable<Response>{
        return this._httpService.put(this.WEB_API_URL + '/' + paragraphId + '/', paragraph, this.options)
        .pipe(
            tap(data => console.log('Se modificó el paragraph : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    AddTextToParagraph(paragraphId: string, text: Text): Observable<Text>{
        return this._httpService.post(this.WEB_API_URL + '/' + paragraphId + '/texts', text, this.options)
        .pipe(
            map((response : Response) => <Text> response.json()),
            tap(data => console.log('Se agregó al paragraph el text : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    delete(paragraphId: string) : Observable<Response>{
        return this._httpService.delete(this.WEB_API_URL + '/' + paragraphId + '/', this.options)
        .pipe(
            tap(data => console.log('Se eliminó el paragraph : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}