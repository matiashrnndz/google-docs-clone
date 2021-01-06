namespace Repository
{
    public interface IRepositoryHandler
    {
        IUserRepository GetUsersRepository();
        IStyleClassRepository GetStyleClassesRepository();
        IFormatRepository GetFormatsRepository();
        IDocumentRepository GetDocumentRepository();
        IHeaderRepository GetHeaderRepository();
        IFooterRepository GetFooterRepository();
        ITextRepository GetTextRepository();
        IContentRepository GetContentRepository();
        IParagraphRepository GetParagraphRepository();
        IStyleRepository GetStyleRepository();
        ISessionRepository GetSessionRepository();
        IDocumentModificationLogRepository GetDocumentModificationLogRepository();
        IFriendRequestRepository GetFriendRequestRepository();
        ICommentRepository GetCommentRepository();
        ILogRepository GetLogRepository();
    }
}
