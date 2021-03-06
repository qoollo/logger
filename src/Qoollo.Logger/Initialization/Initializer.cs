﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qoollo.Logger.Initialization
{
    /// <summary>
    /// Helps with initialization of dependant logger singletons (logger stacking)
    /// </summary>
    public static class Initializer
    {
        private static readonly Logger _thisClassSupportLogger = InnerSupportLogger.Instance.GetClassLogger(typeof(Initializer));


        private static List<Type> FindAllLoggersInAsm(Assembly asm)
        {
            Contract.Requires(asm != null);

            try
            {
                return asm.GetTypes().Where(o => o.IsSubclassOf(typeof(Logger))).ToList();
            }
            catch (ReflectionTypeLoadException tpLdEx)
            {
                _thisClassSupportLogger.Fatal(tpLdEx, "Can't load type from assembly: " + asm.ToString());
                throw;
            }
        }

        private static bool IsAppropriateInitMethod(MethodInfo method)
        {
            Contract.Requires(method != null);

            if (!method.IsDefined(typeof(LoggerWrapperInitializationMethodAttribute)))
                return false;

            return method.GetParameters().All(o =>
                    o.ParameterType == typeof(ILogger) ||
                    o.ParameterType == typeof(LogLevel));
        }

        private static MethodInfo ExtractInitializationMethod(Type tp)
        {
            Contract.Requires(tp != null);

            var allMeth = tp.GetMethods(BindingFlags.Static | BindingFlags.Public);
            if (allMeth == null || allMeth.Length == 0)
                return null;

            var filteredMethods = allMeth.Where(o => IsAppropriateInitMethod(o)).ToList();
            if (filteredMethods.Count == 0)
                return null;


            var byWrapper = filteredMethods.FirstOrDefault(o => o.GetParameters().Length == 1 && o.GetParameters()[0].ParameterType == typeof(ILogger));
            if (byWrapper != null)
                return byWrapper;

            return filteredMethods[0];
        }



        private static void InitLogger(MethodInfo method, ILogger wrapper)
        {
            Contract.Requires(method != null);
            Contract.Requires(wrapper != null);

            var allParams = method.GetParameters();

            object[] parameters = new object[allParams.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (allParams[i].ParameterType == typeof(ILogger))
                    parameters[i] = wrapper;
                else if (allParams[i].ParameterType == typeof(LogLevel))
                    parameters[i] = wrapper.Level;
                else
                    throw new ArgumentException("Bad method description");
            }

            method.Invoke(null, parameters);
        }


        /// <summary>
        /// Method for initialization logger singleton in dependant assembly (with the help of Reflection)
        /// </summary>
        /// <param name="logger">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="assembly">Assembly where the loggers will be initialized</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int InitializeLoggerInAssembly(ILogger logger, Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(assembly != null);
            Contract.Requires<ArgumentNullException>(logger != null);

            var allLoggerInitMethods = FindAllLoggersInAsm(assembly).Select(ExtractInitializationMethod).Where(o => o != null).ToList();

            foreach (var elem in allLoggerInitMethods)
                InitLogger(elem, logger);

            return allLoggerInitMethods.Count;
        }

        /// <summary>
        /// Method for initialization logger singleton in dependant assemblies (with the help of Reflection)
        /// </summary>
        /// <param name="logger">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="assembly">Collection of dependant assemblies where the loggers will be initialized</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int InitializeLoggerInAssembly(ILogger logger, IEnumerable<Assembly> assembly)
        {
            Contract.Requires<ArgumentNullException>(assembly != null);
            Contract.Requires<ArgumentNullException>(logger != null);
            
            int res = 0;
            foreach (var asm in assembly)
                res += InitializeLoggerInAssembly(logger, asm);

           return res;
        }


        /// <summary>
        /// Initialize logger signleton in all referenced assemblies
        /// </summary>
        /// <param name="logger">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int InitializeLoggerInReferencedAssemblies(ILogger logger)
        {
            Contract.Requires<ArgumentNullException>(logger != null);

            var callingAssembly = Assembly.GetCallingAssembly();
            var referencedAssemblies = callingAssembly.GetReferencedAssemblies();

            return InitializeLoggerInAssembly(logger, referencedAssemblies.Select(o => Assembly.Load(o)));
        }



        /// <summary>
        /// Initialize logger signleton in dependant assembly. This logger is used as parent.
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="assembly">Assembly where the loggers will be initialized</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int PassForInitializationToAssembly(this ILogger wrapper, Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(assembly != null);

            return InitializeLoggerInAssembly(wrapper, assembly);
        }

        /// <summary>
        /// Initialize logger signleton in dependant assemblies. This logger is used as parent.
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="assembly">Array of dependant assemblies where the loggers will be initialized</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int PassForInitializationToAssembly(this ILogger wrapper, params Assembly[] assembly)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(assembly != null);

            return InitializeLoggerInAssembly(wrapper, assembly);
        }

        /// <summary>
        /// Initialize logger signleton in dependant assembly. This logger is used as parent.
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="type">Any type in dependant assembly (will be used to extract assembly as 'type.Assembly')</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int PassForInitializationToAssembly(this ILogger wrapper, Type type)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(type != null);

            return InitializeLoggerInAssembly(wrapper, type.Assembly);
        }

        /// <summary>
        /// Initialize logger signleton in dependant assemblies. This logger is used as parent.
        /// </summary>
        /// <param name="wrapper">Parent logger instance that will be wrapped by assembly logger singleton</param>
        /// <param name="types">Array of types from dependant assemblies (will be used to extract assembly as 'type.Assembly')</param>
        /// <returns>The number of loggers initialized by this call</returns>
        public static int PassForInitializationToAssembly(this ILogger wrapper, params Type[] types)
        {
            Contract.Requires<ArgumentNullException>(wrapper != null);
            Contract.Requires<ArgumentNullException>(types != null);

            return InitializeLoggerInAssembly(wrapper, types.Select(t => t.Assembly));
        }


    }
}
