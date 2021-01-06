import { Component, OnInit } from '@angular/core';

import { GraphDocumentModificationService } from '../../services/graph-document-modification/graph-document-modification.service';
import { UserService } from '../../services/user/user.service';

import { Coordinate } from '../../entities/coordinate/coordinate';
import { User } from '../../entities/user/user';

@Component({
    selector: 'app-graph-document-modification',
    templateUrl: './graph-document-modification.component.html',
    styleUrls: ['./graph-document-modification.component.scss'],
    providers: [GraphDocumentModificationService, UserService]
})
export class GraphDocumentModificationComponent implements OnInit {

    coordinates: Array<Coordinate>;
    startingDate: Date = new Date();
    lastestDate: Date = new Date();
    users: Array<User>;
    currentUserEmail: string = 'admin@admin.com';


    id = 'chart1';
    width = '100%';
    height = '500';
    type = 'column2d';
    dataFormat = 'json';
    dataSource;
    title = 'Documents modificated by users by day in a range of dates';

    constructor(
        private graphService: GraphDocumentModificationService,
        private userService: UserService
    ) {
        this.dataSource = {
            "chart": {
                "caption": "Quantity vs Days",
                "subCaption": "",
                "numberprefix": "",
                "theme": "ocean"
            }
        }
    }

    ngOnInit(): void {
        this.users = new Array<User>();

        this.loadUsers();
        this.loadCurrentUserEmail();
    }

    private loadUsers() {
        this.userService.getAll()
            .subscribe(
                ((data: Array<User>) => this.users = data),
                ((error: any) => console.log(error))
            )
    };

    private loadCurrentUserEmail() {
        this.currentUserEmail = 'admin@admin.com'
    };

    onGraph() {
        let starting: string = this.startingDate.getFullYear() + '-' +
            (this.startingDate.getMonth() + 1) + '-' + this.startingDate.getDate() + 'T00:00:00.00';

        let latest: string = this.lastestDate.getFullYear() + '-' +
            (this.startingDate.getMonth() + 1) + '-' + this.lastestDate.getDate() + 'T00:00:00.00';

        this.graphService.get(starting, latest, this.currentUserEmail)
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