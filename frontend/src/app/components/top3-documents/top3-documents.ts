import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material';

import { DocumentAddComponent } from '../document-add/document-add.component';

import { TopService } from '../../services/top/top.service';

import { Document } from '../../entities/document/document';

@Component({
    selector: 'app-top3-documents',
    templateUrl: './top3-documents.html',
    styleUrls: ['./top3-documents.scss'],
    providers: [TopService]
})
export class Top3DocumentsComponent implements OnInit {

    documents: Array<Document> = null;
    addDialogRef: MatDialogRef<DocumentAddComponent>

    constructor(
        private router: Router,
        private topService: TopService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.topService.getTop3Documents().subscribe(
            ((data: Array<Document>) => this.loadDocuments(data)),
            ((error: any) => console.log(error))
        )
    }

    private loadDocuments(data: Array<Document>): void {
        this.documents = data;
        console.log(this.documents);
    }

    onView(documentId: string): void {
        this.router.navigate(['documents', documentId, 'visualize']);
    }

    onComments(documentId: string): void {
        this.router.navigate(['comments', documentId]);
    }

}