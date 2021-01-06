using Exception;
using Service;
using System;
using System.Web.Http;
using WebApi.Models.Sessions;
using WebApi.Models.Users;
using Domain;

namespace WebApi.Controllers
{
    [RoutePrefix("api/login")]
    public class LogInController : BaseController
    {
        private readonly ILogInService logInService;
        private ILoggingService loggingService;
        private IUserManagementService userService;

        public LogInController()
        {
            logInService = serviceHandler.GetLogInService();
            loggingService = serviceHandler.GetLoggingService();
            userService = serviceHandler.GetUserManagementService();
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult LogIn([FromBody] LogInUser user)
        {
            try
            {
                if (logInService.ValidateLogIn(LogInUser.ToEntity(user)))
                {
                    Guid token = sessionHandler.GetToken(LogInUser.ToEntity(user));
                    GetSession session = GetSession.ToModel(sessionHandler.GetSessionByUser(LogInUser.ToEntity(user)));

                    User registeredUser = userService.GetByEmail(LogInUser.ToEntity(user).Email);

                    loggingService.AddLogForLogin(registeredUser.UserName);
                    return Ok(session);
                }
            }
            catch (MissingUserException e)
            {
                return BadRequest(e.Message);
            }

            return BadRequest();
        }
    }
}