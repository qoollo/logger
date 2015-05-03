using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Test
{
    class Program
    {
        static void UseLogger()
        {
            //Qoollo.Logger.LoggerDefault.LoadInstanceFromAppConfig();
            var log1 = Qoollo.Logger.LoggerFactory.CreateLoggerFromAppConfig("123", "LoggerConfigurationSection");
            var log2 = Qoollo.Logger.LoggerFactory.CreateLoggerFromAppConfig("321", "LoggerConfigurationSection");
            var logger = Qoollo.Logger.LoggerDefault.Instance.GetThisClassLogger();

            System.Threading.Thread.Sleep(1000);

            for (int i = 0; i < 500; i++)
            {
                log1.Trace("trace sample: " + i.ToString());
                log2.Trace("trace sample: " + i.ToString());
                logger.Trace("trace sample: " + i.ToString());
            }
            System.Threading.Thread.Sleep(1);
            for (int i = 0; i < 500; i++)
            {
                log1.Debug("debug sample: " + i.ToString());
                log2.Debug("debug sample: " + i.ToString());
                logger.Debug("debug sample: " + i.ToString());
            }
            System.Threading.Thread.Sleep(1);
            for (int i = 0; i < 500; i++)
            {
                log1.Info("info sample: " + i.ToString());
                log2.Info("info sample: " + i.ToString());
                logger.Info("info sample: " + i.ToString());
            }
            System.Threading.Thread.Sleep(1);
            for (int i = 0; i < 500; i++)
            {
                log1.WarnFormat("warn sample: {0}", i);
                log2.WarnFormat("warn sample: {0}", i);
                logger.WarnFormat("warn sample: {0}", i);
            }
            System.Threading.Thread.Sleep(1);
            Exception ex = new InvalidOperationException("test exc");
            for (int i = 0; i < 500; i++)
            {
                log1.Error(ex, "error sample: " + i.ToString());
                log2.Error(ex, "error sample: " + i.ToString());
                logger.Error(ex, "error sample: " + i.ToString());
            }
            System.Threading.Thread.Sleep(1);
            var realExc = GetRealExc();
            for (int i = 0; i < 500; i++)
            {
                log1.Fatal(realExc, "fatal sample: " + i.ToString());
                log2.Fatal(realExc, "fatal sample: " + i.ToString());
                logger.Fatal(realExc, "fatal sample: " + i.ToString());
            }

            log1.Dispose();
            log2.Dispose();
            Qoollo.Logger.LoggerDefault.ResetInstance();
        }

        static void Main(string[] args)
        {
            UseLogger();

            //LoggerDefault.Instance.Debug("Stuff");
            //System.Threading.Thread.Sleep(1000);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.ReadLine();
        }


        static Exception GetRealExc()
        {
            try
            {
                Inner();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        static void Inner()
        {
            throw new NullReferenceException();
        }
    }
}
