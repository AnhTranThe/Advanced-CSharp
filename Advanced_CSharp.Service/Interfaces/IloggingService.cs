namespace Advanced_CSharp.Service.Interfaces
{
    public interface IloggingService
    {
        void LogError(Exception exception);
        void LogInfo(string message);
    }
}
