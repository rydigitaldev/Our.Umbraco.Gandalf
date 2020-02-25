using Semver;
using Simple301.Core.Models;
using Umbraco.Core.Migrations;

namespace Simple301.Core.Migrations
{
    //[Migration("1.0.1", 1, "Redirects")]
    public class CreateRedirectsTable : MigrationBase
    {
        private SemVersion _targetVersion = new SemVersion(1, 0, 1);
        private const string REDIRECTS_TABLE_NAME = "Redirects";

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
        public CreateRedirectsTableMigrationPlan() : base("Simple301")
        {
            From(string.Empty).To<CreateRedirectsTable>("first-migration");
        }
    }
}
