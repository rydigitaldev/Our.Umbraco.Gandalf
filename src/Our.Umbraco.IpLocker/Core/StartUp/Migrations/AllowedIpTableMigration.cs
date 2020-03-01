using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.IpLocker.Core.StartUp.Migrations
{
	public class AllowedIpTableMigration : MigrationBase
    {
        private const string TABLE_NAME = "AllowedIp";

        public AllowedIpTableMigration(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists(TABLE_NAME))
            {
                this.Create.Table<AllowedIp>().Do();
            }
        }
    }

    public class AllowedIpTableMigrationPlan : MigrationPlan
    {
        public AllowedIpTableMigrationPlan() : base("Our.Umbraco.IpLocker")
        {
            From(string.Empty).To<AllowedIpTableMigration>("first-migration");
        }
    }
}
