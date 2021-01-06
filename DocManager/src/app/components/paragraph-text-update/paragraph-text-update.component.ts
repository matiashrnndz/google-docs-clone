import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { MAT_DIALOG_DATA } from '@angular/material';
import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { TextService } from '../../services/text/text.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-paragraph-text-update',
    templateUrl: './paragraph-text-update.component.html',
    styleUrls: ['./paragraph-text-update.component.css'],
    providers: [StyleClassService, TextService]
})
export class ParagraphTextUpdateComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<ParagraphTextUpdateComponent>,
        private styleClassService: StyleClassService,
        private textService: TextService,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.loadStyleClasses();
        this.loadCurrentStyleClass();

        this.form = this.fb.group({
            textcontent: [this.data.text.TextContent],
            position: [this.data.text.Position]
        });
    }

    private loadStyleClasses() {
        this.styleClassService.getAll()
            .subscribe(
                ((data: Array<StyleClass>) => this.loadStyleClassesWithDefault(data)),
                ((error: any) => console.log(error))
            )
    }

    private loadStyleClassesWithDefault(styleClasses: Array<StyleClass>) {
        this.styleClasses = styleClasses;
        let none: StyleClass = new StyleClass();
        this.styleClasses.push(none);
    }

    private loadCurrentStyleClass() {
        if(this.data.text != null) {
            if(this.data.text.StyleClass != null) {
                this.styleClassName = this.data.text.StyleClass.Name;
            }
            else {
                this.styleClassName = 'None';
            }
        }
    }

    onSubmit(): void {
        if (this.styleClassName == 'None') {
            this.data.text.StyleClass = null;
        }
        else {
            let styleClass: StyleClass = new StyleClass();
            styleClass.Name = this.styleClassName;
            this.data.text.StyleClass = styleClass;
        }

        this.data.text.TextContent = this.form.value.textcontent;
        this.data.text.Position = this.form.value.position;

        this.textService.update(this.data.text.Id, this.data.text)
            .subscribe(
                (() => console.log('text updated :' + this.data.text.Id)),
                ((error: any) => console.log(error))
            )

        this.closeDialog();
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    cancel(): void {
        this.form.setValue({ textcontent: this.data.text.TextContent, position: this.data.text.Position });
        this.loadCurrentStyleClass();
        this.closeDialog();
    }

    closeDialog(): void {
        this.dialogRef.close();
    }
}
