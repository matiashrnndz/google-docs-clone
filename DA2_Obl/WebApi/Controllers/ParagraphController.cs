using Domain;
using Exception;
using Service;
using System;
using System.Web.Http;
using WebApi.Models.Paragraphs;
using WebApi.Models.Texts;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/paragraphs")]
    public class ParagraphController : BaseController
    {
        private readonly IParagraphManagementService paragraphManagementService;
        private readonly ITextManagementService textManagementService;
        private readonly IDocumentModificationLogService documentLogger;

        public ParagraphController()
        {
            paragraphManagementService = serviceHandler.GetParagraphManagementService();
            textManagementService = serviceHandler.GetTextManagementService();
            documentLogger = serviceHandler.GetDocumentModificationLogService();
        }

        [Route("{paragraph_id}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] Guid paragraph_id, [FromBody] UpdateParagraph paragraph)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateParagraphs(GetTokenUserEmail(), paragraph_id))
            {
                try
                {
                    paragraphManagementService.Update(paragraph_id, UpdateParagraph.ToEntity(paragraph));
                    documentLogger.LogModificationToParagraph(paragraph_id);

                    return Ok(paragraph_id);
                }
                catch (MissingParagraphException e)
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

        [Route("{paragraph_id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] Guid paragraph_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteParagraphs(GetTokenUserEmail(), paragraph_id))
            {
                try
                {
                    documentLogger.LogModificationToParagraph(paragraph_id);
                    paragraphManagementService.Delete(paragraph_id);


                    return Ok(paragraph_id);
                }
                catch (MissingParagraphException e)
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

        [Route("{paragraph_id}/texts")]
        [HttpGet]
        public IHttpActionResult GetParagraphsTexts([FromUri] Guid paragraph_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetParagraphs(GetTokenUserEmail(), paragraph_id))
            {
                try
                {
                    return Ok(BaseText.ToModel(textManagementService.GetAllByParagraph(paragraph_id)));
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{paragraph_id}/texts")]
        [HttpPost]
        public IHttpActionResult AddTextToParagraph([FromUri] Guid paragraph_id, [FromBody] AddText text)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateParagraphs(GetTokenUserEmail(), paragraph_id))
            {
                try
                {
                    Text newText = textManagementService.AddToParagraph(paragraph_id, AddText.ToEntity(text));
                    BaseText modelNewText = BaseText.ToModel(newText);
                    documentLogger.LogModificationToParagraph(paragraph_id);

                    return CreatedAtRoute("AddTextToParagraph", new { paragraphid = paragraph_id, textid = modelNewText.Id }, modelNewText);
                }
                catch (MissingParagraphException e)
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
