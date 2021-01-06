using Domain;

namespace WebApi.Models.DocumentFilterAndOrders
{
    public class GetDocumentFilterAndOrder : Model<DocumentFilterAndOrder, GetDocumentFilterAndOrder>
    {
        public Document DocumentFilteredData { get; set; }
        public string OrderBy { get; set; }
        public bool IsDesc { get; set; }

        public GetDocumentFilterAndOrder()
        {

        }

        public GetDocumentFilterAndOrder(DocumentFilterAndOrder document)
        {
            SetModel(document);
        }

        public override DocumentFilterAndOrder ToEntity() => new DocumentFilterAndOrder()
        {
            DocumentFilteredData = this.DocumentFilteredData,
            OrderBy = this.OrderBy,
            IsDesc = this.IsDesc

        };

        protected override GetDocumentFilterAndOrder SetModel(DocumentFilterAndOrder document)
        {
            DocumentFilteredData = document.DocumentFilteredData;
            OrderBy = document.OrderBy;
            IsDesc = document.IsDesc;

            return this;
        }
    }
}