import {Headers, RequestOptions} from "@angular/http"

export class AppConfig{

    IP : string;
    Port: string;

    constructor(){
        this.IP = "localhost";
        this.Port = "10073";
    }

    getRequestOptions(): RequestOptions{
        let headers = new Headers();
        headers.append('Authorization', localStorage.getItem('token'));
        
        return new RequestOptions({ headers : headers });
    }

    getUrl():string{
        return "http://" + this.IP + ":" + this.Port + "/";
    }
}