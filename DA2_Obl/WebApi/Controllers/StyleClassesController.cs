using Domain;
using Exception;
using Service;
using System.Web.Http;
using WebApi.Models.StyleClasses;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/styleclasses")]
    public class StyleClassesController : BaseController
    {
        private readonly IStyleClassManagementService styleClassManagementService;

        public StyleClassesController()
        {
            styleClassManagementService = serviceHandler.GetStyleClassManagementService();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetStyleClasses(GetTokenUserEmail()))
            {
                try
                {
                    return Ok(GetStyleClass.ToModel(styleClassManagementService.GetAll()));
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{styleClass_name}")]
        [HttpGet]
        public IHttpActionResult GetByName([FromUri]string styleClass_name)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetStyleClasses(GetTokenUserEmail()))
            {
                try
                {
                    return Ok(GetStyleClass.ToModel(styleClassManagementService.GetByName(styleClass_name)));
                }
                catch (MissingStyleClassException e)
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

        [Route("{styleClass_name}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] string styleClass_name, [FromBody] UpdateStyleClass styleClass)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateStyleClasses(GetTokenUserEmail()))
            {
                try
                {
                    styleClassManagementService.Update(styleClass_name, UpdateStyleClass.ToEntity(styleClass));

                    return Ok(styleClass_name);
                }
                catch (MissingStyleClassException e)
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
        public IHttpActionResult Add([FromBody] AddStyleClass styleClass)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToAddStyleClasses(GetTokenUserEmail()))
            {
                try
                {
                    StyleClass newStyleClass = styleClassManagementService.Add(AddStyleClass.ToEntity(styleClass));
                    BaseStyleClass modelNewStyleClass = BaseStyleClass.ToModel(newStyleClass);

                    return CreatedAtRoute("AddStyleClass", new { styleclassname = modelNewStyleClass.Name }, modelNewStyleClass);
                }
                catch (MissingStyleClassException e)
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

        [Route("{styleClass_name}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string styleClass_name)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteStyleClasses(GetTokenUserEmail()))
            {
                try
                {
                    styleClassManagementService.Delete(styleClass_name);

                    return Ok(styleClass_name);
                }
                catch (MissingStyleClassException e)
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
