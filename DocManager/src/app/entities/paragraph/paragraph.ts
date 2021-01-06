import { Document } from "../document/document";
import { StyleClass } from "../style-class/style-class";

export class Paragraph {
    
    Id : string;
    Document : Document;
    StyleClass : StyleClass;
    Position : number;

    constructor() {
        this.Id = 'Default';
    }
}