import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { MAT_DIALOG_DATA } from '@angular/material';
import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { FooterService } from '../../services/footer/footer.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { Footer } from '../../entities/footer/footer';

@Component({
    selector: 'app-footer-update',
    templateUrl: './footer-update.component.html',
    styleUrls: ['./footer-update.component.css'],
    providers: [StyleClassService, FooterService]
})
export class FooterUpdateComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    footer: Footer;

    constructor(
        public dialogRef: MatDialogRef<FooterUpdateComponent>,
        private styleClassService: StyleClassService,
        private footerService: FooterService,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.footer = this.data.footer;
        this.loadStyleClasses();
        this.loadCurrentStyleClass();
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
        if(this.footer != null) {
            if(this.footer.StyleClass != null) {
                this.styleClassName = this.footer.StyleClass.Name;
            }
            else {
                this.styleClassName = 'None';
            }
        }
    };

    onSave() : void {
        if (this.styleClassName != 'None') {
            this.footer.StyleClass = new StyleClass();
            this.footer.StyleClass.Name = this.styleClassName;
        }
        else {
            this.footer.StyleClass = null;
        }

        this.footerService.update(this.footer.Id, this.footer)
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
