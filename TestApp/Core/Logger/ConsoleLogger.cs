using System;

namespace TestApp
{
    internal class ConsoleLogger : ILogger
    {
        void ILogger.Info(string message)
        {
            Console.WriteLine("Info: " + message);
        }
        void ILogger.Info(string message, params object[] args)
        {
            Console.WriteLine("Info: " + string.Format(message, args));
        }
        void ILogger.Warning(string message)
        {
            Console.WriteLine("Warning: " + message);
        }
        void ILogger.Warning(string message, params object[] args)
        {
            Console.WriteLine("Warning: " + string.Format(message, args));
        }
        void ILogger.Error(string message)
        {
            Console.WriteLine("Error: " + message);
        }
        void ILogger.Error(string message, params object[] args)
        {
            Console.WriteLine("Error: " + string.Format(message, args));
        }
        void ILogger.Exception(string message, Exception ex)
        {
            Console.WriteLine("Exception: " + message + " " + ex.Message + "\n" + ex.StackTrace);
        }
    }
}
