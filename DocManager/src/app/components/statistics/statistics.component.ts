import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-statistics',
    templateUrl: './statistics.component.html',
    styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent {

    constructor(
        private router: Router
    ) {

    }

    onDocumentCreation(): void {
        this.router.navigate(['statistics', 'documentcreationgraphic']);
    }

    onDocumentModification(): void {
        this.router.navigate(['statistics', 'documentmodificationgraphic']);
    }
}