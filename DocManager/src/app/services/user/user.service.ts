import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { User } from "../../entities/user/user";
import { Document } from "../../entities/document/document";
import { DocumentFilterAndOrder } from "../../entities/document-filter-and-order/header";

@Injectable()
export class UserService extends CatchService {

    private WEB_API_URL: string = 'api/users';
    private options: RequestOptions;

    constructor(
        private httpService: Http
    ) {
        super();
        this.message = "User error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    getAll(): Observable<Array<User>> {
        return this.httpService.get(this.WEB_API_URL, this.options)
            .pipe(
                map((response: Response) => <Array<User>>response.json()),
                tap(data => console.log('Users : ' + JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    get(userEmail:string): Observable<User> {
        return this.httpService.get(this.WEB_API_URL + '/' + userEmail + '/', this.options)
        .pipe(
            map((response : Response) => <User> response.json()),
            tap(data => console.log('El dato obtenido fue : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    add(user:User): Observable<User>{
        return this.httpService.post(this.WEB_API_URL, user, this.options)
        .pipe(
            map((response : Response) => <User> response.json()),
            tap(data => console.log('Se agregó el usuario : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    update(email: string, user:User): Observable<User>{
        return this.httpService.put(this.WEB_API_URL + '/' + email + '/', user, this.options)
        .pipe(
            map((response : Response) => <User> response.json()),
            tap(data => console.log('Se modificó el usuario : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    delete(userEmail:string) : Observable<Response>{
        return this.httpService.delete(this.WEB_API_URL + '/' + userEmail + '/', this.options)
        .pipe(
            tap(data => console.log('Se eliminó el usuario : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getDocuments(userEmail:string): Observable<Array<Document>> {
        return this.httpService.get(this.WEB_API_URL  + '/' + userEmail + '/documents', this.options)
        .pipe(
            map((response : Response) => <Array<Document>> response.json()),
            tap(data => console.log('Los documentos que obtuvimos fueron : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getDocumentsFilteredAndOrdered(userEmail: string, document: DocumentFilterAndOrder): Observable<Array<Document>> {
        return this.httpService.post(this.WEB_API_URL  + '/' + userEmail +'/', document, this.options)
        .pipe(
            map((response : Response) => <Array<Document>> response.json()),
            tap(data => console.log('Los documentos filtrados y ordenados que obtuvimos fueron : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    addDocument(userEmail:string, document:Document): Observable<Document>{
        return this.httpService.post(this.WEB_API_URL + '/' + userEmail + '/documents', document, this.options)
        .pipe(
            map((response : Response) => <Document> response.json()),
            tap(data => console.log('Se agregó el documento : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}