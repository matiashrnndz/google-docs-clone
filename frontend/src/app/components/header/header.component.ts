import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { HeaderUpdateComponent } from '../../components/header-update/header-update.component';

import { DocumentService } from '../../services/document/document.service';
import { HeaderService } from '../../services/header/header.service';

import { Header } from '../../entities/header/header';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
    providers: [DocumentService, HeaderService]
})
export class HeaderComponent implements OnInit {

    @Input() documentId: string;
    header: Header;

    updateDialogRef: MatDialogRef<HeaderUpdateComponent>

    constructor(
        private documentService: DocumentService,
        private headerService: HeaderService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.header = new Header();

        this.documentService.getHeader(this.documentId)
            .subscribe(
                ((data: Header) => this.header = data),
                ((error: any) => console.log(error))
            )
    }

    hasHeader(): Boolean {
        return this.header.Id != 'Default';
    }

    notHasHeader(): Boolean {
        return this.header.Id == 'Default';
    }

    onDelete(): void {
        let headerId: string = this.header.Id;
        if (headerId != 'Default') {
            this.header = new Header();
            this.headerService.delete(headerId)
                .subscribe(
                    (() => this.pageRefresh()),
                    ((error: any) => console.log(error))
                )
        }
    }

    onHeaderAdd(): void {
        if (this.header.Id == 'Default') {
            this.header.StyleClass = null;
            this.documentService.addHeader(this.documentId, this.header)
                .subscribe(
                    ((data: Header) => this.header = data),
                    ((error: any) => console.log(error))
                )
        }
        this.pageRefresh()
    }

    openEditDialog(): void {
        this.updateDialogRef = this.dialog.open(HeaderUpdateComponent, {
            data: { header: this.header },
        });
        this.updateDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    private pageRefresh(): void {
        location.reload();
    }
}