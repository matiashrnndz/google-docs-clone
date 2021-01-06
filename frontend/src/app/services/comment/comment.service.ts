import { Injectable } from "@angular/core";
import { Observable } from "rxjs"; 
import { Http, Response, RequestOptions } from "@angular/http";
import { map, tap, catchError } from 'rxjs/operators';

import { CatchService } from '../catch/catch.service';
import { AppConfig } from '../../app-config';

import { Comment } from "../../entities/comment/comment";

@Injectable()
export class CommentService extends CatchService {

    private _httpService : Http;
    private WEB_API_URL : string = 'api/comments';
    private options : RequestOptions;

    constructor(_httpService: Http){
        super();
        this._httpService = _httpService;
        this.message = "Comment error";
        var config = new AppConfig();
        this.WEB_API_URL = config.getUrl() + this.WEB_API_URL;
        this.options = config.getRequestOptions();
    }

    getAllByDocument(documentId:string): Observable<Array<Comment>> {
        return this._httpService.get(this.WEB_API_URL + '/' + documentId, this.options)
        .pipe(
            map((response : Response) => <Array<Comment>> response.json()),
            tap(data => console.log('Los comentarios obtenidos fueron : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    add(documentId:string, comment:Comment): Observable<Comment>{
        return this._httpService.post(this.WEB_API_URL + '/' + documentId, comment, this.options)
        .pipe(
            map((response : Response) => <Comment> response.json()),
            tap(data => console.log('Se agregó el comentario : ' + JSON.stringify(data))),
            catchError(this.handleError)
        );
    }
}