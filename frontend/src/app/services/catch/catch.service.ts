import { Observable, throwError } from "rxjs"; 
import { Http, Response} from '@angular/http';

export class CatchService{

    protected message:string = "";
        
    protected handleError(response: Response) {
        console.error(response);
        var errorMsg : any;

        if(response.status==0){
            errorMsg = "Falló la conexión.";
        }
        else if(response.status==401){
            errorMsg = "Usuario no autorizado.";
        }
        else{
            try{
                errorMsg = response.json()["Message"];
            }
            catch(e){
                errorMsg = response["_body"];
            }
        }

        return Observable.throw(errorMsg || this.message);
    }
}