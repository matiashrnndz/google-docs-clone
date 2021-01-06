import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { ParagraphService } from '../../services/paragraph/paragraph.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { Text } from '../../entities/text/text';

@Component({
    selector: 'app-paragraph-text-add',
    templateUrl: './paragraph-text-add.component.html',
    styleUrls: ['./paragraph-text-add.component.css'],
    providers: [StyleClassService, ParagraphService]
})
export class ParagraphTextAddComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<ParagraphTextAddComponent>,
        private styleClassService: StyleClassService,
        private paragraphService: ParagraphService,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.form = this.fb.group({
            textcontent: ['']
        });

        this.loadStyleClasses();
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

    onSubmit(): void {
        let text: Text = new Text();

        if (this.styleClassName == 'None') {
            text.StyleClass = null;
        }
        else {
            let styleClass: StyleClass = new StyleClass();
            styleClass.Name = this.styleClassName;
            text.StyleClass = styleClass;
        }

        text.TextContent = this.form.value.textcontent;

        this.paragraphService.AddTextToParagraph(this.data.paragraphId, text)
            .subscribe(
                ((data: Text) => console.log('text added to paragraph ' + this.data.paragraphId)),
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
