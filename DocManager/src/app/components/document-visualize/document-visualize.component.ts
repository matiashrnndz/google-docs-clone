import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { DocumentService } from '../../services/document/document.service';
import { FormatService } from '../../services/format/format.service';

import { Format } from '../../entities/format/format';

@Component({
    selector: 'app-document-visualize',
    templateUrl: './document-visualize.component.html',
    styleUrls: ['./document-visualize.component.scss'],
    providers: [DocumentService, FormatService]
})
export class DocumentVisualizeComponent implements OnInit {

    formats: Array<Format>;
    documentId: string;
    currentFormatName: string;
    visualization: string = "It does not contain text.";

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private documentService: DocumentService,
        private formatService: FormatService,
        private location: Location
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.documentId = params['id']
        });

        this.loadFormats();
        this.loadCurrentFormat();
    }

    loadFormats() {
        this.formatService.getAll()
            .subscribe(
                ((data: Array<Format>) => this.loadFormatsWithDefault(data)),
                ((error: any) => console.log(error))
            )
    }

    loadFormatsWithDefault(formats: Array<Format>) {
        this.formats = formats;
        let none: Format = new Format();
        this.formats.push(none);
    }

    loadCurrentFormat() {
        this.currentFormatName = 'None';
    }

    onView() {
        this.documentService.visualize(this.documentId, this.currentFormatName)
            .subscribe(
                ((data: string) => this.visualization = data),
                ((error: any) => console.log(error))
            )
    }
}