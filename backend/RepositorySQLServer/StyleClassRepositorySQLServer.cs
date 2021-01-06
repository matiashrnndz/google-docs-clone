using Domain;
using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class StyleClassRepositorySQLServer : IStyleClassRepository
    {
        private static StyleClassRepositorySQLServer instance;

        private StyleClassRepositorySQLServer()
        {

        }

        public static StyleClassRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new StyleClassRepositorySQLServer();
            }

            return instance;
        }


        public IEnumerable<StyleClass> GetAll()
        {
            List<StyleClass> styleClasses;

            using (DatabaseContext c = new DatabaseContext())
            {
                styleClasses = c.StyleClasses
                    .Include(s1 => s1.BasedOn)
                    .ToList();
            }

            return styleClasses.AsEnumerable();
        }

        public StyleClass GetByName(string styleClassName)
        {
            StyleClass toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.StyleClasses
                    .Where(s => s.Name == styleClassName)
                    .Include(b => b.BasedOn)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(string styleClassName)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.StyleClasses
                    .Any(s => s.Name == styleClassName);
            }

            return exists;
        }

        public void Add(StyleClass toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                c.StyleClasses.Add(toAdd);

                if (toAdd.BasedOn != null)
                {
                    toAdd.BasedOn.BasedOn = null;

                    c.Entry(toAdd.BasedOn)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Update(StyleClass styleClass)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                StyleClass toUpdate = c.StyleClasses
                    .Where(s => s.Name == styleClass.Name)
                    .FirstOrDefault();

                toUpdate.BasedOn = styleClass.BasedOn;

                if (toUpdate.BasedOn != null)
                {
                    toUpdate.BasedOn.BasedOn = null;

                    c.Entry(toUpdate.BasedOn)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Delete(string styleClassName)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                List<Document> documentsToNullifyStyleclass = c.Documents
                    .Where(q => q.StyleClass.Name == styleClassName)
                    .Include(s => s.StyleClass)
                    .ToList();

                foreach (Document document in documentsToNullifyStyleclass)
                {
                    if (document.StyleClass != null)
                    {
                        document.StyleClass = null;
                        c.Entry(document).State = EntityState.Modified;
                    }
                }

                List<Header> headersToNullifyStyleclass = c.Headers
                    .Where(q => q.StyleClass.Name == styleClassName)
                    .Include(s => s.StyleClass)
                    .ToList();

                foreach (Header header in headersToNullifyStyleclass)
                {
                    if (header.StyleClass != null)
                    {
                        header.StyleClass = null;
                        c.Entry(header).State = EntityState.Modified;
                    }
                }

                List<Footer> footersToNullifyStyleclass = c.Footers
                    .Where(q => q.StyleClass.Name == styleClassName)
                    .Include(s => s.StyleClass)
                    .ToList();

                foreach (Footer footer in footersToNullifyStyleclass)
                {
                    if (footer.StyleClass != null)
                    {
                        footer.StyleClass = null;
                        c.Entry(footer).State = EntityState.Modified;
                    }
                }

                List<Paragraph> paragraphsToNullifyStyleclass = c.Paragraphs
                    .Where(q => q.StyleClass.Name == styleClassName)
                    .Include(s => s.StyleClass)
                    .ToList();

                foreach (Paragraph paragraph in paragraphsToNullifyStyleclass)
                {
                    if (paragraph.StyleClass != null)
                    {
                        paragraph.StyleClass = null;
                        c.Entry(paragraph).State = EntityState.Modified;
                    }
                }

                List<Text> textsToNullifyStyleClass = c.Texts
                    .Where(q => q.StyleClass.Name == styleClassName)
                    .Include(s => s.StyleClass)
                    .ToList();

                foreach (Text text in textsToNullifyStyleClass)
                {
                    if (text.StyleClass != null)
                    {
                        text.StyleClass = null;
                        c.Entry(text).State = EntityState.Modified;
                    }
                }

                StyleClass toDelete = c.StyleClasses
                    .Where(s => s.Name == styleClassName)
                    .Include(b => b.BasedOn)
                    .FirstOrDefault();

                if (toDelete.BasedOn != null)
                {
                    toDelete.BasedOn.BasedOn = null;

                    c.Entry(toDelete.BasedOn).State = EntityState.Unchanged;
                }

                c.StyleClasses.Remove(toDelete);

                c.SaveChanges();
            }
        }
    }
}
