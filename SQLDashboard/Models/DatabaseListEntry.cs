using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLDashboard.Models
{
    public class DatabaseListEntry
    {
        #region Properties
        public string DatabaseName { get; set; }
        public int database_id { get; set; }
        public byte[] owner_sid { get; set; }
        public string OwnerName { get; set; }
        public DateTime create_date { get; set; }
        public int compatibility_level { get; set; }
        public string collation_name { get; set; }
        public string user_access_desc { get; set; }
        public bool is_read_only { get; set; }
        public bool is_auto_close_on { get; set; }
        public bool is_auto_shrink_on { get; set; }
        public string state_desc { get; set; }
        public bool is_in_standby { get; set; }
        public bool is_cleanly_shutdown { get; set; }
        public string snapshot_isolation_state_desc { get; set; }
        public bool is_read_committed_snapshot_on { get; set; }
        public string recovery_model_desc { get; set; }
        public string page_verify_option_desc { get; set; }
        public bool is_auto_create_stats_on { get; set; }
        public bool is_auto_update_stats_on { get; set; }
        public bool is_auto_update_stats_async_on { get; set; }
        public bool is_fulltext_enabled { get; set; }
        public bool is_trustworthy_on { get; set; }
        public bool is_db_chaining_on { get; set; }
        public bool is_parameterization_forced { get; set; }
        public bool is_published { get; set; }
        public bool is_subscribed { get; set; }
        public bool is_merge_published { get; set; }
        public bool is_distributor { get; set; }
        public bool is_broker_enabled { get; set; }
        public string log_reuse_wait_desc { get; set; }
        public bool is_cdc_enabled { get; set; }
        public string containment_desc { get; set; }
        #endregion
    }
}