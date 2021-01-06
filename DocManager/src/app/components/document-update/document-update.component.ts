import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material';
import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { DocumentService } from '../../services/document/document.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { Document } from '../../entities/document/document';

@Component({
    selector: 'app-document-update',
    templateUrl: './document-update.component.html',
    styleUrls: ['./document-update.component.css'],
    providers: [StyleClassService, DocumentService]
})
export class DocumentUpdateComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    form: FormGroup;
    document: Document;

    constructor(
        public dialogRef: MatDialogRef<DocumentUpdateComponent>,
        private styleClassService: StyleClassService,
        private documentService: DocumentService,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.document = this.data.document;

        this.loadStyleClasses();
        this.loadCurrentStyleClass();

        this.form = this.fb.group({
            title: [this.document.Title, Validators.required]
        });
    }

    loadStyleClasses() {
        this.styleClassService.getAll()
            .subscribe(
                ((data: Array<StyleClass>) => this.loadStyleClassesWithDefault(data)),
                ((error: any) => console.log(error))
            )
    };

    loadStyleClassesWithDefault(styleClasses: Array<StyleClass>) {
        this.styleClasses = styleClasses;
        let none: StyleClass = new StyleClass();
        this.styleClasses.push(none);
    }

    loadCurrentStyleClass() {
        if (this.document != null) {
            if (this.document.StyleClass != null) {
                this.styleClassName = this.document.StyleClass.Name;
            }
            else {
                this.styleClassName = 'None';
            }
        }
    };

    onSave(): void {
        if (this.styleClassName != 'None') {
            this.document.StyleClass = new StyleClass();
            this.document.StyleClass.Name = this.styleClassName;
        }
        else {
            this.document.StyleClass = null;
        }

        this.documentService.update(this.document.Id, this.document)
            .subscribe(
                (() => console.log('')),
                ((error: any) => console.log(error))
            )

        this.closeDialog();
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    closeDialog(): void {
        this.dialogRef.close();
    }
}
