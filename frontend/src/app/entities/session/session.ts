import { User } from "../user/user";

export class Session {
    
    Token : string;
    User : User;

    constructor(token:string, user:User) {
        this.Token = token;
        this.User = user;
    }
}