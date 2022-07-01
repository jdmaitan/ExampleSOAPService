using System.Data.Entity;
using System.Data.SQLite;
using System.Web;

namespace FiscalInfoWebService.Data
{
    public class FiscalInfoDbContext : DbContext
    {
        private static readonly string CONNECTION_STRING = $@"data source={HttpRuntime.AppDomainAppPath}FiscalInfoDb.db";

        public FiscalInfoDbContext() : base(new SQLiteConnection() { ConnectionString = CONNECTION_STRING }, true)
        {

        }

        public DbSet<Taxpayer> Taxpayers { get; set; }
        public DbSet<GeneralMessage> GeneralMessages { get; set; }
        public DbSet<SpecificMessage> SpecificMessages { get; set; }
        public DbSet<ValidRIFLetter> ValidRIFLetters { get; set; }
        public DbSet<ServiceStatus> ServiceStatus { get; set; }
    }
}