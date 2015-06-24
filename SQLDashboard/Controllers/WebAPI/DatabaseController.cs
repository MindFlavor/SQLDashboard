using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQLDashboard.Controllers.WebAPI
{
    public class DatabaseController : ApiController
    {
        public IEnumerable<Models.DatabaseListEntry> Get(Guid id)
        {
            var ret = SQLServersController.PerformCommand(id, "SQLDashboard.TSQL.DatabaseList.sql");

            foreach (System.Data.DataRow row in ret.Rows)
            {
                yield return new Models.DatabaseListEntry()
                {
                    DatabaseName = (string)row[0],
                    database_id = (int)row[1],
                    owner_sid = (byte[])row[2],
                    OwnerName = (string)row[3],
                    create_date = (DateTime)row[4],
                    compatibility_level = (byte)row[5],
                    collation_name = (string)row[6],
                    user_access_desc = (string)row[7],
                    is_read_only = (bool)row[8],
                    is_auto_close_on = (bool)row[9],
                    is_auto_shrink_on = (bool)row[10],
                    state_desc = (string)row[11],
                    is_in_standby = (bool)row[12],
                    is_cleanly_shutdown = (bool)row[13],
                    snapshot_isolation_state_desc = (string)row[14],
                    is_read_committed_snapshot_on = (bool)row[15],
                    recovery_model_desc = (string)row[16],
                    page_verify_option_desc = (string)row[17],
                    is_auto_create_stats_on = (bool)row[18],
                    is_auto_update_stats_on = (bool)row[19],
                    is_auto_update_stats_async_on = (bool)row[20],
                    is_fulltext_enabled = (bool)row[21],
                    is_trustworthy_on = (bool)row[22],
                    is_db_chaining_on = (bool)row[23],
                    is_parameterization_forced = (bool)row[24],
                    is_published = (bool)row[25],
                    is_subscribed = (bool)row[26],
                    is_merge_published = (bool)row[27],
                    is_distributor = (bool)row[28],
                    is_broker_enabled = (bool)row[29],
                    log_reuse_wait_desc = (string)row[30],
                    //is_cdc_enabled = (bool)row[31],
                    //containment_desc = (string)row[32]
                };
            }
        }
    }
}
