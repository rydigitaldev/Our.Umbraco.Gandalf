using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models.Pocos
{
    [TableName("AllowedIp")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class AllowedIp
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("ipAddress")]
        public string ipAddress { get; set; }

        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }
    }
}
