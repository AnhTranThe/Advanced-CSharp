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
            // Check if the provided userName is not null or empty
            if (!string.IsNullOrEmpty(userName))
            {
                // Save changes with the provided userName for auditing
                return await _context.SaveChangesAsync(userName) > 0;
            }
            else
            {
                // Save changes without specifying a userName
                return await _context.SaveChangesAsync() > 0;
            }
        }
    }
}
