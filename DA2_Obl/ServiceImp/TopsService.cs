using Domain;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServiceImp
{
    public class TopsService : ITopsService
    {
        internal ICommentRepository CommentRepository { get; set; }
        internal IDocumentRepository DocumentRepository { get; set; }

        public IEnumerable<Document> GetTop3DocumentsByRating()
        {
            List<Tuple<Document, float>> documentAndPromedys = this.LoadTable();
            List<Tuple<Document, float>> orderedByRating = documentAndPromedys.OrderByDescending(p => p.Item2).ToList();

            List<Document> top3documents = new List<Document>();

            int added = 0;
            foreach (Tuple<Document, float> documentAndPromedy in orderedByRating)
            {
                if (added < 3)
                {
                    top3documents.Add(DocumentRepository.GetById(documentAndPromedy.Item1.Id));
                    added++;
                }
            }

            return top3documents.AsEnumerable();
        }

        private List<Tuple<Document, float>> LoadTable()
        {
            IEnumerable<Comment> comments = CommentRepository.GetAll();
            IEnumerable<Document> documents = DocumentRepository.GetAll();

            List<Tuple<Document, float>> documentAndPromedys = new List<Tuple<Document, float>>();

            foreach (Document document in documents)
            {
                Tuple<Document, float> documentAndPromedy;

                if (!(comments.Any(d => d.Document.Id == document.Id)))
                {
                    documentAndPromedy = new Tuple<Document, float>(document, 0);
                }
                else
                {
                    float cumulativeRating = 0;
                    float numberOfRatings = 0;

                    foreach (Comment comment in comments)
                    {
                        if (comment.Document.Id == document.Id)
                        {
                            numberOfRatings++;
                            cumulativeRating = cumulativeRating + comment.Rating;
                        }
                    }

                    float promedy = cumulativeRating / numberOfRatings;
                    documentAndPromedy = new Tuple<Document, float>(document, promedy);
                }
                documentAndPromedys.Add(documentAndPromedy);
            }

            return documentAndPromedys;
        }
    }
}
