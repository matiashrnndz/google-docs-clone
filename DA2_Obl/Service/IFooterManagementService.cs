using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IFooterManagementService
    {
        Footer GetByDocument(Guid documentId);
        Footer Add(Guid documentId, Footer footer);
        void Update(Guid documentId, Footer footer);
        void Delete(Guid footerId);
    }
}
