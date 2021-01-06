import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material';

import { CommentAddComponent } from '../comment-add/comment-add.component';

import { CommentService } from '../../services/comment/comment.service';

import { Comment } from '../../entities/comment/comment';

@Component({
    selector: 'app-comment-list',
    templateUrl: './comment-list.component.html',
    styleUrls: ['./comment-list.component.scss'],
    providers: [CommentService]
})
export class CommentListComponent implements OnInit {

    documentId: string;
    comments: Array<Comment> = null;
    commenterEmail: string;
    comment: string;
    rating: number;
    addDialogRef: MatDialogRef<CommentAddComponent>

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private commentService: CommentService,
        private location: Location,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.documentId = params['id']
        });
        
        this.commenterEmail = localStorage.getItem('email');

        this.comment = '';

        this.commentService.getAllByDocument(this.documentId)
        .subscribe(
            ((data: Array<Comment>) => this.comments = data),
            ((error: any) => console.log(error))
        )
    }

     openAddDialog(): void {
        this.addDialogRef = this.dialog.open(CommentAddComponent, {
            data: { documentId: this.documentId },
        });
        this.addDialogRef.afterClosed().subscribe(
            ((result) => this.pageRefresh())
        )
    }

    private pageRefresh(): void {
        location.reload();
    }
}