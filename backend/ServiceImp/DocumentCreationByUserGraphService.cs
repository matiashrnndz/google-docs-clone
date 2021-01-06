using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceImp
{
    public class DocumentCreationByUserGraphService : IDocumentCreationByUserGraphService
    {
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IUserRepository UserRepository { get; set; }

        private int GetCreationsPerUser(User user, DateTime startingDate, DateTime latestDate)
        {
            List<Document> documentsToComputeWith = DocumentRepository.GetAllByUser(user.Email).ToList();
            int creationCount = 0;

            foreach (Document document in documentsToComputeWith)
            {
                if (IsBetween(startingDate, latestDate, document))
                {
                    creationCount++;
                }
            }

            return creationCount;
        }

        private bool IsBetween
            (DateTime startingDate, DateTime latestDate, Document document)
        {
            return document.CreationDate >= startingDate && document.CreationDate <= latestDate;
        }

        public IEnumerable<Tuple<string, int>> GetDocumentByUserCreationGraph(DateTime startingDate, DateTime latestDate)
        {
            if (startingDate == null || latestDate == null)
            {
                throw new NullDateTimeException("DateTime is not valid.");
            }

            List<Tuple<string, int>> result = new List<Tuple<string, int>>();

            List<User> usersInDatabase = UserRepository.GetAll().ToList();

            foreach (User user in usersInDatabase)
            {
                int creationCount = GetCreationsPerUser(user, startingDate, latestDate);
                Tuple<string, int> userResult = new Tuple<string, int>(user.Email, creationCount);
                result.Add(userResult);
            }

            return result.AsEnumerable();
        }
    }
}
