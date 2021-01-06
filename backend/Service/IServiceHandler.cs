namespace Service
{
    public interface IServiceHandler
    {
        IAuthenticationService GetAuthenticationService();
        IUserManagementService GetUserManagementService();
        IStyleClassManagementService GetStyleClassManagementService();
        IFormatManagementService GetFormatManagementService();
        IDocumentManagementService GetDocumentManagementService();
        ILogInService GetLogInService();
        IParagraphManagementService GetParagraphManagementService();
        ISessionService GetSessionService();
        IStyleManagementService GetStyleManagementService();
        IFriendRequestManagementService GetFriendRequestManagementService();
        IHeaderManagementService GetHeaderManagementService();
        IFooterManagementService GetFooterManagementService();
        ITextManagementService GetTextManagementService();
        ICodeGenerator GetCodeGeneratorService();
        IDocumentModificationLogService GetDocumentModificationLogService();
        IDocumentCreationByUserGraphService GetDocumentCreationByUserGraphService();
        IDocumentModificationByUserGraphService GetDocumentModificationByUserGraphService();
        ICommentService GetCommentService();
        ITopsService GetTopsService();
        ILoggingService GetLoggingService();
    }
}