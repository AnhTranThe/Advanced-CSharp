namespace Advanced_CSharp.Service.Interfaces
{
    public interface IUnitWork
    {

        Task<bool> CompleteAsync(string userName);

    }
}
