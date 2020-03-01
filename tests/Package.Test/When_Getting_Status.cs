using Moq;
using Our.Umbraco.IpLocker.Core.Enums;
using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Repositories;
using Our.Umbraco.IpLocker.Core.Services;
using System;
using Xunit;

namespace Package.Test
{
    public class When_Getting_Status
    {
		Mock<IStatusRepository> _statusRepositoryMock;

		IStatusService _statusService; 
		public When_Getting_Status()
		{
			_statusRepositoryMock = new Mock<IStatusRepository>();
			_statusService = new StatusService(_statusRepositoryMock.Object);
		}


        [Fact]
        public void Expected_Status_Is_Returned()
        {
			var expected = new AllowedIpStatus()
			{
				Id = 123,
				Status = Status.Enabled.ToString(),
				LastUpdated = new DateTime(2020, 1, 1)
			};

			_statusRepositoryMock.Setup(x => x.GetStatus()).Returns(expected);

			var result = _statusService.GetStatus();

			Assert.Equal(result.Status, Status.Enabled.ToString());
		}
    }
}
