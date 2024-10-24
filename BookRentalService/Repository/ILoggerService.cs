namespace BookRentalService.Repository
{
    //public interface ILoggerService<T>
    //{
    //    void LogInformation(string message, params object[] args);
    //    void LogWarning(string message, params object[] args);
    //    void LogError(Exception ex, string message, params object[] args);
    //}

    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }
}
