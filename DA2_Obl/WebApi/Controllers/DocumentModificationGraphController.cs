using Exception;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/documentmodificationgraphic")]
    public class DocumentModificationGraphController : BaseController
    {
        private readonly IDocumentModificationByUserGraphService documentModificationByUserGraph;

        public DocumentModificationGraphController()
        {
            documentModificationByUserGraph = serviceHandler.GetDocumentModificationByUserGraphService();
        }

        [Route("{user_email}")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] string user_email)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToManageGraphs(GetTokenUserEmail()))
            {
                try
                {
                    DateTime startingDate = DateTime.Parse(Request.Headers.GetValues("Starting-Date").FirstOrDefault());
                    DateTime lastestDate = DateTime.Parse(Request.Headers.GetValues("Lastest-Date").FirstOrDefault());

                    IEnumerable<Tuple<DateTime, int>> graph = documentModificationByUserGraph.GetModificationsPerUserPerDay(
                        userManagementService.GetByEmail(user_email), startingDate, lastestDate);

                    return Ok(graph);
                }
                catch (NullDateTimeException e)
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
