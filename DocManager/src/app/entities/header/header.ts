import { Document } from "../document/document";
import { StyleClass } from "../style-class/style-class";

export class Header {

    Id: string;
    Document: Document;
    StyleClass: StyleClass;

    constructor() {
        this.Id = 'Default';
    }
}