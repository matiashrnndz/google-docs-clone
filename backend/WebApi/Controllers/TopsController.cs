using Service;
using System.Web.Http;
using WebApi.Models.Comments;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/tops")]
    public class TopsController : BaseController
    {
        private readonly ITopsService topsService;

        public TopsController()
        {
            topsService = serviceHandler.GetTopsService();
        }

        [Route("top3documents")]
        [HttpGet]
        public IHttpActionResult GetTop3Documents()
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetTop3Documents(GetTokenUserEmail()))
            {
                try
                {
                    return Ok(BaseDocument.ToModel(topsService.GetTop3DocumentsByRating()));
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
