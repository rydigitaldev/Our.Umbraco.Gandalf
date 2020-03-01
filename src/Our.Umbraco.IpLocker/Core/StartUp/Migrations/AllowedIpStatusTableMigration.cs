using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.IpLocker.Core.StartUp.Migrations
{
	public class AllowedIpStatusTableMigration : MigrationBase
    {
        private const string TABLE_NAME = "AllowedIpStatus";

        public AllowedIpStatusTableMigration(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists(TABLE_NAME))
            {
                this.Create.Table<AllowedIp>().Do();
            }
        }
    }

    public class AllowedIpStatusTableMigrationPlan : MigrationPlan
    {
        public AllowedIpStatusTableMigrationPlan() : base("Our.Umbraco.IpLocker")
        {
            From(string.Empty).To<AllowedIpTableMigration>("first-migration");
        }
    }
}
