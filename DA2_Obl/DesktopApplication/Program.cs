using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Service;
using ServiceFactory;


namespace DesktopApplication
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IServiceHandler serviceHandler = ServiceFactory.ServiceFactory.GetImplementation();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainMenu.CreateInstance(serviceHandler));
        }
    }
}
