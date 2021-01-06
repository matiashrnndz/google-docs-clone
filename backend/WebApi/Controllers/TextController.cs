using Exception;
using Service;
using System;
using System.Web.Http;
using WebApi.Models.Texts;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/texts")]
    public class TextController : BaseController
    {
        private readonly ITextManagementService textManagementService;
        private readonly IDocumentModificationLogService documentLogger;

        public TextController()
        {
            textManagementService = serviceHandler.GetTextManagementService();
            documentLogger = serviceHandler.GetDocumentModificationLogService();
        }

        [Route("{text_id}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] Guid text_id, [FromBody] UpdateText text)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateText(GetTokenUserEmail(), text_id))
            {
                try
                {
                    textManagementService.Update(text_id, UpdateText.ToEntity(text));
                    documentLogger.LogModificationToText(text_id);

                    return Ok(text_id);
                }
                catch (MissingTextException e)
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

        [Route("{text_id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] Guid text_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteText(GetTokenUserEmail(), text_id))
            {
                try
                {
                    documentLogger.LogModificationToText(text_id);
                    textManagementService.Delete(text_id);

                    return Ok(text_id);
                }
                catch (MissingTextException e)
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