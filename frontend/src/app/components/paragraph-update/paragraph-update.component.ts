import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material';
import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { ParagraphService } from '../../services/paragraph/paragraph.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { Paragraph } from '../../entities/paragraph/paragraph';

@Component({
    selector: 'app-paragraph-update',
    templateUrl: './paragraph-update.component.html',
    styleUrls: ['./paragraph-update.component.css'],
    providers: [StyleClassService, ParagraphService]
})
export class ParagraphUpdateComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    form: FormGroup;
    paragraph: Paragraph;

    constructor(
        public dialogRef: MatDialogRef<ParagraphUpdateComponent>,
        private styleClassService: StyleClassService,
        private paragraphService: ParagraphService,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.paragraph = this.data.paragraph;

        this.loadStyleClasses();
        this.loadCurrentStyleClass();

        this.form = this.fb.group({
            position: [this.paragraph.Position, Validators.required]
        });
    }

    private loadStyleClasses() {
        this.styleClassService.getAll()
            .subscribe(
                ((data: Array<StyleClass>) => this.loadStyleClassesWithDefault(data)),
                ((error: any) => console.log(error))
            )
    };

    private loadStyleClassesWithDefault(styleClasses: Array<StyleClass>) {
        this.styleClasses = styleClasses;
        let none: StyleClass = new StyleClass();
        this.styleClasses.push(none);
    }

    private loadCurrentStyleClass() {
        if(this.paragraph != null) {
            if(this.paragraph.StyleClass != null) {
                this.styleClassName = this.paragraph.StyleClass.Name;
            }
            else {
                this.styleClassName = 'None';
            }
        }
    };

    onSave() : void {
        if (this.styleClassName != 'None') {
            this.paragraph.StyleClass = new StyleClass();
            this.paragraph.StyleClass.Name = this.styleClassName;
        }
        else {
            this.paragraph.StyleClass = null;
        }

        this.paragraphService.update(this.paragraph.Id, this.paragraph)
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
