import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material';

import { ShowErrorComponent } from '../show-error/show-error.component';

import { CommentService } from '../../services/comment/comment.service';

import { Comment } from '../../entities/comment/comment';

@Component({
    selector: 'app-comment-add',
    templateUrl: './comment-add.component.html',
    styleUrls: ['./comment-add.component.css']
})
export class CommentAddComponent extends ShowErrorComponent implements OnInit {

    private formSubmitAttempt: boolean;
    form: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<CommentAddComponent>,
        private fb: FormBuilder,
        private commentService: CommentService,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        super();
    }

    ngOnInit() {
        this.form = this.fb.group({
            text: ['', Validators.required],
            rating: [5, Validators.required]
        });

        this.form.setValue({ text: '', rating: 5 });
    }

    onSubmit() {
        let comment: Comment = new Comment();
        comment.Text = this.form.value.text;
        comment.Rating = this.form.value.rating;

        this.commentService.add(this.data.documentId, comment)
            .subscribe(
                ((comment: Comment) => this.onSuccessful(comment)),
                ((error: any) => this.handleLoginError(error))
            )
        this.closeDialog();
    }

    handleLoginError(error: any): void {
        this.handleError(error);
        this.form.setValue({ text: '', rating: 5 });
    }

    onSuccessful(comment: Comment): void {
        console.log("successful");
        this.closeDialog();
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    closeDialog(): void {
        this.form.setValue({ text: '', rating: 5 });
        this.dialogRef.close();
    }
}
