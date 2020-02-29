﻿using NPoco;
using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    [TableName("Redirects")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class Redirect
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("IsRegex")]
        public bool IsRegex { get; set; }

        [Column("OldUrl")]
        public string OldUrl { get; set; }

        [Column("NewUrl")]
        public string NewUrl { get; set; }

        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }
    }
}
