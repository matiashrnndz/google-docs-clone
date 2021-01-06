using Exception;
using Service;
using System.Web.Http;
using WebApi.Models.Comments;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;

        public CommentController()
        {
            commentService = serviceHandler.GetCommentService();
        }

        [Route("{document_id}")]
        [HttpPost]
        public IHttpActionResult AddComment([FromUri]string document_id, [FromBody] AddComment comment)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToCommentDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    commentService.AddComment(GetTokenUserEmail(), document_id, comment.ToEntity());

                    CreatedAtRoute("AddComment", new { documentid = document_id }, comment);
                }
                catch (MissingUserException e)
                {
                    return BadRequest(e.Message);
                }
                catch (MissingDocumentException e)
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

        [Route("{document_id}")]
        [HttpGet]
        public IHttpActionResult GetComments([FromUri] string document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetComments(GetTokenUserEmail(), document_id))
            {
                try
                {
                    return Ok(GetComment.ToModel(commentService.GetComments(document_id)));
                }
                catch (MissingDocumentException e)
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
