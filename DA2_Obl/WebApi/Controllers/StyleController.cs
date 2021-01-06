using Domain;
using Exception;
using Service;
using System;
using System.Linq;
using System.Web.Http;
using WebApi.Models.Styles;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/styles")]
    public class StyleController : BaseController
    {
        private readonly IStyleManagementService styleManagementService;

        public StyleController()
        {
            styleManagementService = serviceHandler.GetStyleManagementService();
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddAStyle([FromBody] AddStyle style)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToAddStyles(GetTokenUserEmail()))
            {
                try
                {
                    string formatName = Request.Headers.GetValues("Format-Name").FirstOrDefault();
                    string styleclassName = Request.Headers.GetValues("StyleClass-Name").FirstOrDefault();

                    Style newStyle = styleManagementService.Add(formatName, styleclassName, AddStyle.ToEntity(style));
                    BaseStyle modelNewStyle = BaseStyle.ToModel(newStyle);

                    return CreatedAtRoute("AddStyle", new { styleid = modelNewStyle.Id }, modelNewStyle);
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetStyles()
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetStyles(GetTokenUserEmail()))
            {
                try
                {
                    string formatName = Request.Headers.GetValues("Format-Name").FirstOrDefault();
                    string styleclassName = Request.Headers.GetValues("StyleClass-Name").FirstOrDefault();

                    return Ok(GetStyle.ToModel(styleManagementService.GetAll(formatName, styleclassName)));
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{style_id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] Guid style_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteStyles(GetTokenUserEmail()))
            {
                try
                {
                    styleManagementService.Delete(style_id);

                    return Ok(style_id);
                }
                catch (MissingStyleException e)
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
