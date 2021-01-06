import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material';

import { FriendInviteComponent } from '../friend-invite/friend-invite.component';

import { FriendService } from '../../services/friend/friend.service';
import { UserService } from '../../services/user/user.service';

import { User } from '../../entities/user/user';
import { FriendRequest } from '../../entities/friend-request/friend-request';

@Component({
    selector: 'app-friend-list',
    templateUrl: './friend-list.component.html',
    styleUrls: ['./friend-list.component.scss'],
    providers: [UserService, FriendService]
})
export class FriendListComponent implements OnInit {

    friends: Array<User> = null;
    friendRequests: Array<User> = null;
    inviteDialogRef: MatDialogRef<FriendInviteComponent>

    constructor(
        private router: Router,
        private usersService: UserService,
        private friendsService: FriendService,
        private location: Location,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        let email: string = localStorage.getItem('email');
        this.friendsService.getFriends(email)
        .subscribe(
            ((data: Array<User>) => this.loadFriends(data)),
            ((error: any) => console.log(error))
        )
        this.friendsService.getRequests(email)
        .subscribe(
            ((data: Array<User>) => this.loadFriendRequests(data)),
            ((error: any) => console.log(error))
        )
    }

    private loadFriends(data: Array<User>): void {
        this.friends = data;
        console.log(this.friends);
    }

    private loadFriendRequests(data: Array<User>): void {
        this.friendRequests = data;
        console.log(this.friendRequests);
    }

    openInviteDialog(): void {
        this.inviteDialogRef = this.dialog.open(FriendInviteComponent);
        this.inviteDialogRef.afterClosed().subscribe(
            ((result) => console.log("Dialog closed"))
        )
    }

     onAccept(email: string): void {
        this.friendsService.respondRequest(email, true)
        .subscribe(
            ((data: FriendRequest) => this.onSuccessful(data)),
            ((error: any) => console.log(error))
        )
    }

     onReject(email: string): void {
        this.friendsService.respondRequest(email, false)
        .subscribe(
            ((data: FriendRequest) => this.onSuccessful(data)),
            ((error: any) => console.log(error))
        )
    }

     onDocuments(email : string): void {
        this.router.navigate(['users', email, 'documents']);
    }

    private onSuccessful(friendRequest : FriendRequest): void {
        this.pageRefresh();
    }

    private pageRefresh(): void {
        location.reload();
    }
}