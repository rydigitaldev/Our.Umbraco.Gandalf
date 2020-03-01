using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Core.Scoping;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;

namespace Our.Umbraco.IpLocker.Core.StartUp.Migrations
{
	public class MigrationsComposer: IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components()
                 .Append<MigrationsComponent>();
        }
    }

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
			var upgrader = new Upgrader(new CreateAllowedIpTableMigrationPlan());
			upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);
		}

		public void Terminate()
		{

		}
	}
}
