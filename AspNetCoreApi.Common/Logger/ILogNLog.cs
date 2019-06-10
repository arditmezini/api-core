namespace AspNetCoreApi.Common.Logger
{
    public interface ILogNLog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
