using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLDashboard.Models
{
    public class CachedPlanSnapshotEntry
    {
        public string cacheobjtype { get; set; }
        public string objtype { get; set; }

        public long count { get; set;}

        public long sizeInBytes { get; set; }
    }
}