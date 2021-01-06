using Exception;
using Service;
using System.Web.Http;
using WebApi.Models.FriendRequests;
using WebApi.Models.Users;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/friends")]
    public class FriendRequestController : BaseController
    {
        private readonly IFriendRequestManagementService friendRequestManagementService;

        public FriendRequestController()
        {
            friendRequestManagementService = serviceHandler.GetFriendRequestManagementService();
        }

        [Route("{user_email}/listrequests")]
        [HttpGet]
        public IHttpActionResult ListFriendRequests([FromUri]string user_email)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToListFriendRequests(GetTokenUserEmail(), user_email))
            {
                try
                {
                    return Ok(GetUser.ToModel(friendRequestManagementService.ListReceivedFriendRequestsByEmail(user_email)));
                }
                catch (MissingUserException e)
                {
                    return BadRequest(e.Message);
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{user_email}/list")]
        [HttpGet]
        public IHttpActionResult ListFriends([FromUri]string user_email)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToListFriends(GetTokenUserEmail(), user_email))
            {
                try
                {
                    return Ok(GetUser.ToModel(friendRequestManagementService.ListFriendsByEmail(user_email)));
                }
                catch (MissingUserException e)
                {
                    return BadRequest(e.Message);
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{user_email}/sendrequest")]
        [HttpPost]
        public IHttpActionResult SendFriendRequest([FromUri]string user_email)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToSendFriendRequest(GetTokenUserEmail(), user_email))
            {
                try
                {
                    string senderEmail = GetTokenUserEmail();

                    friendRequestManagementService.AddFriendRequest(senderEmail, user_email);

                    return Ok(BaseFriendRequest.ToModel(friendRequestManagementService.GetFriendRequest(senderEmail, user_email)));
                }
                catch (MissingUserException e)
                {
                    return BadRequest(e.Message);
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{user_email}/respondrequest")]
        [HttpPost]
        public IHttpActionResult RespondFriendRequest([FromUri]string user_email, [FromBody] bool answer)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToRespondFriendRequest(GetTokenUserEmail(), user_email))
            {
                try
                {
                    string receiverEmail = GetTokenUserEmail();

                    friendRequestManagementService.RespondFriendRequest(receiverEmail, user_email, answer);

                    return Ok(BaseFriendRequest.ToModel(friendRequestManagementService.GetFriendRequest(receiverEmail, user_email)));
                }
                catch (MissingUserException e)
                {
                    return BadRequest(e.Message);
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }
    }
}
