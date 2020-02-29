using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.IpLocker.Core.StartUp.Migrations
{
	public class CreateRedirectsTable : MigrationBase
    {
        private const string REDIRECTS_TABLE_NAME = "AllowedIp";

        public CreateRedirectsTable(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists(REDIRECTS_TABLE_NAME))
            {
                this.Create.Table<AllowedIp>().Do();
            }
        }
    }

    public class CreateRedirectsTableMigrationPlan : MigrationPlan
    {
        public CreateRedirectsTableMigrationPlan() : base("Our.Umbraco.IpLocker")
        {
            From(string.Empty).To<CreateRedirectsTable>("first-migration");
        }
    }
}
