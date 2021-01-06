import { User } from "../user/user";

export class FriendRequest {
    
    Id: string;
    Accepted: boolean;
    Sender: User;
    Receiver: User;

    constructor() { }
    
}