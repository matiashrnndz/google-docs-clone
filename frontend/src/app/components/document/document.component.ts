import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material';

import { ParagraphUpdateComponent } from '../../components/paragraph-update/paragraph-update.component';

import { DocumentService } from '../../services/document/document.service';
import { ParagraphService } from '../../services/paragraph/paragraph.service';

import { Document } from '../../entities/document/document';
import { Paragraph } from '../../entities/paragraph/paragraph';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss'],
    providers: [DocumentService, ParagraphService]
})
export class DocumentComponent implements OnInit {

    document: Document;
    documentId: string;
    paragraphs: Array<Paragraph>;
    updateDialogRef: MatDialogRef<ParagraphUpdateComponent>;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private documentService: DocumentService,
        private paragraphService: ParagraphService,
        private location: Location,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.documentId = params['id']
        });

        this.document = new Document();

        this.documentService.get(this.documentId)
            .subscribe(
                ((data: Document) => this.document = data),
                ((error: any) => console.log(error))
            )

        this.paragraphs = new Array<Paragraph>();

        this.documentService.getParagraph(this.documentId)
            .subscribe(
                ((data: Array<Paragraph>) => this.paragraphs = data),
                ((error: any) => console.log(error))
            )
    }

    onParagraphAdd(): void {
        let paragraph: Paragraph = new Paragraph();
        paragraph.StyleClass = null;
        this.documentService.addParagraph(this.documentId, paragraph)
            .subscribe(
                ((data: Paragraph) => this.paragraphs.push(data)),
                ((error: any) => console.log(error))
            )
    }

    onParagraphDelete(paragraphId: string): void {
        this.paragraphService.delete(paragraphId)
            .subscribe(
                (() => this.pageRefresh()),
                ((error: any) => console.log(error))
            )
    }

    openParagraphEditDialog(paragraphId: string): void {
        this.updateDialogRef = this.dialog.open(ParagraphUpdateComponent, {
            data: { paragraph: this.getParagraph(paragraphId) },
        });
        this.updateDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    getParagraph(paragraphId: string): Paragraph {
        var toGet = this.paragraphs.filter(function (paragraph) {
            return paragraph.Id === paragraphId;
        })[0];

        return toGet;
    }

    private pageRefresh(): void {
        location.reload();
    }
    
}