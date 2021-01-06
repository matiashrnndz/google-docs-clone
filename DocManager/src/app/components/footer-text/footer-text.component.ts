import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { FooterTextAddComponent } from '../footer-text-add/footer-text-add.component';
import { FooterTextUpdateComponent } from '../footer-text-update/footer-text-update.component';

import { FooterService } from '../../services/footer/footer.service';

import { Text } from '../../entities/text/text';

@Component({
    selector: 'app-footer-text',
    templateUrl: './footer-text.component.html',
    styleUrls: ['./footer-text.component.scss']
})
export class FooterTextComponent implements OnInit {

    @Input() footerId: string;
    text: Text;
    updateTextDialogRef: MatDialogRef<FooterTextUpdateComponent>;
    addTextDialogRef: MatDialogRef<FooterTextAddComponent>;

    constructor(
        private footerService: FooterService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.text = new Text();

        this.footerService.GetFootersText(this.footerId)
            .subscribe(
                ((data: Text) => this.text = data),
                ((error: any) => console.log(error))
            )
    }

    hasText(): boolean {
        return this.text.Id != 'Default';
    }

    notHasText() {
        return this.text.Id == 'Default';
    }

    openAddDialog() {
        this.addTextDialogRef = this.dialog.open(FooterTextAddComponent, {
            data: { footerId: this.footerId },
        });
        this.addTextDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    onDelete() {
        this.footerService.delete(this.footerId)
            .subscribe(
                (() => this.pageRefresh()),
                ((error: any) => console.log(error))
            )
    }

    openEditDialog() {
        this.updateTextDialogRef = this.dialog.open(FooterTextUpdateComponent, {
            data: { text: this.text },
        });
        this.updateTextDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    pageRefresh(): void {
        location.reload();
    }
}