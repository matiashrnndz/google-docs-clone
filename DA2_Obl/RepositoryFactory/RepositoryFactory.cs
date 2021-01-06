using Repository;
using System;
using System.Configuration;
using System.Reflection;

namespace RepositoryFactory
{
    public static class RepositoryFactory
    {
        public static IRepositoryHandler GetSQLServerImplementation()
        {
            var appSettings = ConfigurationManager.AppSettings;
            Assembly assembly = Assembly.LoadFile(appSettings["RepositorySQLServerDLLLocation"]);

            foreach (var item in assembly.GetTypes())
            {
                if (typeof(IRepositoryHandler).IsAssignableFrom(item))
                {
                    return (IRepositoryHandler)Activator.CreateInstance(item);
                }
            }

            throw new NullReferenceException();
        }
    }
}
