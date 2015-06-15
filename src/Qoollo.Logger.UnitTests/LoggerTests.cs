using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qoollo.Logger.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.UnitTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void LoggerWritesToConsole()
        {
            TextWriter initialWriter = Console.Out;
            try
            {
                StringWriter writer = new StringWriter();
                Console.SetOut(writer);

                LoggerConfiguration config = new LoggerConfiguration(LogLevel.FullLog)
                {
                    Writer = new ConsoleWriterConfiguration("{Message}")
                };

                using (var logger = LoggerFactory.CreateLogger("testModule", config))
                {
                    logger.Info("Test message");
                }

                var result = writer.ToString();
                Assert.AreEqual("Test message" + Environment.NewLine, result);
            }
            finally
            {
                Console.SetOut(initialWriter);
            }
        }

    }
}
