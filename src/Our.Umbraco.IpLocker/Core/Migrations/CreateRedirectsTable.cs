using Semver;
using Our.Umbraco.IpLocker.Core.Models;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.IpLocker.Core.Migrations
{
    //[Migration("1.0.1", 1, "Redirects")]
    public class CreateRedirectsTable : MigrationBase
    {
        private SemVersion _targetVersion = new SemVersion(1, 0, 1);
        private const string REDIRECTS_TABLE_NAME = "AllowedIps";

        public CreateRedirectsTable(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists(REDIRECTS_TABLE_NAME))
            {
                this.Create.Table<Redirect>().Do();
            }
        }
    }

    public class CreateRedirectsTableMigrationPlan : MigrationPlan
    {
        public CreateRedirectsTableMigrationPlan() : base("IpLocker")
        {
            From(string.Empty).To<CreateRedirectsTable>("first-migration");
        }
    }
}
