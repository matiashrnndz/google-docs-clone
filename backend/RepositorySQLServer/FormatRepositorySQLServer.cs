using Domain;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace RepositorySQLServer
{
    public class FormatRepositorySQLServer : IFormatRepository
    {
        private static FormatRepositorySQLServer instance;

        private FormatRepositorySQLServer()
        {

        }

        public static FormatRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new FormatRepositorySQLServer();
            }

            return instance;
        }

        public IEnumerable<Format> GetAll()
        {
            List<Format> formats;

            using (DatabaseContext c = new DatabaseContext())
            {
                formats = c.Formats
                    .ToList();
            }

            return formats.AsEnumerable();
        }

        public Format GetByName(string formatName)
        {
            Format toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Formats
                    .Where(f => f.Name == formatName)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(string formatName)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Formats
                    .Any(f => f.Name == formatName);
            }

            return exists;
        }

        public void Add(Format format)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                c.Formats.Add(format);

                c.SaveChanges();
            }
        }

        public void Delete(string formatName)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Format toDelete = c.Formats
                    .Where(f => f.Name == formatName)
                    .FirstOrDefault();

                c.Formats.Remove(toDelete);

                c.SaveChanges();
            }
        }
    }
}
