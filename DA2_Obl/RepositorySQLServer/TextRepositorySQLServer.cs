using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class TextRepositorySQLServer : ITextRepository
    {
        private static TextRepositorySQLServer instance;

        private TextRepositorySQLServer()
        {

        }

        public static TextRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new TextRepositorySQLServer();
            }

            return instance;
        }

        public Text GetById(Guid textId)
        {
            Text toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Texts
                    .Where(t => t.Id == textId)
                    .Include(t => t.ContentThatBelongs)
                    .Include(s => s.StyleClass)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public IEnumerable<Text> GetByContent(Content content)
        {
            IEnumerable<Text> texts;

            using (DatabaseContext c = new DatabaseContext())
            {
                texts = c.Texts
                    .Where(t => t.ContentThatBelongs.Id == content.Id)
                    .Include(t => t.ContentThatBelongs)
                    .Include(s => s.StyleClass)
                    .ToList();
            }

            return texts;
        }

        public bool Exists(Guid textId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Texts
                    .Any(t => t.Id == textId);
            }

            return exists;
        }

        public void Add(Text toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.StyleClass != null)
                {
                    toAdd.StyleClass.BasedOn = null;

                    c.Entry(toAdd.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.ContentThatBelongs != null)
                {
                    c.Entry(toAdd.ContentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                c.Texts.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Update(Text text)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Text toUpdate = c.Texts
                    .Where(t => t.Id == text.Id)
                    .FirstOrDefault();

                toUpdate.ContentThatBelongs = text.ContentThatBelongs;
                toUpdate.StyleClass = text.StyleClass;
                toUpdate.TextContent = text.TextContent;
                toUpdate.Position = text.Position;

                if (toUpdate.StyleClass != null)
                {
                    toUpdate.StyleClass.BasedOn = null;

                    c.Entry(toUpdate.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (toUpdate.ContentThatBelongs != null)
                {
                    c.Entry(toUpdate.ContentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Delete(Guid textId)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Text toDelete = c.Texts
                    .Where(t => t.Id == textId)
                    .Include(t => t.ContentThatBelongs)
                    .Include(s => s.StyleClass)
                    .FirstOrDefault();

                if (toDelete.StyleClass != null)
                {
                    toDelete.StyleClass.BasedOn = null;

                    c.Entry(toDelete.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (toDelete.ContentThatBelongs != null)
                {
                    c.Entry(toDelete.ContentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                c.Texts.Remove(toDelete);

                c.SaveChanges();
            }
        }
    }
}
