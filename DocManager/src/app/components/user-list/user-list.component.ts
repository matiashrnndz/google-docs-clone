import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material';

import { UserAddComponent } from '../user-add/user-add.component';
import { UserUpdateComponent } from '../user-update/user-update.component';

import { UserService } from '../../services/user/user.service';

import { User } from '../../entities/user/user';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss'],
    providers: [UserService]
})
export class UserListComponent implements OnInit {

    users: Array<User> = null;
    addDialogRef: MatDialogRef<UserAddComponent>
    updateDialogRef: MatDialogRef<UserUpdateComponent>

    constructor(
        private router: Router,
        private usersService: UserService,
        private location: Location,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.usersService.getAll().subscribe(
            ((data: Array<User>) => this.loadUsers(data)),
            ((error: any) => console.log(error))
        )
    }

    openAddDialog(): void {
        this.addDialogRef = this.dialog.open(UserAddComponent);
        this.addDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    openUpdateDialog(email: string): void {
        this.updateDialogRef = this.dialog.open(UserUpdateComponent, {
            data: { userEmail: email },
        });
        this.updateDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    private loadUsers(data: Array<User>): void {
        this.users = data;
        console.log(this.users);
    }

    onView(email: string): void {
        this.router.navigate(['users', email]);
    }

    onDelete(email: string): void {
        this.usersService.delete(email).subscribe(
            (() => this.pageRefresh()),
            ((error: any) => console.log(error))
        )
    }

    private pageRefresh(): void {
        location.reload();
    }
}