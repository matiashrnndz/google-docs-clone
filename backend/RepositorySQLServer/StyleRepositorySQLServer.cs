using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class StyleRepositorySQLServer : IStyleRepository
    {
        private static StyleRepositorySQLServer instance;

        private StyleRepositorySQLServer()
        {

        }

        public static StyleRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new StyleRepositorySQLServer();
            }

            return instance;
        }

        public IEnumerable<Style> GetStyles(string styleClassName, string formatName)
        {
            IEnumerable<Style> styles;

            using (DatabaseContext c = new DatabaseContext())
            {
                styles = c.Styles
                    .Where(p => (p.Format.Name == formatName)
                    && (p.StyleClass.Name == styleClassName))
                    .Include(f => f.Format)
                    .Include(s => s.StyleClass)
                    .ToList();
            }

            return styles;
        }

        public bool Exists(StyleClass styleClass, Format format)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Styles
                    .Any(p => (p.Format.Name == format.Name)
                    && (p.StyleClass.Name == styleClass.Name));
            }

            return exists;
        }

        public void Add(Style toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.Format != null)
                {
                    c.Entry(toAdd.Format)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.StyleClass != null)
                {
                    toAdd.StyleClass.BasedOn = null;

                    c.Entry(toAdd.StyleClass)
                        .State = EntityState.Unchanged;
                }

                c.Styles.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Delete(Style style)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Style toDelete = c.Styles
                    .Where(d => d.Id == style.Id)
                    .Include(f => f.Format)
                    .Include(s => s.StyleClass)
                    .FirstOrDefault();

                if (toDelete.Format != null)
                {
                    c.Entry(toDelete.Format).State = EntityState.Unchanged;
                }

                if (toDelete.StyleClass != null)
                {
                    toDelete.StyleClass.BasedOn = null;

                    c.Entry(toDelete.StyleClass).State = EntityState.Unchanged;
                }

                c.Styles.Remove(toDelete);

                c.SaveChanges();
            }
        }

        public Style GetById(Guid styleId)
        {
            Style toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Styles
                    .Where(s => s.Id == styleId)
                    .Include(f => f.Format)
                    .Include(a => a.StyleClass)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool ExistsById(Guid styleId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Styles
                    .Any(p => p.Id == styleId);
            }

            return exists;
        }
    }
}
