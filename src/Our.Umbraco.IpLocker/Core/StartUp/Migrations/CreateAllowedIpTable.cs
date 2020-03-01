using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.IpLocker.Core.StartUp.Migrations
{
	public class CreateAllowedIpTable : MigrationBase
    {
        private const string TABLE_NAME = "AllowedIp";

        public CreateAllowedIpTable(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists(TABLE_NAME))
            {
                this.Create.Table<AllowedIp>().Do();
            }
        }
    }

    public class CreateAllowedIpTableMigrationPlan : MigrationPlan
    {
        public CreateAllowedIpTableMigrationPlan() : base("Our.Umbraco.IpLocker")
        {
            From(string.Empty).To<CreateAllowedIpTable>("first-migration");
        }
    }
}
