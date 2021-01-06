using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IHeaderManagementService
    {
        Header GetByDocument(Guid documentId);
        void Update(Guid headerId, Header header);
        Header Add(Guid documentId, Header header);
        void Delete(Guid headerId);
    }
}
