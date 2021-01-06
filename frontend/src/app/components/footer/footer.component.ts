import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { FooterUpdateComponent } from '../../components/footer-update/footer-update.component';

import { DocumentService } from '../../services/document/document.service';
import { FooterService } from '../../services/footer/footer.service';

import { Footer } from '../../entities/footer/footer';

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss'],
    providers: [DocumentService, FooterService]
})
export class FooterComponent implements OnInit {

    @Input() documentId: string;
    footer: Footer;

    updateDialogRef: MatDialogRef<FooterUpdateComponent>

    constructor(
        private documentService: DocumentService,
        private footerService: FooterService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.footer = new Footer();

        this.documentService.getFooter(this.documentId)
            .subscribe(
                ((data: Footer) => this.footer = data),
                ((error: any) => console.log(error))
            )
    }

    hasFooter(): Boolean {
        return this.footer.Id != 'Default';
    }

    notHasFooter(): Boolean {
        return this.footer.Id == 'Default';
    }

    onDelete(): void {
        let footerId: string = this.footer.Id;
        if (footerId != 'Default') {
            this.footer = new Footer();
            this.footerService.delete(footerId)
                .subscribe(
                    (() => this.pageRefresh()),
                    ((error: any) => console.log(error))
                )
        }
    }

    onFooterAdd(): void {
        if (this.footer.Id == 'Default') {
            this.footer.StyleClass = null;
            this.documentService.addFooter(this.documentId, this.footer)
                .subscribe(
                    ((data: Footer) => this.footer = data),
                    ((error: any) => console.log(error))
                )
        }
        this.pageRefresh()
    }

    openEditDialog(): void {
        this.updateDialogRef = this.dialog.open(FooterUpdateComponent, {
            data: { footer: this.footer },
        });
        this.updateDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    pageRefresh(): void {
        location.reload();
    }
}