import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { MAT_DIALOG_DATA } from '@angular/material';
import { ShowErrorComponent } from '../show-error/show-error.component';

import { StyleClassService } from '../../services/style-class/style-class.service';
import { HeaderService } from '../../services/header/header.service';

import { StyleClass } from '../../entities/style-class/style-class';
import { Header } from '../../entities/header/header';

@Component({
    selector: 'app-header-update',
    templateUrl: './header-update.component.html',
    styleUrls: ['./header-update.component.css'],
    providers: [StyleClassService, HeaderService]
})
export class HeaderUpdateComponent extends ShowErrorComponent implements OnInit {

    styleClasses: Array<StyleClass>;
    styleClassName: string = 'None';
    header: Header;

    constructor(
        public dialogRef: MatDialogRef<HeaderUpdateComponent>,
        private styleClassService: StyleClassService,
        private headerService: HeaderService,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.header = this.data.header;
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
        if(this.header != null) {
            if(this.header.StyleClass != null) {
                this.styleClassName = this.header.StyleClass.Name;
            }
            else {
                this.styleClassName = 'None';
            }
        }
    };

    onSave() : void {
        if (this.styleClassName != 'None') {
            this.header.StyleClass = new StyleClass();
            this.header.StyleClass.Name = this.styleClassName;
        }
        else {
            this.header.StyleClass = null;
        }

        this.headerService.update(this.header.Id, this.header)
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
