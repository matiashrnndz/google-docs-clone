import { User } from "../user/user";
import { StyleClass } from "../style-class/style-class";

export class Document {

    Id: string;
    Creator: User;
    Title: string;
    StyleClass: StyleClass;
    CreationDate: string;
    LastModification: string;
    
}