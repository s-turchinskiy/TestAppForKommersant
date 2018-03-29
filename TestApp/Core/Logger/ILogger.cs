
namespace TestApp
{
    public interface ILogger
    {
        void Info(string message);
        void Info(string message, params object[] args);
        void Warning(string message);
        void Warning(string message, params object[] args);
        void Error(string message);
        void Error(string message, params object[] args);
        void Exception(string message, System.Exception ex);
    }
}
