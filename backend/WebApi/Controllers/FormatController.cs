using Domain;
using Exception;
using Service;
using System.Web.Http;
using WebApi.Models.Formats;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/formats")]
    public class FormatController : BaseController
    {
        private readonly IFormatManagementService formatManagementService;
        private readonly IStyleManagementService styleManagementService;

        public FormatController()
        {
            formatManagementService = serviceHandler.GetFormatManagementService();
            styleManagementService = serviceHandler.GetStyleManagementService();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetFormats(GetTokenUserEmail()))
            {
                try
                {
                    return Ok(GetFormat.ToModel(formatManagementService.GetAll()));
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{format_name}")]
        [HttpGet]
        public IHttpActionResult GetByName([FromUri] string format_name)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetFormats(GetTokenUserEmail()))
            {
                try
                {
                    return Ok(GetFormat.ToModel(formatManagementService.GetByName(format_name)));
                }
                catch (MissingFormatException e)
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

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddFormat format)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToAddFormats(GetTokenUserEmail()))
            {
                try
                {
                    Format newStyleClass = formatManagementService.Add(AddFormat.ToEntity(format));
                    BaseFormat modelNewFormat = BaseFormat.ToModel(newStyleClass);

                    return CreatedAtRoute("AddFormat", new { formatname = modelNewFormat.Name }, modelNewFormat);
                }
                catch (MissingFormatException e)
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

        [Route("{format_name}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string format_name)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteFormats(GetTokenUserEmail()))
            {
                try
                {
                    formatManagementService.Delete(format_name);

                    return Ok(format_name);
                }
                catch (MissingFormatException e)
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
