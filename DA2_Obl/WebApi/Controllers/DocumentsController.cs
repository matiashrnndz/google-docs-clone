using Domain;
using Exception;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.Models.Comments;
using WebApi.Models.Footers;
using WebApi.Models.Headers;
using WebApi.Models.Paragraphs;
using Exceptions = System.Exception;

namespace WebApi.Controllers
{
    [RoutePrefix("api/documents")]
    public class DocumentsController : BaseController
    {
        private readonly IDocumentManagementService documentManagementService;
        private readonly IHeaderManagementService headerManagementService;
        private readonly IParagraphManagementService paragraphManagementService;
        private readonly IFooterManagementService footerManagementService;
        private readonly IFormatManagementService formatManagementService;
        private readonly ICodeGenerator codeGenerator;
        private readonly IDocumentModificationLogService documentLogger;

        public DocumentsController()
        {
            documentManagementService = serviceHandler.GetDocumentManagementService();
            headerManagementService = serviceHandler.GetHeaderManagementService();
            paragraphManagementService = serviceHandler.GetParagraphManagementService();
            footerManagementService = serviceHandler.GetFooterManagementService();
            codeGenerator = serviceHandler.GetCodeGeneratorService();
            documentLogger = serviceHandler.GetDocumentModificationLogService();
            formatManagementService = serviceHandler.GetFormatManagementService();
        }

        [Route("{document_id}/visualize")]
        [HttpGet]
        public IHttpActionResult VisualizeByFormat([FromUri] Guid document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToVisualizeDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    string formatName = Request.Headers.GetValues("Format-Name").FirstOrDefault();
                    Document document = documentManagementService.GetById(document_id);
                    Format format = formatManagementService.GetByName(formatName);

                    return Ok(codeGenerator.GenerateHTML(document, format));
                }
                catch (MissingDocumentException e)
                {
                    return BadRequest(e.Message);
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

        [Route("{document_id}")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] Guid document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    return Ok(BaseDocument.ToModel(documentManagementService.GetById(document_id)));
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
        [HttpPut]
        public IHttpActionResult Update([FromUri] Guid document_id, [FromBody] UpdateDocument document)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    documentManagementService.Update(document_id, UpdateDocument.ToEntity(document));
                    documentLogger.LogModificationToDocument(document_id);

                    return Ok(document_id);
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
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] Guid document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToDeleteDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    documentManagementService.Delete(document_id);

                    return Ok(document_id);
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

        [Route("{document_id}/headers")]
        [HttpPost]
        public IHttpActionResult AddHeaderToDocument([FromUri] Guid document_id, [FromBody] AddHeader header)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    Header newHeader = headerManagementService.Add(document_id, AddHeader.ToEntity(header));
                    BaseHeader modelNewHeader = BaseHeader.ToModel(newHeader);
                    documentLogger.LogModificationToDocument(document_id);

                    return CreatedAtRoute("AddHeader", new { documentid = document_id, headerid = modelNewHeader.Id }, modelNewHeader);
                }
                catch (MissingDocumentException e)
                {
                    return BadRequest(e.Message);
                }
                catch (ExistingHeaderException e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Unauthorized();
        }

        [Route("{document_id}/headers")]
        [HttpGet]
        public IHttpActionResult GetDocumentsHeader([FromUri] Guid document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    List<Header> headers = new List<Header>();
                    headers.Add(headerManagementService.GetByDocument(document_id));

                    return Ok(BaseHeader.ToModel(headers.AsEnumerable().ElementAt(0)));
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

        [Route("{document_id}/paragraphs")]
        [HttpPost]
        public IHttpActionResult AddParagraphToDocument([FromUri] Guid document_id, [FromBody] AddParagraph paragraph)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    Paragraph newParagraph = paragraphManagementService.Add(document_id, AddParagraph.ToEntity(paragraph));
                    BaseParagraph modelNewParagraph = BaseParagraph.ToModel(newParagraph);
                    documentLogger.LogModificationToDocument(document_id);

                    return CreatedAtRoute("AddParagraph", new { documentid = document_id, paragraphid = modelNewParagraph.Id }, modelNewParagraph);
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

        [Route("{document_id}/paragraphs")]
        [HttpGet]
        public IHttpActionResult GetDocumentsParagraphs([FromUri] Guid document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    return Ok(BaseParagraph.ToModel(paragraphManagementService.GetAllByDocument(document_id)));
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

        [Route("{document_id}/footers")]
        [HttpPost]
        public IHttpActionResult AddFooterToDocument([FromUri] Guid document_id, [FromBody] AddFooter footer)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToUpdateDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    Footer newFooter = footerManagementService.Add(document_id, AddFooter.ToEntity(footer));
                    BaseFooter modelNewFooter = BaseFooter.ToModel(newFooter);
                    documentLogger.LogModificationToDocument(document_id);

                    return CreatedAtRoute("AddFooter", new { documentid = document_id, footerid = modelNewFooter.Id }, modelNewFooter);
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

        [Route("{document_id}/footers")]
        [HttpGet]
        public IHttpActionResult GetDocumentsFooter([FromUri] Guid document_id)
        {
            if (IsTokenValid() && authenticationService.IsAllowedToGetDocument(GetTokenUserEmail(), document_id))
            {
                try
                {
                    return Ok(BaseFooter.ToModel(footerManagementService.GetByDocument(document_id)));
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