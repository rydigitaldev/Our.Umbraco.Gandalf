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
            composition.Components().Append<IpTableMigrationComponent>();
            composition.Components().Append<StatusTableMigrationComponent>();
        }
    }

	public class IpTableMigrationComponent : IComponent
	{
		private readonly IScopeProvider _scopeProvider;
		private readonly IMigrationBuilder _migrationBuilder;
		private readonly IKeyValueService _keyValueService;
		private readonly ILogger _logger;

		public IpTableMigrationComponent(IScopeProvider scopeProvider,IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
		{
			_scopeProvider = scopeProvider;
			_migrationBuilder = migrationBuilder;
			_keyValueService = keyValueService;
			_logger = logger;
		}

		public void Initialize()
		{
			// perform any upgrades (as needed)
			var upgrader = new Upgrader(new AllowedIpTableMigrationPlan());
			upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
		}

		public void Terminate(){ }
	}



	public class StatusTableMigrationComponent : IComponent
	{
		private readonly IScopeProvider _scopeProvider;
		private readonly IMigrationBuilder _migrationBuilder;
		private readonly IKeyValueService _keyValueService;
		private readonly ILogger _logger;

		public StatusTableMigrationComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
		{
			_scopeProvider = scopeProvider;
			_migrationBuilder = migrationBuilder;
			_keyValueService = keyValueService;
			_logger = logger;
		}

		public void Initialize()
		{
			// perform any upgrades (as needed)
			var upgrader = new Upgrader(new AllowedIpStatusTableMigrationPlan());
			upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
		}

		public void Terminate() { }
	}
}
