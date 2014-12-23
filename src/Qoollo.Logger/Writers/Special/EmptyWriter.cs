using System;
using System.IO;
using Qoollo.Logger.Common;

namespace Qoollo.Logger.Writers
{
    /// <summary>
    /// EmptyWriter. Ресурс для вывода в пустоту.
    /// </summary>
    internal class EmptyWriter : Writer
    {
        private static EmptyWriter _instance;
        private static readonly object _creationLock = new object();

        public static EmptyWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_creationLock)
                    {
                        if (_instance == null)
                            _instance = new EmptyWriter();
                    }
                }

                return _instance;
            }
        }


        private EmptyWriter()
            : base(LogLevel.Trace)
        {
        }


        public override bool Write(LoggingEvent data)
        {
            return true;
        }
    }
}