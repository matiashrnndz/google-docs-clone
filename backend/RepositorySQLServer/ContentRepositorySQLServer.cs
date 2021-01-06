using Domain;
using Repository;
using System;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class ContentRepositorySQLServer : IContentRepository
    {
        private static ContentRepositorySQLServer instance;

        private ContentRepositorySQLServer()
        {

        }

        public static ContentRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new ContentRepositorySQLServer();
            }

            return instance;
        }

        public Content GetById(Guid contentId)
        {
            Content toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Contents
                    .Where(f => f.Id == contentId)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public Content GetByHeader(Header header)
        {
            Content toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                Header headerFromDb = c.Headers
                    .Where(f => f.Id == header.Id)
                    .Include(t => t.Content)
                    .FirstOrDefault();

                toGet = c.Contents
                    .Where(f => f.Id == headerFromDb.Content.Id)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public Content GetByParagraph(Paragraph paragraph)
        {
            Content toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                Paragraph paragraphFromDb = c.Paragraphs
                    .Where(f => f.Id == paragraph.Id)
                    .Include(t => t.Content)
                    .FirstOrDefault();

                toGet = c.Contents
                    .Where(f => f.Id == paragraphFromDb.Content.Id)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public Content GetByFooter(Footer footer)
        {
            Content toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                Footer footerFromDb = c.Footers
                    .Where(f => f.Id == footer.Id)
                    .Include(t => t.Content)
                    .FirstOrDefault();

                toGet = c.Contents
                    .Where(f => f.Id == footerFromDb.Content.Id)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public Content GetByText(Text text)
        {
            Content toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                Text textFromDb = c.Texts
                    .Where(f => f.Id == text.Id)
                    .Include(t => t.ContentThatBelongs)
                    .FirstOrDefault();

                toGet = c.Contents
                    .Where(f => f.Id == textFromDb.ContentThatBelongs.Id)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public void Add(Content content)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                c.Contents.Add(content);

                c.SaveChanges();
            }
        }

        public void Delete(Content content)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Content toGet = c.Contents
                    .Where(f => f.Id == content.Id)
                    .FirstOrDefault();

                c.Contents.Remove(toGet);

                c.SaveChanges();
            }
        }

        public bool Exists(Guid contentId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Contents
                    .Any(d => d.Id == contentId);
            }

            return exists;
        }
    }
}
