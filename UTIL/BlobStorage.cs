using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NPOI.OpenXml4Net.OPC;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace UTIL
{
    /// <summary>
    /// @author ELH 
    /// </summary>
    public class BlobStorage
    {

        private static string[] mes = { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };

        private static string ConnectionStrings = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        public static void setConnectionStrings(string connectionStrings)
        {
            //BlobStorage.ConnectionStrings = ConfigurationManager.ConnectionStrings["BitacoraStorage"].ConnectionString;
            BlobStorage.ConnectionStrings = connectionStrings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        private static CloudBlobContainer getConnection(string containerName)
        {
            //setParametros();

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(BlobStorage.ConnectionStrings);

            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            return cloudBlobContainer;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public static async Task<string> save(string path, string containerName, string blobName)
        {

            CloudBlobContainer cloudBlobContainer = getConnection(containerName);

            await createContainer(cloudBlobContainer);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            string url = null;

            using (var fileStream = System.IO.File.OpenRead(path))
            {
                await cloudBlockBlob.UploadFromStreamAsync(fileStream);

                url = cloudBlockBlob.Uri.ToString();

                fileStream.Close();
            }

            return url;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public static async Task<string> save(byte[] bytes, string containerName, string blobName)
        {

            CloudBlobContainer cloudBlobContainer = getConnection(containerName);

            await createContainer(cloudBlobContainer);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            string url = null;

            using (MemoryStream stream = new MemoryStream(bytes, writable: false))
            {
                await cloudBlockBlob.UploadFromStreamAsync(stream);
                url = cloudBlockBlob.Uri.ToString();
            }

            return url;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="url"></param>
        //public static async Task download(HttpResponseBase response, string url)
        //{
        //    string[] datos = url.Split('/');

        //    string storage = datos[2].Split('.')[0];

        //    string containerName = datos[3];

        //    string blobName = datos[4];

        //    CloudBlobContainer cloudBlobContainer = getConnection(containerName);

        //    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

        //    byte[] bytes = null;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await cloudBlockBlob.DownloadToStreamAsync(memoryStream);

        //        bytes = memoryStream.ToArray();

        //    }

        //    //byte[] bytes = cloudBlockBlob.DownloadByteArray();

        //    /*      response.AppendHeader("content-disposition", "attachment; filename=" + blobName);
        //          response.ContentType = "application/octet-stream";
        //          response.OutputStream.Write(bytes, 0, bytes.Length);
        //          response.Flush();
        //          response.Close();*/

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string getKey(string url)
        {
            string[] datos = url.Split('/');

            string storage = datos[2].Split('.')[0];

            string containerName = datos[3];

            string blobName = datos[4];

            CloudBlobContainer cloudBlobContainer = getConnection(containerName);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();

            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);

            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(10);

            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            var sasBlobToken = cloudBlockBlob.GetSharedAccessSignature(sasConstraints);

            return sasBlobToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public static async Task SetPermissions(string url)
        {
            try
            {
                string[] datos = url.Split('/');

                string storage = datos[2].Split('.')[0];

                string containerName = datos[3];

                string blobName = datos[4];

                CloudBlobContainer cloudBlobContainer = getConnection(containerName);

                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container }); //Blob

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception -->> " + e.Message);
            }

        }

        public static async Task download(string url, string pathFile)
        {

            string[] datos = url.Split('/');

            string storage = datos[2].Split('.')[0];

            string containerName = datos[3];

            string blobName = datos[4];

            CloudBlobContainer cloudBlobContainer = getConnection(containerName);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            byte[] bytes = null;

            using (var memoryStream = new MemoryStream())
            {
                await cloudBlockBlob.DownloadToStreamAsync(memoryStream);

                bytes = memoryStream.ToArray();

            }

            FileIO.copiar(pathFile, bytes);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        public static async Task createContainerReference(string containerName)
        {
            CloudBlobContainer cloudBlobContainer = getConnection(containerName);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container }); //Blob
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudBlobContainer"></param>
        public static async Task createContainer(CloudBlobContainer cloudBlobContainer)
        {

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                BlobContainerPermissions blobContainerPermissions = await cloudBlobContainer.GetPermissionsAsync();

                blobContainerPermissions.PublicAccess = BlobContainerPublicAccessType.Container;

                await cloudBlobContainer.SetPermissionsAsync(blobContainerPermissions);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="containerName"></param>
        public static async Task delete(string blobName, string containerName)
        {
            CloudBlobContainer cloudBlobContainer = getConnection(containerName);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            await cloudBlockBlob.DeleteAsync();
        }

    }


}
