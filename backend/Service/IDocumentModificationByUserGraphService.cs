using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IDocumentModificationByUserGraphService
    {
        IEnumerable<Tuple<DateTime, int>> GetModificationsPerUserPerDay(User user, DateTime startingDate, DateTime latestDate);
    }
}
