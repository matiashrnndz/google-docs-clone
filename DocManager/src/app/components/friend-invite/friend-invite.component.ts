import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ShowErrorComponent } from '../show-error/show-error.component';
import { FriendService } from '../../services/friend/friend.service';

import { FriendRequest } from '../../entities/friend-request/friend-request';

@Component({
    selector: 'app-friend-invite',
    templateUrl: './friend-invite.component.html',
    styleUrls: ['./friend-invite.component.css']
})
export class FriendInviteComponent extends ShowErrorComponent implements OnInit {
    
     formSubmitAttempt: boolean;
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<FriendInviteComponent>,
        private fb: FormBuilder,
        private friendService: FriendService
    ) { 
        super();
    }

    ngOnInit() {
        this.form = this.fb.group({
            email: ['', Validators.required]
        });
    }

    isFieldInvalid(field: string) {
        return (
            (!this.form.get(field).valid && this.form.get(field).touched) ||
            (this.form.get(field).untouched && this.formSubmitAttempt)
        );
    }

    onSubmit() {
        if (this.form.valid) {
            let userEmail : string = this.form.value.email;
            this.friendService.sendRequest(userEmail)
                .subscribe(
                    ((friendRequest: FriendRequest) => this.onSuccessful(friendRequest)),
                    ((error: any) => this.handleLoginError(error))
                )
        }
        this.formSubmitAttempt = true;
    }

    handleLoginError(error: any): void {
        this.handleError(error);
        this.form.setValue({ email: '' });
    }

    onSuccessful(friendRequest: FriendRequest): void {
        console.log("The request was sent successfully");
        this.closeDialog();
    }
    
    onNoClick(): void {
        this.dialogRef.close();
    }

    closeDialog(): void {
        this.form.setValue({ email: '' });
        this.dialogRef.close();
    }
}
