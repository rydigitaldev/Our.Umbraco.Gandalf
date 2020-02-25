using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Core.Composing;
using Umbraco.Core.Scoping;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Simple301.Core.Migrations;

namespace Simple301.Core.Components
{
    public class MigrationsComponent : IComponent
    {
        private readonly IScopeProvider scopeProvider;
        private readonly IMigrationBuilder migrationBuilder;
        private readonly IKeyValueService keyValueService;
        private readonly ILogger logger;

        public MigrationsComponent(
            IScopeProvider scopeProvider,
            IMigrationBuilder migrationBuilder,
            IKeyValueService keyValueService,
            ILogger logger)
        {
            this.scopeProvider = scopeProvider;
            this.migrationBuilder = migrationBuilder;
            this.keyValueService = keyValueService;
            this.logger = logger;
        }

        public void Initialize()
        {
            // perform any upgrades (as needed)
            var upgrader = new Upgrader(new CreateRedirectsTableMigrationPlan());
            upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);
        }

        public void Terminate()
        {

        }
    }
}
