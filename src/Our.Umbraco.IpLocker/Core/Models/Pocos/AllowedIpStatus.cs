using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models.Pocos
{
    [TableName("AllowedIpStatus")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class AllowedIpStatus
	{
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }

    }
}
