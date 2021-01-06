import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { HeaderTextAddComponent } from '../header-text-add/header-text-add.component';
import { HeaderTextUpdateComponent } from '../header-text-update/header-text-update.component';

import { HeaderService } from '../../services/header/header.service';

import { Text } from '../../entities/text/text';

@Component({
    selector: 'app-header-text',
    templateUrl: './header-text.component.html',
    styleUrls: ['./header-text.component.scss']
})
export class HeaderTextComponent implements OnInit {

    @Input() headerId: string;
    text: Text;
    updateTextDialogRef: MatDialogRef<HeaderTextUpdateComponent>;
    addTextDialogRef: MatDialogRef<HeaderTextAddComponent>;

    constructor(
        private headerService: HeaderService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.text = new Text();

        this.headerService.GetHeadersText(this.headerId)
            .subscribe(
                ((data: Text) => this.text = data),
                ((error: any) => console.log(error))
            )
    }

     hasText() : boolean {
        return this.text.Id != 'Default';
    }

     notHasText() {
        return this.text.Id == 'Default';
    }

     openAddDialog() {
        this.addTextDialogRef = this.dialog.open(HeaderTextAddComponent, {
            data: { headerId: this.headerId },
        });
        this.addTextDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

     onDelete() {
        this.headerService.delete(this.headerId)
        .subscribe(
            (() => this.pageRefresh()),
            ((error: any) => console.log(error))
        )
    }

     openEditDialog() {
        this.updateTextDialogRef = this.dialog.open(HeaderTextUpdateComponent, {
            data: { text: this.text },
        });
        this.updateTextDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    private pageRefresh(): void {
        location.reload();
    }
}