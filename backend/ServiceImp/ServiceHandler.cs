using System;
using Repository;
using Service;

namespace ServiceImp
{
    public class ServiceHandler : IServiceHandler
    {
        private readonly IRepositoryHandler repositoryHandler;

        public ServiceHandler()
        {
            repositoryHandler = RepositoryFactory.RepositoryFactory.GetSQLServerImplementation();
        }

        public IUserManagementService GetUserManagementService()
        {
            UserManagementService userService = new UserManagementService
            {
                UserRepository = repositoryHandler.GetUsersRepository()
            };

            return userService;
        }

        public IAuthenticationService GetAuthenticationService()
        {
            AuthenticationService authenticationService = new AuthenticationService()
            {
                ContentRepository = repositoryHandler.GetContentRepository(),
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                FooterRepository = repositoryHandler.GetFooterRepository(),
                HeaderRepository = repositoryHandler.GetHeaderRepository(),
                ParagraphRepository = repositoryHandler.GetParagraphRepository(),
                TextRepository = repositoryHandler.GetTextRepository(),
                UserRepository = repositoryHandler.GetUsersRepository(),
                FriendRequestRespository = repositoryHandler.GetFriendRequestRepository()
            };

            return authenticationService;
        }

        public IStyleClassManagementService GetStyleClassManagementService()
        {
            StyleClassManagementService styleClassService = new StyleClassManagementService()
            {
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository()
            };

            return styleClassService;
        }

        public IFormatManagementService GetFormatManagementService()
        {
            FormatManagementService formatService = new FormatManagementService()
            {
                FormatRepository = repositoryHandler.GetFormatsRepository()
            };

            return formatService;
        }

        public IDocumentManagementService GetDocumentManagementService()
        {
            DocumentManagementService documentService = new DocumentManagementService()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                FormatRepository = repositoryHandler.GetFormatsRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository(),
                UserRepository = repositoryHandler.GetUsersRepository(),
                CodeGenerator = GetCodeGeneratorService()
            };

            return documentService;
        }

        public IHeaderManagementService GetHeaderManagementService()
        {
            HeaderManagementService headerService = new HeaderManagementService()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                HeaderRepository = repositoryHandler.GetHeaderRepository(),
                ContentRepository = repositoryHandler.GetContentRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository()
            };

            return headerService;
        }

        public IFooterManagementService GetFooterManagementService()
        {
            FooterManagementService footerService = new FooterManagementService()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                FooterRepository = repositoryHandler.GetFooterRepository(),
                ContentRepository = repositoryHandler.GetContentRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository()
            };

            return footerService;
        }

        public ITextManagementService GetTextManagementService()
        {
            TextManagementService textService = new TextManagementService()
            {
                ContentRepository = repositoryHandler.GetContentRepository(),
                FooterRepository = repositoryHandler.GetFooterRepository(),
                HeaderRepository = repositoryHandler.GetHeaderRepository(),
                ParagraphRepository = repositoryHandler.GetParagraphRepository(),
                TextRepository = repositoryHandler.GetTextRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository()
            };

            return textService;
        }

        public IParagraphManagementService GetParagraphManagementService()
        {
            ParagraphManagementService paragraphService = new ParagraphManagementService()
            {
                ParagraphRepository = repositoryHandler.GetParagraphRepository(),
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                ContentRepository = repositoryHandler.GetContentRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository()
            };

            return paragraphService;
        }

        public IStyleManagementService GetStyleManagementService()
        {
            StyleManagementService styleService = new StyleManagementService()
            {
                StyleRepository = repositoryHandler.GetStyleRepository(),
                FormatRepository = repositoryHandler.GetFormatsRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository(),
                StyleBuilder = new GenericStyleBuilder()
            };

            return styleService;
        }

        public ILogInService GetLogInService()
        {
            LogInService logInService = new LogInService()
            {
                UserRepository = repositoryHandler.GetUsersRepository()
            };

            return logInService;
        }

        public ISessionService GetSessionService()
        {
            SessionService sessionHandler = new SessionService()
            {
                SessionRepository = repositoryHandler.GetSessionRepository()
            };

            return sessionHandler;
        }

        public ICodeGenerator GetCodeGeneratorService()
        {
            HTMLGenerator htmlGenerator = new HTMLGenerator()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                HeaderRepository = repositoryHandler.GetHeaderRepository(),
                ParagraphRepository = repositoryHandler.GetParagraphRepository(),
                FooterRepository = repositoryHandler.GetFooterRepository(),
                ContentRepository = repositoryHandler.GetContentRepository(),
                TextRepository = repositoryHandler.GetTextRepository(),
                FormatRepository = repositoryHandler.GetFormatsRepository(),
                StyleClassRepository = repositoryHandler.GetStyleClassesRepository(),
                StyleRepository = repositoryHandler.GetStyleRepository(),
                StyleHTMLBuilder = new StyleHTMLBuilder()
            };

            return htmlGenerator;
        }

        public IDocumentModificationLogService GetDocumentModificationLogService()
        {
            DocumentModificationLogService documentModification = new DocumentModificationLogService()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                DocumentModificationLogRepository = repositoryHandler.GetDocumentModificationLogRepository(),
                ContentRepository = repositoryHandler.GetContentRepository(),
                HeaderRepository = repositoryHandler.GetHeaderRepository(),
                FooterRepository = repositoryHandler.GetFooterRepository(),
                ParagraphRepository = repositoryHandler.GetParagraphRepository(),
                TextRepository = repositoryHandler.GetTextRepository()
            };

            return documentModification;
        }

        public IDocumentCreationByUserGraphService GetDocumentCreationByUserGraphService()
        {
            DocumentCreationByUserGraphService documentCreation = new DocumentCreationByUserGraphService()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                UserRepository = repositoryHandler.GetUsersRepository()
            };

            return documentCreation;
        }

        public IDocumentModificationByUserGraphService GetDocumentModificationByUserGraphService()
        {
            DocumentModificationByUserGraphService documentModification = new DocumentModificationByUserGraphService()
            {
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                UserRepository = repositoryHandler.GetUsersRepository(),
                DocumentModificationLogRepository = repositoryHandler.GetDocumentModificationLogRepository()
            };

            return documentModification;
        }

        public IFriendRequestManagementService GetFriendRequestManagementService()
        {
            FriendRequestManagementService friendRequestManagementService = new FriendRequestManagementService()
            {
                UserRepository = repositoryHandler.GetUsersRepository(),
                FriendRequestRepository = repositoryHandler.GetFriendRequestRepository()
            };

            return friendRequestManagementService;
        }

        public ICommentService GetCommentService()
        {
            CommentService commentService = new CommentService()
            {
                CommentRepository = repositoryHandler.GetCommentRepository(),
                DocumentRepository = repositoryHandler.GetDocumentRepository(),
                UserRepository = repositoryHandler.GetUsersRepository()
            };

            return commentService;
        }

        public ITopsService GetTopsService()
        {
            TopsService topsService = new TopsService()
            {
                CommentRepository = repositoryHandler.GetCommentRepository(),
                DocumentRepository = repositoryHandler.GetDocumentRepository()
            };

            return topsService;
        }

        public ILoggingService GetLoggingService()
        {
            LoggingService loggingService = new LoggingService()
            {
                LogRepository = repositoryHandler.GetLogRepository()
            };

            return loggingService;
        }
    }
}