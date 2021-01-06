import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ShowErrorComponent } from '../show-error/show-error.component';
import { UserService } from '../../services/user/user.service';

import { Document } from '../../entities/document/document';

@Component({
    selector: 'app-document-add',
    templateUrl: './document-add.component.html',
    styleUrls: ['./document-add.component.css']
})
export class DocumentAddComponent extends ShowErrorComponent implements OnInit {
    
    private formSubmitAttempt: boolean;
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<DocumentAddComponent>,
        private fb: FormBuilder,
        private userService: UserService
    ) { 
        super();
    }

    ngOnInit() {
        this.form = this.fb.group({
            title: ['', Validators.required]
        });
    }

    isFieldInvalid(field: string) {
        return (
            (!this.form.get(field).valid && this.form.get(field).touched) ||
            (this.form.get(field).untouched && this.formSubmitAttempt)
        );
    }

    onSubmit() {
        if (this.form.valid) {
            let newDocument : Document = new Document();
            newDocument.Title = this.form.value.title;
            let email : string = localStorage.getItem('email');
            this.userService.addDocument(email, newDocument)
                .subscribe(
                    ((document: Document) => this.onSuccessful(document)),
                    ((error: any) => this.handleLoginError(error))
                )
        }
        this.formSubmitAttempt = true;
    }

    handleLoginError(error: any): void {
        this.handleError(error);
        this.form.setValue({ title: '' });
    }

    onSuccessful(document: Document): void {
        console.log("successful" + document);
        this.closeDialog();
    }
    
    onNoClick(): void {
        this.dialogRef.close();
    }

    closeDialog(): void {
        this.form.setValue({ title: '' });
        this.dialogRef.close();
    }
}
