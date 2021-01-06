using Service;
using System.Linq;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly IServiceHandler serviceHandler;

        protected readonly ISessionService sessionHandler;
        protected readonly IAuthenticationService authenticationService;
        protected readonly IUserManagementService userManagementService;

        public BaseController()
        {
            serviceHandler = ServiceFactory.ServiceFactory.GetImplementation();

            sessionHandler = serviceHandler.GetSessionService();
            authenticationService = serviceHandler.GetAuthenticationService();
            userManagementService = serviceHandler.GetUserManagementService();
        }

        protected string GetHeader(string headerKey)
        {
            string token = string.Empty;

            if (HeaderHasKey(headerKey))
            {
                token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            }

            return token;
        }

        protected bool HeaderHasKey(string headerKey)
        {
            return Request.Headers.Contains(headerKey);
        }

        protected string GetAuthorizationHeader()
        {
            return GetHeader("Authorization");
        }

        protected bool IsTokenValid()
        {
            string token = GetAuthorizationHeader();

            return sessionHandler.VerifyToken(token);
        }

        protected string GetTokenUserEmail()
        {
            string token = GetAuthorizationHeader();

            return sessionHandler.GetLoggedUsersMail(token);
        }
    }
}