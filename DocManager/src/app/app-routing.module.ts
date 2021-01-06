import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './services/auth/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { LoginLayoutComponent } from './components/layouts/login-layout.component';
import { HomeComponent } from './components/home/home.component';
import { HomeLayoutComponent } from './components/layouts/home-layout.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { FriendListComponent } from './components/friend-list/friend-list.component';
import { DocumentListComponent } from './components/document-list/document-list.component';
import { FriendDocumentListComponent } from './components/friend-document-list/friend-document-list.component';
import { DocumentComponent} from './components/document/document.component';
import { DocumentVisualizeComponent } from './components/document-visualize/document-visualize.component';
import { CommentListComponent } from './components/comment-list/comment-list.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { GraphDocumentCreationComponent } from './components/graph-document-creation/graph-document-creation.component';
import { GraphDocumentModificationComponent } from './components/graph-document-modification/graph-document-modification.component';

const routes: Routes = [
    { path: '', component: HomeLayoutComponent, canActivate: [AuthGuard],
        children: [
            { path: '', component: HomeComponent },
            { path: 'users', component: UserListComponent },
            { path: 'users/:email/documents', component: FriendDocumentListComponent },
            { path: 'friends', component: FriendListComponent },
            { path: 'documents', component: DocumentListComponent },
            { path: 'documents/:id', component: DocumentComponent },
            { path: 'documents/:id/visualize', component: DocumentVisualizeComponent },
            { path: 'comments/:id', component: CommentListComponent },
            { path: 'statistics', component: StatisticsComponent },
            { path: 'statistics/documentcreationgraphic', component: GraphDocumentCreationComponent},
            { path: 'statistics/documentmodificationgraphic', component: GraphDocumentModificationComponent }
        ]
    },
    { path: '', component: LoginLayoutComponent,
        children: [{ path: 'login', component: LoginComponent}
        ]
    },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {useHash:true})],
    exports: [RouterModule]
})

export class AppRoutingModule { }
