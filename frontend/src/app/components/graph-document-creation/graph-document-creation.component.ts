import { Component } from '@angular/core';
import { GraphDocumentCreationService } from '../../services/graph-document-creation/graph-document-creation.service';
import { Coordinate } from '../../entities/coordinate/coordinate';

@Component({
    selector: 'app-graph-document-creation',
    templateUrl: './graph-document-creation.component.html',
    styleUrls: ['./graph-document-creation.component.scss'],
    providers: [GraphDocumentCreationService]
})
export class GraphDocumentCreationComponent {

    coordinates: Array<Coordinate>;
    startingDate: Date = new Date();
    lastestDate: Date = new Date();
    id = 'chart1';
    width = '100%';
    height = '500';
    type = 'column2d';
    dataFormat = 'json';
    dataSource;
    title = 'Documents created by users in a range of dates';

    constructor(
        private graphService: GraphDocumentCreationService
    ) {
        this.dataSource = {
            "chart": {
                "caption": "Quantity vs User",
                "subCaption": "",
                "numberprefix": "",
                "theme": "ocean"
            }
        }
    }

     onGraph() {
        let starting: string = this.startingDate.getFullYear() + '-' + 
        (this.startingDate.getMonth() + 1) + '-' + this.startingDate.getDate() + 'T00:00:00.00';

        let latest: string = this.lastestDate.getFullYear() + '-' + 
        (this.startingDate.getMonth() + 1) + '-' + this.lastestDate.getDate() + 'T00:00:00.00';

        this.graphService.get(starting, latest)
        .subscribe(
            ((data: Array<Coordinate>) => this.loadCoordinates(data)),
            ((error: any) => console.log(error))
        )
    }

    private loadCoordinates(coordinates: Array<any>): void {
        this.dataSource.data = new Array<any>();
        for (let i in coordinates) {
            this.dataSource.data[i] = new Coordinate(coordinates[i].Item1, coordinates[i].Item2);
        }
    }
}