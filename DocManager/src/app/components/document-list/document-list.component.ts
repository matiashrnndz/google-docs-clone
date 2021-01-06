import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { DocumentAddComponent } from '../document-add/document-add.component';
import { DocumentUpdateComponent } from '../document-update/document-update.component';

import { DocumentService } from '../../services/document/document.service';
import { UserService } from '../../services/user/user.service';

import { Document } from '../../entities/document/document';
import { DocumentFilterAndOrder } from '../../entities/document-filter-and-order/header';

@Component({
    selector: 'app-document-list',
    templateUrl: './document-list.component.html',
    styleUrls: ['./document-list.component.scss'],
    providers: [DocumentService, UserService]
})
export class DocumentListComponent implements OnInit {

    documents: Array<Document> = null;
    addDialogRef: MatDialogRef<DocumentAddComponent>
    updateDialogRef: MatDialogRef<DocumentUpdateComponent>

    formSubmitAttempt: boolean;
    form: FormGroup;

    selectedFilter: string = 'Title';
    selectedOrder: string = 'Descending';
    selectedDate: Date = new Date();

    noIdFilter: string = "11111111-1111-1111-1111-111111111111";
    noTitleFilter: string = '';
    noDateTimeFilter: string = '9999-12-31T00:00:00.00';

    lastId: string = this.noIdFilter;
    lastTitle: string = this.noTitleFilter;
    lastCreationDate: string = this.noDateTimeFilter;
    lastLastModification: string = this.noDateTimeFilter;

    constructor(
        private router: Router,
        private documentsService: DocumentService,
        private usersService: UserService,
        private location: Location,
        public fb: FormBuilder,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        let email: string = localStorage.getItem('email');
        this.usersService.getDocuments(email).subscribe(
            ((data: Array<Document>) => this.loadDocuments(data)),
            ((error: any) => console.log(error))
        )

        this.form = this.fb.group({
            filter: ['', Validators.required]
        });
    }

    isFieldInvalid(field: string) {
        return (
            (!this.form.get(field).valid && this.form.get(field).touched) ||
            (this.form.get(field).untouched && this.formSubmitAttempt)
        );
    }

    loadDocuments(data: Array<Document>): void {
        this.documents = data;
        console.log(this.documents);
    }

    openAddDialog(): void {
        this.addDialogRef = this.dialog.open(DocumentAddComponent);
        this.addDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    pageRefresh(): void {
        location.reload();
    }

    onView(documentId: string): void {
        this.router.navigate(['documents', documentId, 'visualize']);
    }

    onEdit(documentId: string): void {
        this.router.navigate(['documents', documentId]);
    }

    onComments(documentId: string): void {
        this.router.navigate(['comments', documentId]);
    }

    onInfo(documentId: string): void {
        this.updateDialogRef = this.dialog.open(DocumentUpdateComponent, {
            data: { document: this.getDocument(documentId) },
        });
        this.updateDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    getDocument(documentId: string): Document {
        var toGet = this.documents.filter(function (document) {
            return document.Id === documentId;
        })[0];

        return toGet;
    }

    onTextFilter(): void {
        let documentFilteredAndOrdered: DocumentFilterAndOrder = new DocumentFilterAndOrder();

        documentFilteredAndOrdered.DocumentFilteredData = new Document();

        documentFilteredAndOrdered.DocumentFilteredData.CreationDate = this.noDateTimeFilter;
        documentFilteredAndOrdered.DocumentFilteredData.LastModification = this.noDateTimeFilter;
        this.lastCreationDate = this.noDateTimeFilter;
        this.lastLastModification = this.noDateTimeFilter;

        if (this.selectedFilter == 'Id') {
            documentFilteredAndOrdered.DocumentFilteredData.Id = this.form.value.filter;
            documentFilteredAndOrdered.DocumentFilteredData.Title = this.noTitleFilter;
            this.lastId = this.form.value.filter;
            this.lastTitle = this.noTitleFilter;
        }
        else if (this.selectedFilter == 'Title') {
            documentFilteredAndOrdered.DocumentFilteredData.Id = this.noIdFilter;
            documentFilteredAndOrdered.DocumentFilteredData.Title = this.form.value.filter;
            this.lastId = this.noIdFilter;
            this.lastTitle = this.form.value.filter;
        }

        this.onFilterChange(documentFilteredAndOrdered);
    }

    onDateFilter(): void {
        let documentFilteredAndOrdered: DocumentFilterAndOrder = new DocumentFilterAndOrder();

        documentFilteredAndOrdered.DocumentFilteredData = new Document();

        documentFilteredAndOrdered.DocumentFilteredData.Id = this.noIdFilter;
        documentFilteredAndOrdered.DocumentFilteredData.Title = this.noTitleFilter;
        this.lastId = this.noIdFilter;
        this.lastTitle = this.noTitleFilter;

        if (this.selectedFilter == 'CreationDate') {
            let date: string = this.selectedDate.getFullYear() + '-' +
                (this.selectedDate.getMonth() + 1) + '-' + this.selectedDate.getDate() + 'T00:00:00.00';

            documentFilteredAndOrdered.DocumentFilteredData.CreationDate = date;
            documentFilteredAndOrdered.DocumentFilteredData.LastModification = this.noDateTimeFilter;
            this.lastCreationDate = date;
            this.lastLastModification = this.noDateTimeFilter;
        }
        else if (this.selectedFilter == 'LastModification') {
            let date: string = this.selectedDate.getFullYear() + '-' +
                (this.selectedDate.getMonth() + 1) + '-' + this.selectedDate.getDate() + 'T00:00:00.00';

            documentFilteredAndOrdered.DocumentFilteredData.LastModification = date;
            documentFilteredAndOrdered.DocumentFilteredData.CreationDate = this.noDateTimeFilter;
            this.lastLastModification = date;
            this.lastCreationDate = this.noDateTimeFilter;
        }

        this.onFilterChange(documentFilteredAndOrdered);
    }

    public onFilterChange(documentFilteredAndOrdered: DocumentFilterAndOrder) {
        let userEmail: string = localStorage.getItem('email');

        if (this.selectedOrder == 'Ascending') {
            documentFilteredAndOrdered.IsDesc = false;
        }
        else {
            documentFilteredAndOrdered.IsDesc = true;
        }

        documentFilteredAndOrdered.OrderBy = this.selectedFilter;

        this.usersService.getDocumentsFilteredAndOrdered(userEmail, documentFilteredAndOrdered)
            .subscribe(
                ((data: Array<Document>) => this.documents = data),
                ((error: any) => console.log(error))
            );
    };

    onOrder(): void {
        let documentFilteredAndOrdered: DocumentFilterAndOrder = new DocumentFilterAndOrder();

        documentFilteredAndOrdered.DocumentFilteredData = new Document();

        documentFilteredAndOrdered.DocumentFilteredData.Id = this.lastId;
        documentFilteredAndOrdered.DocumentFilteredData.Title = this.lastTitle;
        documentFilteredAndOrdered.DocumentFilteredData.CreationDate = this.lastCreationDate;
        documentFilteredAndOrdered.DocumentFilteredData.LastModification = this.lastLastModification;

        this.onFilterChange(documentFilteredAndOrdered);
    }

    isDateFilterSelected(): boolean {
        return (this.selectedFilter == 'CreationDate') || (this.selectedFilter == 'LastModification');
    }

    onDelete(documentId: string): void {
        console.log(documentId);
        this.documentsService.delete(documentId).subscribe(
            (() => this.pageRefresh()),
            ((error: any) => console.log(error))
        )
    }

}