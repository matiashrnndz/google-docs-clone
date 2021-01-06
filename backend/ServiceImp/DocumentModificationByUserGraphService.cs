using System;
using Domain;
using Exception;
using Service;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImp
{
    public class DocumentModificationByUserGraphService : IDocumentModificationByUserGraphService
    {
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IUserRepository UserRepository { get; set; }
        internal IDocumentModificationLogRepository DocumentModificationLogRepository { get; set; }

        public IEnumerable<Tuple<DateTime, int>> GetModificationsPerUserPerDay(User user, DateTime startingDate, DateTime latestDate)
        {
            if (startingDate == null || latestDate == null)
            {
                throw new NullDateTimeException("There is a missing date.");
            }

            List <Document> documentsToComputeWith = DocumentRepository.GetAllByUser(user.Email).ToList();
            List<Tuple<DateTime, int>> modificationsPerDay = new List<Tuple<DateTime, int>>();
            DateTime currentDate;
            int numberOfModifications;
            for (currentDate = startingDate.Date; currentDate < latestDate.Date; currentDate = currentDate.AddDays(1))   
            {
                numberOfModifications = 0;
                foreach (Document document in documentsToComputeWith)
                {              
                    if(document.LastModification.Date >= currentDate.Date)
                    {
                        numberOfModifications += GetModificationsPerDocumentPerDay(document, currentDate);
                    }
                }
                Tuple<DateTime, int> dayAndNumberOfModifications = new Tuple<DateTime, int>(currentDate, numberOfModifications);
                modificationsPerDay.Add(dayAndNumberOfModifications);
            }
            return modificationsPerDay.AsEnumerable();
        }

        private int GetModificationsPerDocumentPerDay(Document document, DateTime currentDate)
        {
            int modificationCount = 0;
            List<DocumentModificationLog> modifications = DocumentModificationLogRepository.GetAllByDocument(document.Id).ToList();
            foreach(DocumentModificationLog modification in modifications)
            {
                if(modification.DateOfModification.Date.Equals(currentDate.Date))
                {
                    modificationCount++;
                }
            }
            return modificationCount; 
        }
        
    }
}
