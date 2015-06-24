using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace SQLDashboard.Azure.Storage
{
    public class SQLConnectionString
    {
        public Guid GUID { get; set; }
        public ApplicationIntent ApplicationIntent { get; set; }
        public string ApplicationName { get; set; }
        public int ConnectTimeout { get; set; }
        public string DataSource { get; set; }
        public bool Encrypt { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string Password { get; set; }
        public bool PersistSecurityInfo { get; set; }
        public bool TrustServerCertificate { get; set; }
        public string UserID { get; set; }
        public string WorkstationID { get; set; }

        public string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                scsb.ApplicationIntent = ApplicationIntent;
                scsb.ApplicationName = ApplicationName;
                scsb.ConnectTimeout = ConnectTimeout;
                scsb.DataSource = DataSource;
                scsb.Encrypt = Encrypt;
                scsb.InitialCatalog = InitialCatalog;
                scsb.IntegratedSecurity = IntegratedSecurity;
                scsb.Password = Password;
                scsb.PersistSecurityInfo = PersistSecurityInfo;
                scsb.TrustServerCertificate = TrustServerCertificate;
                scsb.UserID = UserID;
                scsb.Password = Password;

                return scsb.ConnectionString;
            }
        }
    }
}
