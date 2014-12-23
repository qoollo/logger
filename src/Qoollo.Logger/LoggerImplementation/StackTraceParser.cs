using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Libs.Logger.Implementation
{
    internal class StackTraceShot
    {
        public enum StackTraceFormat
        {
            Raw,
            Flat,
            DetailedFlat
        }

        /// <summary>
        /// Задает формат вывода StackTrace в виде строки
        /// </summary>
        [DefaultValue("Flat")]
        public StackTraceFormat Format { get; set; }

        /// <summary>
        /// Задает верхушку StackTrace (определяет длину выводимой последовательности вывозов)
        /// </summary>
        [DefaultValue(3)]
        public int TopFrames { get; set; }

        /// <summary>
        /// Задает разделитель между фраймами при преобразовании в строку
        /// </summary>
        [DefaultValue(" => ")]
        public string Separator { get; set; }

        /// <summary>
        /// Свойство содержит значение текущего StackTrace
        /// Обновление поля происходит через метод RefreshStackTrace
        /// </summary>
        public StackTrace StackTrace { get; set; }

        /// <summary>
        /// Номер первого фрайма не принадлежащего библиотеке логирования и относящегося к пользовательсокому коду
        /// </summary>
        public int FirstUserFrameNumber { get; set; }

        /// <summary>
        /// Первый фрайм не принадлежащий библиотеке логирования и относящийся к пользовательсокому коду
        /// </summary>
        public StackFrame FirstUserFrame { get; set; }

        /// <summary>
        /// Возвращает имя класса, откуда было вызвано логирование
        /// </summary>
        public string Class
        {
            get { return FirstUserFrame.GetMethod().DeclaringType.FullName; }
        }

        /// <summary>
        /// Возвращает имя метода, откуда было вызвано логирование
        /// </summary>
        public string Method
        {
            get { return FirstUserFrame.GetMethod().Name; }
        }

        /// <summary>
        /// Возвращает имя файла исходного кода, откуда было вызвано логирование
        /// </summary>
        public string FilePath
        {
            get { return FirstUserFrame.GetFileName(); }
        }

        /// <summary>
        /// Возвращает номер строки в файле исходного кода, откуда было вызвано логирование
        /// </summary>
        public int LineNumber
        {
            get { return FirstUserFrame.GetFileLineNumber(); }
        }

        #region ForFuture

        private string ParseForFutureMaybe()
        {
            bool first = true;
            var builder = new StringBuilder();
            int startingFrame = FirstUserFrameNumber + TopFrames - 1;

            if (startingFrame >= StackTrace.FrameCount)
            {
                startingFrame = StackTrace.FrameCount - 1;
            }

            switch (this.Format)
            {
                case StackTraceFormat.Raw:
                    for (int i = startingFrame; i >= FirstUserFrameNumber; --i)
                    {
                        var f = StackTrace.GetFrame(i);
                        builder.Append(f.ToString());
                    }

                    break;

                case StackTraceFormat.Flat:
                    for (int i = startingFrame; i >= FirstUserFrameNumber; --i)
                    {
                        StackFrame f = StackTrace.GetFrame(i);
                        if (!first)
                        {
                            builder.Append(this.Separator);
                        }

                        var type = f.GetMethod().DeclaringType;

                        if (type != null)
                        {
                            builder.Append(type.Name);
                        }
                        else
                        {
                            builder.Append("<no type>");
                        }

                        builder.Append(".");
                        builder.Append(f.GetMethod().Name);
                        first = false;
                    }

                    break;

                case StackTraceFormat.DetailedFlat:
                    for (int i = startingFrame; i >= FirstUserFrameNumber; --i)
                    {
                        StackFrame f = StackTrace.GetFrame(i);
                        if (!first)
                        {
                            builder.Append(this.Separator);
                        }

                        builder.Append("[");
                        builder.Append(f.GetMethod());
                        builder.Append("]");
                        first = false;
                    }

                    break;
            }

            return builder.ToString();
        }

        #endregion
    }

    static class StackTraceParser
    {
        private static readonly Assembly LogAssembly = typeof(Logger).Assembly;
        private static readonly Assembly MscorlibAssembly = typeof(string).Assembly;
        private static readonly Assembly SystemAssembly = typeof(Debug).Assembly;

        #region Работа со StackTrace

        public static StackTraceShot GetStackTrace()
        {
            var stackTraceShot = new StackTraceShot();

            stackTraceShot.StackTrace = new StackTrace(true); // true means get line numbers.
            stackTraceShot.FirstUserFrameNumber = FindCallingMethodOnStackTrace(stackTraceShot.StackTrace, typeof(Logger));
            stackTraceShot.FirstUserFrame = stackTraceShot.StackTrace.GetFrame(stackTraceShot.FirstUserFrameNumber);

            return stackTraceShot;
        }

        private static bool SkipAssembly(Assembly assembly)
        {
            if (assembly == LogAssembly)
            {
                return true;
            }

            if (assembly == MscorlibAssembly)
            {
                return true;
            }

            if (assembly == SystemAssembly)
            {
                return true;
            }

            return false;
        }

        private static int FindCallingMethodOnStackTrace(StackTrace stackTrace, Type loggerType)
        {
            StackFrame curFrame = null;

            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                curFrame = stackTrace.GetFrame(i);
                MethodBase mb = curFrame.GetMethod();

                if (mb.DeclaringType == null)
                    continue;

                var methodAssembly = mb.DeclaringType.Assembly;

                if (!SkipAssembly(methodAssembly) && mb.DeclaringType != loggerType)
                    return i;
            }

            return 0;
        }

        #endregion
    }
}
