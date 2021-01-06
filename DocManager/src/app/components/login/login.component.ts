import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { ShowErrorComponent } from '../show-error/show-error.component';

import { AuthService } from '../../services/auth/auth.service';

import { Session } from "../../entities/session/session";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    providers: [AuthService]
})
export class LoginComponent extends ShowErrorComponent implements OnInit {

    private formSubmitAttempt: boolean;
    form: FormGroup;

    constructor(
        private router: Router,
        private fb: FormBuilder,
        private authService: AuthService
    ) {
        super();
    }

    ngOnInit() {
        this.form = this.fb.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
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
            this.authService.login(this.form.value)
                .subscribe(
                    ((session: Session) => this.loginSuccessful(session)),
                    ((error: any) => this.handleLoginError(error))
                )
        }
        this.formSubmitAttempt = true;
    }

    loginSuccessful(session: Session): void {
        localStorage.setItem("token", session.Token);
        localStorage.setItem("email", session.User.Email);
        localStorage.setItem("name", session.User.Name);
        localStorage.setItem("lastname", session.User.LastName);

        this.router.navigate(['/home']);
    }

    handleLoginError(error: any): void {
        this.handleError(error);
        this.form.setValue({ email: '', password: '' });
    }
}
