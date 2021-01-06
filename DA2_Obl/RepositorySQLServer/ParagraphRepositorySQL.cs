using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class ParagraphRepositorySQL : IParagraphRepository
    {
        private static ParagraphRepositorySQL instance;

        private ParagraphRepositorySQL()
        {

        }

        public static ParagraphRepositorySQL GetInstance()
        {
            if (instance == null)
            {
                instance = new ParagraphRepositorySQL();
            }

            return instance;

        }

        public IEnumerable<Paragraph> GetAllByDocument(Guid documentId)
        {
            IEnumerable<Paragraph> paragraphs;

            using (DatabaseContext c = new DatabaseContext())
            {
                paragraphs = c.Paragraphs
                    .Where(p => p.DocumentThatBelongs.Id == documentId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .ToList();
            }

            return paragraphs;
        }

        public Paragraph GetById(Guid paragraphId)
        {
            Paragraph toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Paragraphs
                    .Where(p => p.Id == paragraphId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(Guid paragraphId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Paragraphs
                    .Any(p => p.Id == paragraphId);
            }

            return exists;
        }

        public void Add(Paragraph toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.DocumentThatBelongs != null)
                {
                    c.Entry(toAdd.DocumentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.StyleClass != null)
                {
                    toAdd.StyleClass.BasedOn = null;

                    c.Entry(toAdd.StyleClass)
                    .State = EntityState.Unchanged;
                }

                if (toAdd.Content != null)
                {
                    c.Entry(toAdd.Content)
                        .State = EntityState.Unchanged;
                }

                c.Paragraphs.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Update(Paragraph paragraph)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Paragraph toUpdate = c.Paragraphs
                    .Where(p => p.Id == paragraph.Id)
                    .Include(a => a.Content)
                    .Include(b => b.DocumentThatBelongs)
                    .FirstOrDefault();

                toUpdate.StyleClass = paragraph.StyleClass;
                toUpdate.Position = paragraph.Position;

                if (toUpdate.StyleClass != null)
                {
                    toUpdate.StyleClass.BasedOn = null;

                    c.Entry(toUpdate.StyleClass)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Delete(Guid paragraphId)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Paragraph toDelete = c.Paragraphs
                    .Where(p => p.Id == paragraphId)
                    .FirstOrDefault();

                if (toDelete.DocumentThatBelongs != null)
                {
                    c.Entry(toDelete.DocumentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                if (toDelete.StyleClass != null)
                {
                    toDelete.StyleClass.BasedOn = null;

                    c.Entry(toDelete.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (toDelete.Content != null)
                {
                    c.Entry(toDelete.Content)
                        .State = EntityState.Unchanged;
                }

                c.Paragraphs.Remove(toDelete);

                c.SaveChanges();
            }
        }

        public bool ExistsWithContent(Content content)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Paragraphs.Any(p => p.Content.Id == content.Id);
            }

            return exists;
        }

        public Paragraph GetByContent(Content content)
        {
            Paragraph toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Paragraphs.Where(p => p.Content.Id == content.Id)
                    .Include(a => a.Content)
                    .Include(b => b.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }
    }
}
