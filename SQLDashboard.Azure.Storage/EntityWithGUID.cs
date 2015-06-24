using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLDashboard.Azure.Storage
{
    public class EntityWithGUID : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {

        public Guid GUID { get; set; }
        public string Entity { get; set; }

        public int Number { get; set; }
    }
}