using Moq;
using Our.Umbraco.IpLocker.Core.Enums;
using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Repositories;
using Our.Umbraco.IpLocker.Core.Services;
using System;
using Xunit;

namespace Package.Test
{
    public class When_Changing_Status
	{
		Mock<IStatusRepository> _statusRepositoryMock;

		IStatusService _statusService; 
		public When_Changing_Status()
		{
			_statusRepositoryMock = new Mock<IStatusRepository>();
			_statusService = new StatusService(_statusRepositoryMock.Object);
		}


        [Fact]
        public void Status_Is_Set_And_Returned()
        {
			var poco = new AllowedIpStatus()
			{
				Id = 123,
				Status = Status.Enabled.ToString(),
				LastUpdated = new DateTime(2020, 1, 1)
			};

			_statusRepositoryMock.Setup(x => x.InsertStatus(It.IsAny<AllowedIpStatus>())).Returns(poco);

			var result = _statusService.SetStatus(Status.Enabled.ToString());

			Assert.Equal(result.Status, Status.Enabled.ToString());
		}
    }
}
