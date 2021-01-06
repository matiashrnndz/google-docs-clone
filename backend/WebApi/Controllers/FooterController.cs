using Domain;
using Exception;
using Service;
using System;
using System.Web.Http;
using WebApi.Models.Footers;
using WebApi.Models.Texts;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/footers")]
    public class FooterController : BaseController
    {
        private readonly IFooterManagementService footerManagementService;
        private readonly ITextManagementService textManagementService;
        private readonly IDocumentModificationLogService documentLogger;

        public FooterController()
        {
            footerManagementService = serviceHandler.GetFooterManagementService();
            textManagementService = serviceHandler.GetTextManagementService();
            documentLogger = serviceHandler.GetDocumentModificationLogService();
        }

        [Route("{footer_id}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] Guid footer_id, [FromBody] UpdateFooter footer)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateFooters(GetTokenUserEmail(), footer_id))
            {
                try
                {
                    footerManagementService.Update(footer_id, UpdateFooter.ToEntity(footer));
                    documentLogger.LogModificationToFooter(footer_id);

                    return Ok(footer_id);
                }
                catch (MissingFooterException e)
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

        [Route("{footer_id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] Guid footer_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteFooters(GetTokenUserEmail(), footer_id))
            {
                try
                {
                    documentLogger.LogModificationToFooter(footer_id);
                    footerManagementService.Delete(footer_id);


                    return Ok(footer_id);
                }
                catch (MissingFooterException e)
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

        [Route("{footer_id}/texts")]
        [HttpGet]
        public IHttpActionResult GetFootersText([FromUri] Guid footer_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetFooters(GetTokenUserEmail(), footer_id))
            {
                try
                {
                    return Ok(BaseText.ToModel(textManagementService.GetByFooter(footer_id)));
                }
                catch (MissingFooterException e)
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

        [Route("{footer_id}/texts")]
        [HttpPost]
        public IHttpActionResult AddTextToFooter([FromUri] Guid footer_id, [FromBody] AddText text)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateFooters(GetTokenUserEmail(), footer_id))
            {
                try
                {
                    Text newText = textManagementService.AddToFooter(footer_id, AddText.ToEntity(text));
                    BaseText modelNewText = BaseText.ToModel(newText);
                    documentLogger.LogModificationToFooter(footer_id);

                    return CreatedAtRoute("AddTextToFooter", new { footerid = footer_id, textid = modelNewText.Id }, modelNewText);
                }
                catch (MissingFooterException e)
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