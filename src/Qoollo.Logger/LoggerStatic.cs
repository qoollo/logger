using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger
{
    /// <summary>
    /// Static logger. Shortcut for 'LoggerDefault.Instance'
    /// </summary>
    public partial class LoggerStatic
    {
        /// <summary>
        /// Instance of a wrapped logger
        /// </summary>
        public static Logger Instance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return LoggerDefault.Instance; }
        }

        /// <summary>
        /// Set logger singleton instance
        /// </summary>
        /// <param name="newDefault">New instance</param>
        public static void SetInstance(Logger newDefault)
        {
            LoggerDefault.SetInstance(newDefault);
        }
        /// <summary>
        /// Reset logger singleton
        /// </summary>
        public static void ResetInstance()
        {
            LoggerDefault.ResetInstance();
        }
        /// <summary>
        /// Load logger singleton from AppConfig
        /// </summary>
        /// <param name="sectionName">Section name in AppConfig</param>
        public static void LoadInstanceFromAppConfig(string sectionName = "LoggerConfigurationSection")
        {
            LoggerDefault.LoadInstanceFromAppConfig(sectionName);
        }



        /// <summary>
        /// Creates the logger which bound to the concrete type.
        /// (add type information to log message without slow StackTrace extraction)
        /// </summary>
        /// <param name="typeInfo">Type</param>
        /// <returns>Instance of logger for passed type</returns>
        public static Logger GetClassLogger(Type typeInfo)
        {
            return Instance.GetClassLogger(typeInfo);
        }

        /// <summary>
        /// Creates the logger which bound to the type from which this method is calling
        /// </summary>
        /// <returns>Instance of logger for current class</returns>
        public static Logger GetThisClassLogger()
        {
            return Instance.GetThisClassLogger();
        }
    }
}
