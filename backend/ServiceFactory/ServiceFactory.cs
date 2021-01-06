using Service;
using System;
using System.Configuration;
using System.Reflection;

namespace ServiceFactory
{
    public static class ServiceFactory
    {
        public static IServiceHandler GetImplementation()
        {
            var appSettings = ConfigurationManager.AppSettings;
            Assembly assembly = Assembly.LoadFile(appSettings["ServiceImpDLLLocation"]);

            foreach (var item in assembly.GetTypes())
            {
                if (typeof(IServiceHandler).IsAssignableFrom(item))
                {
                    return (IServiceHandler)Activator.CreateInstance(item);
                }
            }

            throw new NullReferenceException();
        }
    }
}
