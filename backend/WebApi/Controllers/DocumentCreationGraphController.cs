using Exception;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/documentcreationgraphic")]
    public class DocumentCreationGraphController : BaseController
    {
        private readonly IDocumentCreationByUserGraphService documentCreationByUserGraph;

        public DocumentCreationGraphController()
        {
            documentCreationByUserGraph = serviceHandler.GetDocumentCreationByUserGraphService();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            if (IsTokenValid() && authenticationService.IsAllowedToManageGraphs(GetTokenUserEmail()))
            {
                try
                {
                    DateTime startingDate = DateTime.Parse(Request.Headers.GetValues("Starting-Date").FirstOrDefault());
                    DateTime lastestDate = DateTime.Parse(Request.Headers.GetValues("Lastest-Date").FirstOrDefault());

                    IEnumerable<Tuple<string, int>> graph = documentCreationByUserGraph.GetDocumentByUserCreationGraph(startingDate, lastestDate);

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
