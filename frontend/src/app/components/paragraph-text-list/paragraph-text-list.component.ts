import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { ParagraphUpdateComponent } from '../../components/paragraph-update/paragraph-update.component';
import { ParagraphTextAddComponent } from '../paragraph-text-add/paragraph-text-add.component';
import { ParagraphTextUpdateComponent } from '../paragraph-text-update/paragraph-text-update.component';

import { DocumentService } from '../../services/document/document.service';
import { ParagraphService } from '../../services/paragraph/paragraph.service';

import { Text } from '../../entities/text/text';

@Component({
    selector: 'app-paragraph-text-list',
    templateUrl: './paragraph-text-list.component.html',
    styleUrls: ['./paragraph-text-list.component.scss'],
    providers: [DocumentService, ParagraphService]
})
export class ParagraphTextListComponent implements OnInit {

    @Input() paragraphId: string;
    texts: Array<Text>;
    updateDialogRef: MatDialogRef<ParagraphUpdateComponent>;
    addTextDialogRef: MatDialogRef<ParagraphTextAddComponent>;
    updateTextDialogRef: MatDialogRef<ParagraphTextUpdateComponent>;

    constructor(
        private paragraphService: ParagraphService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {

        this.texts = new Array<Text>();

        this.paragraphService.GetParagraphsText(this.paragraphId)
            .subscribe(
                ((data: Array<Text>) => this.texts = data),
                ((error: any) => console.log(error))
            )

    }

    hasText(): boolean {
        return this.texts[0] != null;
    }

    notHasText(): boolean {
        return this.texts[0] == null;
    }

    openAddDialog(): void {
        this.addTextDialogRef = this.dialog.open(ParagraphTextAddComponent, {
            data: { paragraphId: this.paragraphId },
        });
        this.addTextDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    openEditDialog(text: Text): void {
        this.updateTextDialogRef = this.dialog.open(ParagraphTextUpdateComponent, {
            data: { text: text },
        });
        this.addTextDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    onDelete(): void {
        this.paragraphService.delete(this.paragraphId)
            .subscribe(
                (() => this.pageRefresh()),
                ((error: any) => console.log(error))
            )
    }

    private pageRefresh(): void {
        location.reload();
    }
}