using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Service.Interfaces;

namespace Advanced_CSharp.Service.Services
{
    public class UnitWork : IUnitWork
    {

        private readonly AdvancedCSharpDbContext _context;
        public UnitWork(AdvancedCSharpDbContext context)
        {

            _context = context;
        }
        public async Task<bool> CompleteAsync(string userName)
        {
            return await _context.SaveChangesAsync(userName) > 0;
        }
    }
}
