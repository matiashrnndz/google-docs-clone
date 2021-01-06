import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material';

import { ShowErrorComponent } from '../show-error/show-error.component';
import { UserService } from '../../services/user/user.service';

import { User } from '../../entities/user/user';

@Component({
    selector: 'app-user-update',
    templateUrl: './user-update.component.html',
    styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent extends ShowErrorComponent implements OnInit {

    private formSubmitAttempt: boolean;
    form: FormGroup;
    user: User = new User();
    private saved: boolean = false;

    constructor(
        public dialogRef: MatDialogRef<UserUpdateComponent>,
        private fb: FormBuilder,
        private userService: UserService,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.loadUser(this.data.userEmail);
        this.form = this.fb.group({
            name: ["", Validators.required],
            lastname: ["", Validators.required],
            username: ["", Validators.required],
            password: ["", Validators.required],
            administrator: [false]
        });
    }

    public loadUser(email: string) {
        this.userService.get(email)
            .subscribe(
                ((user: User) => this.setUser(user)),
                ((error: any) => this.handleLoginError(error))
            )
    };

    private setUser(user: User) {
        this.user = user;
        this.form.setValue({
            name: this.user.Name, lastname: this.user.LastName,
            username: this.user.UserName, password: this.user.Password, administrator: this.user.Administrator
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
            this.userService.update(this.data.userEmail, this.form.value)
                .subscribe(
                    ((user: User) => this.onSuccessful(user)),
                    ((error: any) => this.handleLoginError(error))
                )
            this.closeDialog();
        }
        this.formSubmitAttempt = true;
    }

    handleLoginError(error: any): void {
        this.handleError(error);
        this.form.setValue({
            name: this.user.Name, lastname: this.user.LastName,
            username: this.user.UserName, password: this.user.Password, administrator: this.user.Administrator
        });
    }

    onSuccessful(user: User): void {
        console.log("updated : " + user);
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    closeDialog(): void {
        this.dialogRef.close();
    }
}
