using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pe.Installer
{
    public enum LogKind
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
    }

    public interface ILogger
    {
        #region function

        void LogTrace(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogDebug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogInfo(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogWarning(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogError(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        #endregion
    }

    public interface ILoggerFactory
    {
        #region function

        ILogger CreateLogger(Type type);

        #endregion
    }

    internal class InternalLogger: ILogger
    {
        public InternalLogger(ListBox listBox, string name)
        {
            ListBox = listBox;
            Name = name;
        }

        #region property

        ListBox ListBox { get; }

        public string Name { get; }

        #endregion

        #region function

        void Log(LogKind logKind, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            if(ListBox.InvokeRequired) {
                ListBox.BeginInvoke(new Action(() => {
                    ListBox.Items.Add($"{logKind} {message}");
                }));
            } else {
                ListBox.Items.Add($"{logKind} {message}");
            }
        }

        #endregion

        #region ILogger

        /// <inheritdoc cref="ILogger.LogTrace(string, string, string, int)"/>
        public void LogTrace(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Trace, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.LogDebug(string, string, string, int)"/>
        public void LogDebug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Debug, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.Information(string, string, string, int)"/>
        public void LogInfo(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Information, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.LogWarning(string, string, string, int)"/>
        public void LogWarning(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Warning, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.LogError(string, string, string, int)"/>
        public void LogError(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Error, message, callerMemberName, callerFilePath, callerLineNumber);

        #endregion
    }

    internal class InternalLoggerFactory: ILoggerFactory
    {
        public InternalLoggerFactory(ListBox listBox)
        {
            ListBox = listBox;
        }

        #region property

        ListBox ListBox { get; }

        #endregion

        #region ILoggerFactory

        public ILogger CreateLogger(Type type)
        {
            return new InternalLogger(ListBox, type.FullName);
        }

        #endregion
    }

}
