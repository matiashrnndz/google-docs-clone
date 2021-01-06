import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppMaterialModule } from './app-material.module';
import { AppRoutingModule } from './app-routing.module';
import { HttpModule } from '@angular/http';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRadioModule } from '@angular/material/radio';
import { LayoutModule } from '@angular/cdk/layout';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material';
import { MatSelectModule } from '@angular/material';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material';
import { FormsModule } from '@angular/forms';


import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import * as FintTheme from 'fusioncharts/themes/fusioncharts.theme.fint';
import { FusionChartsModule } from 'angular4-fusioncharts';
FusionChartsModule.fcRoot(FusionCharts, Charts, FintTheme);

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HomeLayoutComponent } from './components/layouts/home-layout.component';
import { LoginComponent } from './components/login/login.component';
import { LoginLayoutComponent } from './components/layouts/login-layout.component';
import { MainNavComponent } from './components/main-nav/main-nav.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserAddComponent } from './components/user-add/user-add.component';
import { UserUpdateComponent } from './components/user-update/user-update.component';
import { FriendListComponent } from './components/friend-list/friend-list.component';
import { FriendInviteComponent } from './components/friend-invite/friend-invite.component';
import { FriendDocumentListComponent } from './components/friend-document-list/friend-document-list.component';
import { DocumentComponent } from './components/document/document.component';
import { DocumentListComponent } from './components/document-list/document-list.component';
import { DocumentAddComponent } from './components/document-add/document-add.component';
import { DocumentUpdateComponent } from './components/document-update/document-update.component';
import { DocumentVisualizeComponent } from './components/document-visualize/document-visualize.component';
import { HeaderComponent } from './components/header/header.component';
import { HeaderUpdateComponent } from './components/header-update/header-update.component';
import { HeaderTextComponent } from './components/header-text/header-text.component';
import { HeaderTextAddComponent } from './components/header-text-add/header-text-add.component';
import { HeaderTextUpdateComponent } from './components/header-text-update/header-text-update.component';
import { ParagraphUpdateComponent } from './components/paragraph-update/paragraph-update.component';
import { ParagraphTextListComponent } from './components/paragraph-text-list/paragraph-text-list.component';
import { ParagraphTextAddComponent } from './components/paragraph-text-add/paragraph-text-add.component';
import { ParagraphTextUpdateComponent } from './components/paragraph-text-update/paragraph-text-update.component';
import { FooterComponent } from './components/footer/footer.component';
import { FooterUpdateComponent } from './components/footer-update/footer-update.component';
import { FooterTextComponent } from './components/footer-text/footer-text.component';
import { FooterTextAddComponent } from './components/footer-text-add/footer-text-add.component';
import { FooterTextUpdateComponent } from './components/footer-text-update/footer-text-update.component';
import { CommentListComponent } from './components/comment-list/comment-list.component';
import { CommentAddComponent } from './components/comment-add/comment-add.component';
import { Top3DocumentsComponent } from './components/top3-documents/top3-documents';
import { GraphDocumentCreationComponent } from './components/graph-document-creation/graph-document-creation.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { GraphDocumentModificationComponent } from './components/graph-document-modification/graph-document-modification.component';

import { SanitizeHtmlPipe } from './components/document-visualize/sanitizehtml.pipe';

import { AuthService } from './services/auth/auth.service';
import { AuthGuard } from './services/auth/auth.guard';
import { CatchService } from './services/catch/catch.service';
import { UserService } from './services/user/user.service';
import { FriendService } from './services/friend/friend.service';
import { DocumentService } from './services/document/document.service';
import { HeaderService } from './services/header/header.service';
import { ParagraphService } from './services/paragraph/paragraph.service';
import { FooterService } from './services/footer/footer.service';
import { TextService } from './services/text/text.service';
import { CommentService } from './services/comment/comment.service';
import { TopService } from './services/top/top.service';
import { GraphDocumentCreationService } from './services/graph-document-creation/graph-document-creation.service';
import { GraphDocumentModificationService } from './services/graph-document-modification/graph-document-modification.service';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        HomeLayoutComponent,
        LoginComponent,
        LoginLayoutComponent,
        MainNavComponent,
        UserListComponent,
        UserAddComponent,
        UserUpdateComponent,
        FriendListComponent,
        FriendInviteComponent,
        FriendDocumentListComponent,
        DocumentComponent,
        DocumentListComponent,
        DocumentAddComponent,
        DocumentUpdateComponent,
        DocumentVisualizeComponent,
        SanitizeHtmlPipe,
        HeaderComponent,
        HeaderUpdateComponent,
        HeaderTextComponent,
        HeaderTextAddComponent,
        HeaderTextUpdateComponent,
        ParagraphUpdateComponent,
        ParagraphTextListComponent,
        ParagraphTextAddComponent,
        ParagraphTextUpdateComponent,
        FooterComponent,
        FooterUpdateComponent,
        FooterTextComponent,
        FooterTextAddComponent,
        FooterTextUpdateComponent,
        CommentListComponent,
        CommentAddComponent,
        Top3DocumentsComponent,
        GraphDocumentCreationComponent,
        GraphDocumentModificationComponent,
        StatisticsComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        AppMaterialModule,
        MatCheckboxModule,
        HttpModule,
        LayoutModule,
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        MatDialogModule,
        MatExpansionModule,
        MatSelectModule,
        MatInputModule,
        MatButtonToggleModule,
        MatRadioModule,
        FusionChartsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        FormsModule
    ],
    entryComponents: [
        UserAddComponent,
        UserUpdateComponent,
        FriendInviteComponent,
        DocumentAddComponent,
        DocumentUpdateComponent,
        HeaderUpdateComponent,
        HeaderTextAddComponent,
        HeaderTextUpdateComponent,
        FooterUpdateComponent,
        ParagraphUpdateComponent,
        ParagraphTextAddComponent,
        ParagraphTextUpdateComponent,
        FooterTextAddComponent,
        FooterTextUpdateComponent,
        CommentAddComponent
    ],
    providers: [
        AuthService,
        AuthGuard,
        CatchService,
        UserService,
        FriendService,
        DocumentService,
        HeaderService,
        ParagraphService,
        FooterService,
        TextService,
        CommentService,
        TopService,
        GraphDocumentCreationService,
        GraphDocumentModificationService
    ],
    bootstrap: [
        AppComponent
    ],
})
export class AppModule { }
