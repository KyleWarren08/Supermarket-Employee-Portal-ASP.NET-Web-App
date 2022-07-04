using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ABCSupermarket_19001700_Final.TableHandler
{
    public class TableManager
    {
        private CloudTable table;

        //check if table name is empty
        public TableManager(string _CloudTableName)
        {
            if (string.IsNullOrEmpty(_CloudTableName))
            {
                throw new ArgumentNullException("Table", "Table name cannot be empty");
            }
            try
            {
                //Get azure table storage connection string
                string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=abcstoragemarket;AccountKey=5jogWcVdguvjtA4LDEdD2ZVSaYsupwxyW+z4CXZ/QTY/LyAb3xpRMVZXAuP8qiKkjsP3SGctl+xK1mxBbDlRiA==;EndpointSuffix=core.windows.net";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

                //creates table if it does not exist
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                table = tableClient.GetTableReference(_CloudTableName);
                table.CreateIfNotExists();
            }
            catch (StorageException StorageExceptionObj)
            {
                throw StorageExceptionObj;
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }

        // Retrieve Products (Get > List)
        public List<T> RetrieveEntity<T>(String Query = null) where T : TableEntity, new()
        {
            try
            {
                TableQuery<T> DataTableQuery = new TableQuery<T>();
                if (!string.IsNullOrEmpty(Query))
                {
                    DataTableQuery = new TableQuery<T>().Where(Query);
                }
                IEnumerable<T> IDataList = table.ExecuteQuery(DataTableQuery);
                List<T> DataList = new List<T>();
                foreach (var singleItem in IDataList)
                    DataList.Add(singleItem);
                return DataList;
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }

        //Insert Products
        public void InsertEntity<T>(T entity, bool forInsert = true) where T : TableEntity, new()
        {
            try
            {
                if (forInsert)
                {
                    var InsertOperation = TableOperation.Insert(entity);
                    table.Execute(InsertOperation);
                }
                else
                {
                    var InsertOrReplaceOperation = TableOperation.InsertOrReplace(entity);
                    table.Execute(InsertOrReplaceOperation);
                }
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }

        //Delete Products
        public bool DeleteEntity<T>(T entity) where T : TableEntity, new()
        {
            try
            {
                var DeleteOperation = TableOperation.Delete(entity);
                table.Execute(DeleteOperation);
                return true;
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }
    }
}