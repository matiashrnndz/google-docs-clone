import { Injectable } from "@angular/core";
import { Observable } from "rxjs"; 
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Document } from "../../entities/document/document";
import { Header } from "../../entities/header/header";
import { Paragraph } from "../../entities/paragraph/paragraph";
import { Footer } from "../../entities/footer/footer";

@Injectable()
export class DocumentService extends CatchService {

    private _httpService : Http;
    private WEB_API_URL : string = 'api/documents';
    private options : RequestOptions;

    constructor(_httpService: Http){
        super();
        this._httpService = _httpService;
        this.message = "Document error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    visualize(documentId:string, formatName:string): Observable<string> {
        this.options.headers.delete('Format-Name');
        this.options.headers.append('Format-Name', formatName);
        return this._httpService.get(this.WEB_API_URL + '/' + documentId + '/visualize', this.options)
        .pipe(
            map((response : Response) => <string> response.json()),
            tap(data => console.log('La visualización obtenida fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    get(documentId:string): Observable<Document> {
        return this._httpService.get(this.WEB_API_URL + '/' + documentId + '/', this.options)
        .pipe(
            map((response : Response) => <Document> response.json()),
            tap(data => console.log('El documento obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    update(documentId:string, document:Document): Observable<Document>{
        return this._httpService.put(this.WEB_API_URL + '/' + documentId + '/', document, this.options)
        .pipe(
            map((response : Response) => <Document> response.json()),
            tap(data => console.log('Se modificó el documento : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    delete(documentId:string) : Observable<Response>{
        return this._httpService.delete(this.WEB_API_URL + '/' + documentId + '/', this.options)
        .pipe(
            tap(data => console.log('Se eliminó el documento : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getHeader(documentId:string): Observable<Header> {
        return this._httpService.get(this.WEB_API_URL + '/' + documentId + '/headers', this.options)
        .pipe(
            map((response : Response) => <Header> response.json()),
            tap(data => console.log('El header obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
    
    addHeader(documentId:string, header:Header): Observable<Header>{
        return this._httpService.post(this.WEB_API_URL + '/' + documentId + '/headers', header, this.options)
        .pipe(
            map((response : Response) => <Header> response.json()),
            tap(data => console.log('Se agregó el header : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getParagraph(documentId:string): Observable<Array<Paragraph>> {
        return this._httpService.get(this.WEB_API_URL + '/' + documentId + '/paragraphs', this.options)
        .pipe(
            map((response : Response) => <Array<Paragraph>> response.json()),
            tap(data => console.log('El paragraph obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    addParagraph(documentId:string, paragraph:Paragraph): Observable<Paragraph>{
        return this._httpService.post(this.WEB_API_URL + '/' + documentId + '/paragraphs', paragraph, this.options)
        .pipe(
            map((response : Response) => <Paragraph> response.json()),
            tap(data => console.log('Se agregó el paragraph : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getFooter(documentId:string): Observable<Footer> {
        return this._httpService.get(this.WEB_API_URL + '/' + documentId + '/footers', this.options)
        .pipe(
            map((response : Response) => <Footer> response.json()),
            tap(data => console.log('El footer obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
    
    addFooter(documentId:string, footer:Footer): Observable<Footer>{
        return this._httpService.post(this.WEB_API_URL + '/' + documentId + '/footers', footer, this.options)
        .pipe(
            map((response : Response) => <Footer> response.json()),
            tap(data => console.log('Se agregó el footer : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}