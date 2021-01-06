using System;

namespace Service
{
    public interface IAuthenticationService
    {
        bool IsUserAdmin(string loggedUserEmail);

        bool IsAllowedToManageGraphs(string userEmail);

        bool IsAllowedToAddUsers(string loggedUserEmail);
        bool IsAllowedToUpdateUsers(string loggedUserEmail, string userToUpdateEmail);
        bool IsAllowedToDeleteUsers(string loggedUserEmail, string userToDeleteEmail);

        bool IsAllowedToGetAllDocuments(string loggedUserEmail, string userToGetDocumentEmail);
        bool IsAllowedToGetDocument(string loggedUserEmail, Guid documentToGetId);
        bool IsAllowedToVisualizeDocument(string loggedUserEmail, Guid documentToVisualizeId);
        bool IsAllowedToAddDocument(string loggedUserEmail, string userToAddDocumentEmail);

        bool IsAllowedToUpdateDocument(string loggedUserEmail, Guid documentToUpdateId);
        bool IsAllowedToDeleteDocument(string loggedUserEmail, Guid documentToDeleteId);

        bool IsAllowedToUpdateText(string loggedUserEmail, Guid textToUpdateId);
        bool IsAllowedToDeleteText(string loggedUserEmail, Guid textToDeleteId);

        bool IsAllowedToGetStyles(string loggedUserEmail);
        bool IsAllowedToAddStyles(string loggedUserEmail);
        bool IsAllowedToDeleteStyles(string loggedUserEmail);

        bool IsAllowedToGetStyleClasses(string loggedUserEmail);
        bool IsAllowedToAddStyleClasses(string loggedUserEmail);
        bool IsAllowedToUpdateStyleClasses(string loggedUserEmail);
        bool IsAllowedToDeleteStyleClasses(string loggedUserEmail);

        bool IsAllowedToGetParagraphs(string loggedUserEmail, Guid paragraphToGetId);
        bool IsAllowedToUpdateParagraphs(string loggedUserEmail, Guid paragraphToUpdateId);
        bool IsAllowedToDeleteParagraphs(string loggedUserEmail, Guid paragraphToDeleteId);

        bool IsAllowedToGetHeaders(string loggedUserEmail, Guid headerToGetId);
        bool IsAllowedToUpdateHeaders(string loggedUserEmail, Guid headerToUpdateId);
        bool IsAllowedToDeleteHeaders(string loggedUserEmail, Guid headerToDeleteId);

        bool IsAllowedToAddFormats(string loggedUserEmail);
        bool IsAllowedToGetFormats(string loggedUserEmail);
        bool IsAllowedToDeleteFormats(string loggedUserEmail);

        bool IsAllowedToGetFooters(string loggedUserEmail, Guid footerToGetId);
        bool IsAllowedToUpdateFooters(string loggedUserEmail, Guid footerToUpdateId);
        bool IsAllowedToDeleteFooters(string loggedUserEmail, Guid footerToDeleteId);

        bool IsAllowedToRespondFriendRequest(string loggedUserEmail, string userEmail);
        bool IsAllowedToListFriendRequests(string loggedUserEmail, string userEmail);
        bool IsAllowedToSendFriendRequest(string loggedUserEmail, string userEmail);
        bool IsAllowedToListFriends(string loggedUserEmail, string userEmail);

        bool IsAllowedToGetComments(string loggedUserEmail, string documentId);
        bool IsAllowedToCommentDocument(string loggedUserEmail, string documentId);

        bool IsAllowedToGetTop3Documents(string loggedUserEmail);
    }
}
