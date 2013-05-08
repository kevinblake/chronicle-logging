using System.Data.Entity;
using Chronicle.Logging.Business.Entities;

namespace Chronicle.Logging.Business.DataAccess
{
    public class LogDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

    }
}