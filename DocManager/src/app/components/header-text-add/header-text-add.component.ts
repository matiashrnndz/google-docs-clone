import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { HeaderService } from '../../services/header/header.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { Text } from '../../entities/text/text';

@Component({
    selector: 'app-header-text-add',
    templateUrl: './header-text-add.component.html',
    styleUrls: ['./header-text-add.component.css'],
    providers: [StyleClassService, HeaderService]
})
export class HeaderTextAddComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<HeaderTextAddComponent>,
        private styleClassService: StyleClassService,
        private headerService: HeaderService,
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

        this.headerService.AddTextToHeader(this.data.headerId, text)
            .subscribe(
                ((data: Text) => console.log('text added to header ' + this.data.headerId)),
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
