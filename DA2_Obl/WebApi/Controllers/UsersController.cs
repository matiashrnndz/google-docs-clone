using Domain;
using Exception;
using Service;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models.Comments;
using WebApi.Models.DocumentFilterAndOrders;
using WebApi.Models.Users;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        private readonly IDocumentManagementService documentManagementService;
        private readonly IDocumentModificationLogService documentLogger;

        public UsersController()
        {
            documentManagementService = serviceHandler.GetDocumentManagementService();
            documentLogger = serviceHandler.GetDocumentModificationLogService();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            if (IsTokenValid())
            {
                try
                {
                    return Ok(GetUser.ToModel(userManagementService.GetAll()));
                }
                catch (MissingUserException e)
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

        [Route("{user_email}/")]
        [HttpPost]
        public IHttpActionResult GetDocumentsFilteredAndOrdered([FromUri]string user_email, [FromBody] GetDocumentFilterAndOrder documentFiltersAndOrdersModel)
        {
            if (IsTokenValid())
            {
                try
                {
                    DocumentFilterAndOrder documentFiltersAndOrders = GetDocumentFilterAndOrder.ToEntity(documentFiltersAndOrdersModel);
                    return Ok(BaseDocument.ToModel(documentManagementService.GetAllByUserFilteredAndOrdered(user_email, documentFiltersAndOrders)));
                }
                catch (MissingUserException e)
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

        [Route("{user_email}/")]
        [HttpGet]
        public IHttpActionResult Get([FromUri]string user_email)
        {
            if (IsTokenValid())
            {
                try
                {
                    return Ok(BaseUser.ToModel(userManagementService.GetByEmail(user_email)));
                }
                catch (MissingUserException e)
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
        public IHttpActionResult Add([FromBody] AddUser user)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToAddUsers(GetTokenUserEmail()))
            {
                try
                {
                    User newUser = userManagementService.Add(AddUser.ToEntity(user));
                    BaseUser modelNewUser = BaseUser.ToModel(newUser);

                    return CreatedAtRoute("AddUser", new { useremail = modelNewUser.Email }, modelNewUser);
                }
                catch (ExistingUserException e)
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


        [Route("{user_email}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] string user_email, [FromBody]UpdateUser user)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateUsers(GetTokenUserEmail(), user_email))
            {
                try
                {
                    userManagementService.Update(user_email, user.ToEntity());

                    return Ok(BaseUser.ToModel(userManagementService.GetByEmail(user_email)));
                }
                catch (MissingUserException e)
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

        [Route("{user_email}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string user_email)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteUsers(GetTokenUserEmail(), user_email))
            {
                try
                {
                    userManagementService.Delete(user_email);

                    return Ok(user_email);
                }
                catch (MissingUserException e)
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

        [Route("{user_email}/documents")]
        [HttpGet]
        public IHttpActionResult GetAllDocuments([FromUri] string user_email)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetAllDocuments(GetTokenUserEmail(), user_email))
            {
                try
                {
                    IEnumerable<Document> documents = documentManagementService.GetAllByUser(user_email);
                    return Ok(BaseDocument.ToModel(documents));
                }
                catch (MissingUserException e)
                {
                    BadRequest(e.Message);
                }
                catch (Exceptions e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{user_email}/documents")]
        [HttpPost]
        public IHttpActionResult AddADocument([FromUri] string user_email, [FromBody] AddDocument document)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToAddDocument(GetTokenUserEmail(), user_email))
            {
                try
                {
                    Document newDocument = documentManagementService.Add(user_email, AddDocument.ToEntity(document));
                    BaseDocument modelNewDocument = BaseDocument.ToModel(newDocument);
                    documentLogger.LogModificationToDocument(modelNewDocument.Id);

                    return CreatedAtRoute("AddDocument", new { useremail = user_email, documentid = modelNewDocument.Id }, modelNewDocument);
                }
                catch (MissingUserException e)
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
