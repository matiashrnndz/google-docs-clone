import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';

import { DocumentAddComponent } from '../document-add/document-add.component';

import { DocumentService } from '../../services/document/document.service';
import { UserService } from '../../services/user/user.service';

import { Document } from '../../entities/document/document';

@Component({
    selector: 'app-friend-document-list',
    templateUrl: './friend-document-list.component.html',
    styleUrls: ['./friend-document-list.component.scss'],
    providers: [DocumentService, UserService]
})
export class FriendDocumentListComponent implements OnInit {

    documents: Array<Document> = null;
    addDialogRef: MatDialogRef<DocumentAddComponent>
    userEmail: string;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private documentsService: DocumentService,
        private usersService: UserService,
        private location: Location,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.userEmail = params['email']
        });

        this.usersService.getDocuments(this.userEmail).subscribe(
            ((data: Array<Document>) => this.loadDocuments(data)),
            ((error: any) => console.log(error))
        )
    }

    private loadDocuments(data: Array<Document>): void {
        this.documents = data;
        console.log(this.documents);
    }

     openAddDialog(): void {
        this.addDialogRef = this.dialog.open(DocumentAddComponent);
        this.addDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    private pageRefresh(): void {
        location.reload();
    }
    
     onView(documentId: string): void {
        this.router.navigate(['documents', documentId, 'visualize']);
    }
    
     onComments(documentId: string): void {
        this.router.navigate(['comments', documentId]);
    }
    
}