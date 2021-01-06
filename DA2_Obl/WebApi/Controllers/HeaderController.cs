using Domain;
using Exception;
using Service;
using System;
using System.Web.Http;
using WebApi.Models.Headers;
using WebApi.Models.Texts;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/headers")]
    public class HeaderController : BaseController
    {
        private readonly IHeaderManagementService headerManagementService;
        private readonly ITextManagementService textManagementService;
        private readonly IDocumentModificationLogService documentLogger;

        public HeaderController()
        {
            headerManagementService = serviceHandler.GetHeaderManagementService();
            textManagementService = serviceHandler.GetTextManagementService();
            documentLogger = serviceHandler.GetDocumentModificationLogService();
        }

        [Route("{header_id}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] Guid header_id, [FromBody] UpdateHeader header)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateHeaders(GetTokenUserEmail(), header_id))
            {
                try
                {
                    headerManagementService.Update(header_id, UpdateHeader.ToEntity(header));
                    documentLogger.LogModificationToHeader(header_id);

                    return Ok(header_id);
                }
                catch (MissingHeaderException e)
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

        [Route("{header_id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] Guid header_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteHeaders(GetTokenUserEmail(), header_id))
            {
                try
                {
                    documentLogger.LogModificationToHeader(header_id);
                    headerManagementService.Delete(header_id);


                    return Ok(header_id);
                }
                catch (MissingHeaderException e)
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

        [Route("{header_id}/texts")]
        [HttpGet]
        public IHttpActionResult GetHeadersText([FromUri] Guid header_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetHeaders(GetTokenUserEmail(), header_id))
            {
                return Ok(BaseText.ToModel(textManagementService.GetByHeader(header_id)));
            }

            return Unauthorized();
        }

        [Route("{header_id}/texts")]
        [HttpPost]
        public IHttpActionResult AddTextToHeader([FromUri] Guid header_id, [FromBody] AddText text)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateHeaders(GetTokenUserEmail(), header_id))
            {
                try
                {
                    Text newText = textManagementService.AddToHeader(header_id, AddText.ToEntity(text));
                    BaseText modelNewText = BaseText.ToModel(newText);
                    documentLogger.LogModificationToHeader(header_id);

                    return CreatedAtRoute("AddTextToHeader", new { headerid = header_id, textid = modelNewText.Id }, modelNewText);
                }
                catch (MissingHeaderException e)
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
