using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceImp
{
    public class AuthenticationService : IAuthenticationService
    {
        internal IUserRepository UserRepository;
        internal IDocumentRepository DocumentRepository;
        internal IHeaderRepository HeaderRepository;
        internal IParagraphRepository ParagraphRepository;
        internal IFooterRepository FooterRepository;
        internal IContentRepository ContentRepository;
        internal ITextRepository TextRepository;
        internal IFriendRequestRepository FriendRequestRespository;

        public bool IsAllowedToManageGraphs(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToAddUsers(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToUpdateUsers(string loggedUserEmail, string userToUpdateEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToDeleteUsers(string loggedUserEmail, string userToDeleteEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToGetAllDocuments(string loggedUserEmail, string userToGetDocumentEmail)
        {
            return IsValidUser(loggedUserEmail) && (IsSameUser(loggedUserEmail, userToGetDocumentEmail) || IsFriend(loggedUserEmail, userToGetDocumentEmail));
        }

        public bool IsAllowedToGetDocument(string loggedUserEmail, Guid documentToGetId)
        {
            IsValidUser(loggedUserEmail);

            IsValidDocument(documentToGetId);

            Document toGet = DocumentRepository.GetById(documentToGetId);

            return IsSameUser(loggedUserEmail, toGet.Creator.Email) || IsFriend(loggedUserEmail, toGet.Creator.Email);
        }

        public bool IsAllowedToVisualizeDocument(string loggedUserEmail, Guid documentToVisualizeId)
        {
            IsValidDocument(documentToVisualizeId);

            Document toVisualize = DocumentRepository.GetById(documentToVisualizeId);

            return IsValidUser(loggedUserEmail) && (IsSameUser(loggedUserEmail, toVisualize.Creator.Email) || IsFriend(loggedUserEmail, toVisualize.Creator.Email));
        }

        public bool IsAllowedToAddDocument(string loggedUserEmail, string userToAddDocumentEmail)
        {
            return IsValidUser(loggedUserEmail) && IsValidUser(userToAddDocumentEmail) && IsSameUser(loggedUserEmail, userToAddDocumentEmail);
        }

        public bool IsAllowedToUpdateDocument(string loggedUserEmail, Guid documentToUpdateId)
        {
            IsValidUser(loggedUserEmail);

            IsValidDocument(documentToUpdateId);

            Document toUpdate = DocumentRepository.GetById(documentToUpdateId);

            return IsSameUser(loggedUserEmail, toUpdate.Creator.Email);
        }

        public bool IsAllowedToDeleteDocument(string loggedUserEmail, Guid documentToDeleteId)
        {
            IsValidUser(loggedUserEmail);

            IsValidDocument(documentToDeleteId);

            Document toDelete = DocumentRepository.GetById(documentToDeleteId);

            return IsSameUser(loggedUserEmail, toDelete.Creator.Email);
        }


        public bool IsAllowedToGetStyles(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToAddStyles(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToDeleteStyles(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToGetStyleClasses(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToAddStyleClasses(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToUpdateStyleClasses(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToDeleteStyleClasses(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToGetParagraphs(string loggedUserEmail, Guid paragraphToGetId)
        {
            IsValidUser(loggedUserEmail);

            IsValidParagraph(paragraphToGetId);

            Paragraph paragraph = ParagraphRepository.GetById(paragraphToGetId);
            Document document = DocumentRepository.GetById(paragraph.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToUpdateParagraphs(string loggedUserEmail, Guid paragraphToUpdateId)
        {
            IsValidUser(loggedUserEmail);

            IsValidParagraph(paragraphToUpdateId);

            Paragraph paragraph = ParagraphRepository.GetById(paragraphToUpdateId);
            Document document = DocumentRepository.GetById(paragraph.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToDeleteParagraphs(string loggedUserEmail, Guid paragraphToDeleteId)
        {
            IsValidUser(loggedUserEmail);

            IsValidParagraph(paragraphToDeleteId);

            Paragraph paragraph = ParagraphRepository.GetById(paragraphToDeleteId);
            Document document = DocumentRepository.GetById(paragraph.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToGetHeaders(string loggedUserEmail, Guid headerToGetId)
        {
            IsValidUser(loggedUserEmail);

            IsValidHeader(headerToGetId);

            Header header = HeaderRepository.GetById(headerToGetId);
            Document document = DocumentRepository.GetById(header.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToUpdateHeaders(string loggedUserEmail, Guid headerToUpdateId)
        {
            IsValidUser(loggedUserEmail);

            IsValidHeader(headerToUpdateId);

            Header header = HeaderRepository.GetById(headerToUpdateId);
            Document document = DocumentRepository.GetById(header.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToDeleteHeaders(string loggedUserEmail, Guid headerToDeleteId)
        {
            IsValidUser(loggedUserEmail);

            IsValidHeader(headerToDeleteId);

            Header header = HeaderRepository.GetById(headerToDeleteId);
            Document document = DocumentRepository.GetById(header.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToAddFormats(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToGetFormats(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail);
        }

        public bool IsAllowedToDeleteFormats(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail) && IsUserAdmin(loggedUserEmail);
        }

        public bool IsAllowedToGetFooters(string loggedUserEmail, Guid footerToGetId)
        {
            IsValidUser(loggedUserEmail);

            IsValidFooter(footerToGetId);

            Footer footer = FooterRepository.GetById(footerToGetId);
            Document document = DocumentRepository.GetById(footer.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToUpdateFooters(string loggedUserEmail, Guid footerToUpdateId)
        {
            IsValidUser(loggedUserEmail);

            IsValidFooter(footerToUpdateId);

            Footer footer = FooterRepository.GetById(footerToUpdateId);
            Document document = DocumentRepository.GetById(footer.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToDeleteFooters(string loggedUserEmail, Guid footerToDeleteId)
        {
            IsValidUser(loggedUserEmail);

            IsValidFooter(footerToDeleteId);

            Footer footer = FooterRepository.GetById(footerToDeleteId);
            Document document = DocumentRepository.GetById(footer.DocumentThatBelongs.Id);

            return IsSameUser(loggedUserEmail, document.Creator.Email);
        }
        public bool IsAllowedToUpdateText(string loggedUserEmail, Guid textToUpdateId)
        {
            bool isAllowed = false;

            IsValidUser(loggedUserEmail);

            if (TextRepository.Exists(textToUpdateId))
            {
                Text text = TextRepository.GetById(textToUpdateId);

                Content content = ContentRepository.GetByText(new Text { Id = textToUpdateId });

                bool headerWithThatContent = HeaderRepository.ExistsWithContent(content);
                bool footerWithThatContent = FooterRepository.ExistsWithContent(content);
                bool paragraphWithThatContent = ParagraphRepository.ExistsWithContent(content);

                if (headerWithThatContent == true)
                {
                    Header header = HeaderRepository.GetByContent(content);
                    Document document = DocumentRepository.GetById(header.DocumentThatBelongs.Id);
                    User user = document.Creator;

                    isAllowed = user.Email == loggedUserEmail;
                }
                else if (footerWithThatContent == true)
                {
                    Footer footer = FooterRepository.GetByContent(content);
                    Document document = DocumentRepository.GetById(footer.DocumentThatBelongs.Id);
                    User user = document.Creator;

                    isAllowed = user.Email == loggedUserEmail;
                }
                else if (paragraphWithThatContent == true)
                {
                    Paragraph paragraph = ParagraphRepository.GetByContent(content);
                    Document document = DocumentRepository.GetById(paragraph.DocumentThatBelongs.Id);
                    User user = document.Creator;

                    isAllowed = user.Email == loggedUserEmail;
                }
            }

            return isAllowed;
        }

        public bool IsAllowedToDeleteText(string loggedUserEmail, Guid textToDeleteId)
        {
            IsValidUser(loggedUserEmail);

            bool isAllowed = false;

            if (TextRepository.Exists(textToDeleteId))
            {
                Text text = TextRepository.GetById(textToDeleteId);

                Content content = ContentRepository.GetByText(new Text { Id = textToDeleteId });

                bool headerWithThatContent = HeaderRepository.ExistsWithContent(content);
                bool footerWithThatContent = FooterRepository.ExistsWithContent(content);
                bool paragraphWithThatContent = ParagraphRepository.ExistsWithContent(content);

                if (headerWithThatContent == true)
                {
                    Header header = HeaderRepository.GetByContent(content);
                    Document document = DocumentRepository.GetById(header.DocumentThatBelongs.Id);
                    User user = document.Creator;

                    isAllowed = user.Email == loggedUserEmail;
                }
                else if (footerWithThatContent == true)
                {
                    Footer footer = FooterRepository.GetByContent(content);
                    Document document = DocumentRepository.GetById(footer.DocumentThatBelongs.Id);
                    User user = document.Creator;

                    isAllowed = user.Email == loggedUserEmail;
                }
                else if (paragraphWithThatContent == true)
                {
                    Paragraph paragraph = ParagraphRepository.GetByContent(content);
                    Document document = DocumentRepository.GetById(paragraph.DocumentThatBelongs.Id);
                    User user = document.Creator;

                    isAllowed = user.Email == loggedUserEmail;
                }
            }

            return isAllowed;
        }

        public bool IsAllowedToRespondFriendRequest(string loggedUserEmail, string userEmail)
        {
            return IsValidUser(loggedUserEmail) && !IsSameUser(loggedUserEmail, userEmail);
        }

        public bool IsAllowedToListFriendRequests(string loggedUserEmail, string userEmail)
        {
            return IsValidUser(loggedUserEmail) && IsSameUser(loggedUserEmail, userEmail);
        }

        public bool IsAllowedToSendFriendRequest(string loggedUserEmail, string userEmail)
        {
            return IsValidUser(loggedUserEmail) && !IsSameUser(loggedUserEmail, userEmail);
        }

        public bool IsAllowedToListFriends(string loggedUserEmail, string userEmail)
        {
            return IsValidUser(loggedUserEmail) && IsSameUser(loggedUserEmail, userEmail);
        }

        public bool IsAllowedToCommentDocument(string loggedUserEmail, string documentId)
        {
            IsValidUser(loggedUserEmail);

            Guid id = Guid.Parse(documentId);

            IsValidDocument(id);

            Document document = DocumentRepository.GetById(id);

            return IsSameUser(loggedUserEmail, document.Creator.Email) || IsFriend(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToGetComments(string loggedUserEmail, string documentId)
        {
            IsValidUser(loggedUserEmail);

            Guid id = Guid.Parse(documentId);

            IsValidDocument(id);

            Document document = DocumentRepository.GetById(id);

            return IsSameUser(loggedUserEmail, document.Creator.Email) || IsFriend(loggedUserEmail, document.Creator.Email);
        }

        public bool IsAllowedToGetTop3Documents(string loggedUserEmail)
        {
            return IsValidUser(loggedUserEmail);
        }

        private bool IsValidUser(string userEmail)
        {
            if (!UserRepository.Exists(userEmail))
            {
                throw new MissingUserException("The user does not exist.");
            }

            return true;
        }

        public bool IsUserAdmin(string userEmail)
        {
            User user = UserRepository.GetByEmail(userEmail);

            return user.Administrator;
        }

        private bool IsFriend(string userAEmail, string userBEmail)
        {
            IEnumerable<FriendRequest> userAFriends = FriendRequestRespository.GetAllAcceptedByEmail(userAEmail);

            return userAFriends.Any(f => (f.Sender.Email == userBEmail || f.Receiver.Email == userBEmail));
        }

        private bool IsSameUser(string loggedUserEmail, string userToGetDocumentEmail)
        {
            return loggedUserEmail.Equals(userToGetDocumentEmail);
        }

        private bool IsValidDocument(Guid documentId)
        {
            if (!DocumentRepository.Exists(documentId))
            {
                throw new MissingDocumentException("The documment does not exist.");
            }

            return true;
        }

        private bool IsValidParagraph(Guid paragraphId)
        {
            if (!ParagraphRepository.Exists(paragraphId))
            {
                throw new MissingDocumentException("The paragraph does not exist.");
            }

            return true;
        }

        private bool IsValidHeader(Guid headerId)
        {
            if (!HeaderRepository.Exists(headerId))
            {
                throw new MissingHeaderException("The header does not exist.");
            }

            return true;
        }

        private bool IsValidFooter(Guid footerId)
        {
            if (!FooterRepository.Exists(footerId))
            {
                throw new MissingFooterException("The footer does not exist.");
            }

            return true;
        }
    }
}