using LoggerServer.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerServer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var curAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Console.Title = string.Format("ConsoleServer. v{0}.   Path: {1}", curAssembly.GetName().Version, curAssembly.Location);

            LoggerServerController controller = new LoggerServerController();
            try
            {
                controller.Start();
                System.Console.WriteLine("============ Service has started ==========");
                System.Console.WriteLine("==>  Type 'exit' to stop                 ==");
                System.Console.WriteLine("===========================================");
                System.Console.WriteLine();
                System.Console.WriteLine();


                string newLn = null;
                do
                {
                    newLn = System.Console.ReadLine();
                    if (newLn != null)
                        newLn = newLn.ToLower();
                }
                while (newLn != "exit");
            }
            finally
            {
                controller.Dispose();
            }

            System.Console.WriteLine("============ Service has stopped ==========");
        }
    }
}
