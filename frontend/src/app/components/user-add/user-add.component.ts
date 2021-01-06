import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ShowErrorComponent } from '../show-error/show-error.component';
import { UserService } from '../../services/user/user.service';

import { User } from '../../entities/user/user';

@Component({
    selector: 'app-user-add',
    templateUrl: './user-add.component.html',
    styleUrls: ['./user-add.component.css']
})
export class UserAddComponent extends ShowErrorComponent implements OnInit {
    
    private formSubmitAttempt: boolean;
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<UserAddComponent>,
        private fb: FormBuilder,
        private userService: UserService
    ) { 
        super();
    }

    ngOnInit() {
        this.form = this.fb.group({
            name: ['', Validators.required],
            lastname: ['', Validators.required],
            email: ['', Validators.required],
            username: ['', Validators.required],
            password: ['', Validators.required],
            administrator: [false]
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
            this.userService.add(this.form.value)
                .subscribe(
                    ((user: User) => this.onSuccessful(user)),
                    ((error: any) => this.handleLoginError(error))
                )
        }
        this.formSubmitAttempt = true;
    }

    handleLoginError(error: any): void {
        this.handleError(error);
        this.form.setValue({ name: '', lastname: '', email: '', username: '', password: '', administrator: false });
    }

    onSuccessful(user: User): void {
        console.log("successful");
        this.closeDialog();
    }
    
    onNoClick(): void {
        this.dialogRef.close();
    }

    closeDialog(): void {
        this.form.setValue({ name: '', lastname: '', email: '', username: '', password: '', administrator: false });
        this.dialogRef.close();
    }
}
