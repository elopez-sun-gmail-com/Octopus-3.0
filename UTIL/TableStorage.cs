using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;

namespace UTIL
{
    /// <summary>
    /// 
    /// </summary>
    public class TableStorage
    {
        private static string CONNECTION_STRING_AZURE;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        public static void setConnectionStrings(string connectionStrings)
        {
            TableStorage.CONNECTION_STRING_AZURE = connectionStrings;// ConfigurationManager.ConnectionStrings[appSettings].ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static async Task<CloudTable> getConnection(string tableName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(TableStorage.CONNECTION_STRING_AZURE);

            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();

            CloudTable cloudTable = tableClient.GetTableReference(tableName);

            await cloudTable.CreateIfNotExistsAsync();

            return cloudTable;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="obj"></param>
        public static async Task Insert(string tableName, TableEntity obj)
        {
            CloudTable cloudTable = await TableStorage.getConnection(tableName);

            TableOperation tableOperation = TableOperation.InsertOrMerge(obj);

            await cloudTable.ExecuteAsync(tableOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="list"></param>
        public static async Task Insert<T>(string tableName, List<T> list)
        {
            ArrayList lista = new ArrayList(list);

            CloudTable cloudTable = await TableStorage.getConnection(tableName);

            TableBatchOperation tableBatchOperation = new TableBatchOperation();

            foreach (TableEntity item in lista)
            {
                tableBatchOperation.InsertOrReplace(item); //InsertOrMerge
            }

            await cloudTable.ExecuteBatchAsync(tableBatchOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<DynamicTableEntity> query(string tableName, string partitionKey, string rowkey)
        {
            CloudTable cloudTable = await TableStorage.getConnection(tableName);

            var tableResults = await cloudTable.ExecuteAsync(TableOperation.Retrieve(partitionKey, rowkey));

            var entitys = (DynamicTableEntity)tableResults.Result;

            return entitys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<List<DynamicTableEntity>> query(string tableName)
        {
            CloudTable cloudTable = await TableStorage.getConnection(tableName);

            var condition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual, "123456");
            var querys = new TableQuery().Where(condition);

            //List<DynamicTableEntity> lst = cloudTable.ExecuteQuery(querys).ToList();

            TableQuerySegment tableQuerySegment = await cloudTable.ExecuteQuerySegmentedAsync(querys, null);

            IEnumerator<DynamicTableEntity> lista = tableQuerySegment.GetEnumerator();

            List<DynamicTableEntity> lst = new List<DynamicTableEntity>();

            while (lista.MoveNext())
            {
                lst.Add(lista.Current);
            }

            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column"></param>
        /// <param name="queryComparisons"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<List<DynamicTableEntity>> query(string tableName, string column, string queryComparisons, string value)
        {
            CloudTable cloudTable = await TableStorage.getConnection(tableName);

            var condition = TableQuery.GenerateFilterCondition(column, queryComparisons, value);
            var querys = new TableQuery().Where(condition);
            //List<DynamicTableEntity> lst = cloudTable.ExecuteQuery(querys).ToList();

            TableQuerySegment tableQuerySegment = await cloudTable.ExecuteQuerySegmentedAsync(querys, null);

            IEnumerator<DynamicTableEntity> lista = tableQuerySegment.GetEnumerator();

            List<DynamicTableEntity> lst = new List<DynamicTableEntity>();

            while (lista.MoveNext())
            {
                lst.Add(lista.Current);
            }

            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dynamicTableEntity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T getEntity<T>(DynamicTableEntity dynamicTableEntity, Type type)
        {
            object objEntity = null;

            if (dynamicTableEntity != null)
            {
                Reflections reflections = new Reflections();

                objEntity = reflections.create(type);

                reflections.setPropertyValue(objEntity, "PartitionKey", dynamicTableEntity.PartitionKey);
                reflections.setPropertyValue(objEntity, "RowKey", dynamicTableEntity.RowKey);
                reflections.setPropertyValue(objEntity, "Timestamp", dynamicTableEntity.Timestamp);
                reflections.setPropertyValue(objEntity, "ETag", dynamicTableEntity.ETag);

                foreach (KeyValuePair<string, EntityProperty> row in dynamicTableEntity.Properties)
                {

                    PropertyInfo propertyInfo = reflections.getProperty(objEntity, row.Key);

                    if (reflections.isString(propertyInfo))
                    {
                        reflections.setPropertyValue(objEntity, row.Key, row.Value.StringValue);
                    }

                    if (reflections.isInt64(propertyInfo))
                    {
                        reflections.setPropertyValue(objEntity, row.Key, row.Value.Int64Value);
                    }

                    if (reflections.isInt32(propertyInfo))
                    {
                        Int32? value = null;
                        try
                        {
                            value = row.Value.Int32Value;
                        }
                        catch (Exception e)
                        {
                            value = Convert.ToInt32(row.Value.Int64Value.Value);
                        }

                        reflections.setPropertyValue(objEntity, row.Key, value);
                    }


                }
            }

            return (T)objEntity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listDynamicTableEntity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> getListEntity<T>(List<DynamicTableEntity> listDynamicTableEntity, Type type)
        {
            List<T> listEntity = new List<T>();

            if (listDynamicTableEntity != null && listDynamicTableEntity.Any())
            {

                foreach (DynamicTableEntity dynamicTableEntity in listDynamicTableEntity)
                {
                    listEntity.Add(getEntity<T>(dynamicTableEntity, type));
                }
            }

            return listEntity;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class Logs4J : TableEntity
    {
        public string app { get; set; }
        //public string idUsuario { get; set; }
        public string json { get; set; }
        //public int code { get; set; }
        public string message { get; set; }
        public string estatus { get; set; }

        public Logs4J(string vat)
        {
            this.PartitionKey = vat;
            this.RowKey = Guid.NewGuid().ToString();
        }

        public Logs4J()
        {
            string id = Guid.NewGuid().ToString();
            this.PartitionKey = id;
            this.RowKey = id;
        }

    }

}
