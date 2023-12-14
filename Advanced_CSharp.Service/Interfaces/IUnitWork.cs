namespace Advanced_CSharp.Service.Interfaces
{
    public interface IUnitWork
    {
        /// <summary>
        /// CompleteAsync
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> CompleteAsync(string userName);

    }
}
