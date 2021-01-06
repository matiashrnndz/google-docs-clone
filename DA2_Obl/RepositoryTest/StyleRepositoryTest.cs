using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositorySQLServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryTest
{
    [TestClass]
    public class StyleRepositoryTest
    {
        DatabaseContext context = new DatabaseContext();

        IFormatRepository formatRepository = FormatRepositorySQLServer.GetInstance();
        IStyleClassRepository StyleClassRepository = StyleClassRepositorySQLServer.GetInstance();
        IStyleRepository styleRepository = StyleRepositorySQLServer.GetInstance();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestStyleIsAddedAndGottenCorrectlyFromDatabase()
        {
            Format format = new Format
            {
                Name = Guid.NewGuid().ToString()
            };

            formatRepository.Add(format);

            StyleClass styleClass = new StyleClass
            {
                Name = Guid.NewGuid().ToString(),
                BasedOn = null
            };

            StyleClassRepository.Add(styleClass);

            Style style = new Style
            {
                Format = format,
                StyleClass = styleClass,
                Id = Guid.NewGuid(),
                Key = "Negrita",
                Value = ""
            };

            styleRepository.Add(style);

            IEnumerable<Style> fromDatabase = styleRepository.GetStyles(styleClass.Name, format.Name);
            Style styleFromDatabase = fromDatabase.ElementAt(0);

            styleRepository.Delete(style);
            StyleClassRepository.Delete(styleClass.Name);
            formatRepository.Delete(format.Name);

            Assert.AreEqual(fromDatabase.Count(), 1);
            Assert.AreEqual(styleFromDatabase.Id, style.Id);
            Assert.AreEqual(styleFromDatabase.Format.Name, format.Name);
            Assert.AreEqual(styleFromDatabase.StyleClass.Name, styleClass.Name);
            Assert.AreEqual(styleFromDatabase.Key, style.Key);
            Assert.AreEqual(styleFromDatabase.Value, style.Value);
        }

        [TestMethod]
        public void TestStyleIsDeletedByDeletingStyleClassFromDatabase()
        {
            Format format = new Format
            {
                Name = Guid.NewGuid().ToString()
            };

            formatRepository.Add(format);

            StyleClass styleClass = new StyleClass
            {
                Name = Guid.NewGuid().ToString(),
                BasedOn = null
            };

            StyleClassRepository.Add(styleClass);

            Style style = new Style
            {
                Format = format,
                StyleClass = styleClass,
                Id = Guid.NewGuid(),
                Key = "Negrita",
                Value = ""
            };

            styleRepository.Add(style);

            bool existsBeforeDeleting = styleRepository.Exists(styleClass, format);

            StyleClassRepository.Delete(styleClass.Name);

            bool existsAfterDeleting = styleRepository.Exists(styleClass, format);

            formatRepository.Delete(format.Name);

            Assert.IsTrue(existsBeforeDeleting);
            Assert.IsFalse(existsAfterDeleting);
        }

        [TestMethod]
        public void TestStyleIsDeletedByDeletingFormatFromDatabase()
        {
            Format format = new Format
            {
                Name = Guid.NewGuid().ToString()
            };

            formatRepository.Add(format);

            StyleClass styleClass = new StyleClass
            {
                Name = Guid.NewGuid().ToString(),
                BasedOn = null
            };

            StyleClassRepository.Add(styleClass);

            Style style = new Style
            {
                Format = format,
                StyleClass = styleClass,
                Id = Guid.NewGuid(),
                Key = "Negrita",
                Value = ""
            };

            styleRepository.Add(style);

            bool existsBeforeDeleting = styleRepository.Exists(styleClass, format);

            formatRepository.Delete(format.Name);

            bool existsAfterDeleting = styleRepository.Exists(styleClass, format);

            StyleClassRepository.Delete(styleClass.Name);

            Assert.IsTrue(existsBeforeDeleting);
            Assert.IsFalse(existsAfterDeleting);
        }

    }
}
