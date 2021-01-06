using Domain;
using System.Collections.Generic;

namespace Service
{
    public interface ITopsService
    {
        IEnumerable<Document> GetTop3DocumentsByRating();
    }
}
