using System;
using Repository;

namespace RepositorySQLServer
{
    public class RepositoryHandlerSQLServer : IRepositoryHandler
    {
        public IStyleClassRepository GetStyleClassesRepository()
        {
            return StyleClassRepositorySQLServer.GetInstance();
        }

        public IUserRepository GetUsersRepository()
        {
            return UserRepositorySQLServer.GetInstance();
        }

        public IFormatRepository GetFormatsRepository()
        {
            return FormatRepositorySQLServer.GetInstance();
        }

        public IDocumentRepository GetDocumentRepository()
        {
            return DocumentRepositorySQLServer.GetInstance();
        }

        public IHeaderRepository GetHeaderRepository()
        {
            return HeaderRepositorySQLServer.GetInstance();
        }

        public IFooterRepository GetFooterRepository()
        {
            return FooterRepositorySQLServer.GetInstance();
        }

        public ITextRepository GetTextRepository()
        {
            return TextRepositorySQLServer.GetInstance();
        }

        public IContentRepository GetContentRepository()
        {
            return ContentRepositorySQLServer.GetInstance();
        }

        public IParagraphRepository GetParagraphRepository()
        {
            return ParagraphRepositorySQL.GetInstance();
        }

        public IStyleRepository GetStyleRepository()
        {
            return StyleRepositorySQLServer.GetInstance();
        }

        public IDocumentModificationLogRepository GetDocumentModificationLogRepository()
        {
            return DocumentModificationLogRepositorySQLServer.GetInstance();
        }

        public ISessionRepository GetSessionRepository()
        {
            return SessionRepositorySQLServer.GetInstance();
        }

        public IFriendRequestRepository GetFriendRequestRepository()
        {
            return FriendRequestRepositorySQLServer.GetInstance();
        }

        public ICommentRepository GetCommentRepository()
        {
            return CommentRepositorySQLServer.GetInstance();
        }

        public ILogRepository GetLogRepository()
        {
            return LogRepositorySQLServer.GetInstance();
        }
    }
}
