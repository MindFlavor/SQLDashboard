using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace SQLDashboard.Azure.Storage
{
    public class InstanceConnections
    {
        public CloudStorageAccount StorageAccount { get; }

        protected CloudTable Table { get; set; }

        public InstanceConnections(string connectionString)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient ctc = StorageAccount.CreateCloudTableClient();// new CloudTableClient(StorageAccount.BlobStorageUri, StorageAccount.Credentials);

            Table = ctc.GetTableReference("SQLDashboard");
            Table.CreateIfNotExists();
        }

        public IEnumerable<EntityWithGUID> Get()
        {
            var query = new TableQuery<EntityWithGUID>();
            var ret = Table.ExecuteQuery(query);
            return ret;
            //TableQuery<SQLConnectionString> query = new TableQuery<SQLConnectionString>().AsEnumerable();
        }

        public void Put(EntityWithGUID e)
        {
            e.PartitionKey = ""; // all in the same partition
            e.RowKey = e.GUID.ToString();
            TableOperation to = TableOperation.InsertOrReplace(e);
            var result = Table.Execute(to);
        }

        public void Delete(Guid guid)
        {
            EntityWithGUID ew = new EntityWithGUID() { GUID = guid, PartitionKey = "", RowKey = guid.ToString(), ETag="*" };
            TableOperation to = TableOperation.Delete(ew);
            var result = Table.Execute(to);
        }
    }
}
