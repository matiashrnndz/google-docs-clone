using Domain;
using Exception;
using Repository;
using Service;
using System.Collections.Generic;

namespace ServiceImp
{
    public class FormatManagementService : IFormatManagementService
    {
        internal IFormatRepository FormatRepository { get; set; }

        public Format Add(Format format)
        {
            if (!FormatRepository.Exists(format.Name))
            {
                FormatRepository.Add(format);

                return format;
            }
            else
            {
                throw new ExistingFormatException("The Format already exists on the database.");
            }
        }

        public void Delete(string formatName)
        {
            if (FormatRepository.Exists(formatName))
            {
                FormatRepository.Delete(formatName);
            }
            else
            {
                throw new MissingFormatException("The format could not be deleted as it does not exist.");
            }
        }

        public IEnumerable<Format> GetAll()
        {
            return FormatRepository.GetAll();
        }

        public Format GetByName(string formatName)
        {
            if (FormatRepository.Exists(formatName))
            {
                return FormatRepository.GetByName(formatName);
            }
            else
            {
                throw new MissingFormatException("The format was not found on the database");
            }

        }
    }
}
