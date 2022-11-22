using DATOS.Octopus;
//using REPORTES.Util;
//using static REPORTES.Util.IReports;
//using REPORTES.People.Implementacion;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using MODELO;
using MODELO.Comun.Data;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using System.Transactions;
using UTIL;


namespace DOMINIO.Octopus.Implementacion
{
    public  class ServicioOctopus : IServicioOctopus
    {
        private readonly IOctopusDataMapper IPersonasDataMapper;

        public static string StorageConnectionStrings = null;

        public ServicioOctopus()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IPersonasDataMapper"></param>
        public ServicioOctopus(IOctopusDataMapper IPersonasDataMapper)
        {
            this.IPersonasDataMapper = IPersonasDataMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Solicitud(string json)
        {
            JsonResponse response = new JsonResponse();

            try
            {

                Logs4J table = new Logs4J()
                {
                    app = "LOG",
                    json = JsonConvert.SerializeObject(json),
                    estatus = "0"
                };

                TableStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

                await TableStorage.Insert("Log4J", table);

                response.code = 200;
                response.model = true;
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = e.Message;
            }

            return response;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public async Task<JsonResponse> ProcesarSolicitud()
        {
            JsonResponse response = new JsonResponse();

            try
            {
                List<Logs4J> listLog4J = new List<Logs4J>();

                TableStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

                List<DynamicTableEntity> listDynamicTableEntity = await TableStorage.query("Log4J", "estatus", QueryComparisons.Equal, "0");

                if (listDynamicTableEntity != null && listDynamicTableEntity.Any())
                {
                    listLog4J = TableStorage.getListEntity<Logs4J>(listDynamicTableEntity, typeof(Logs4J));
                }

                response.code = 200;
                response.model = listLog4J;

            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = e.Message;
            }

            return response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<JsonResponse> ActualizarSolicitud(string key)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                // Log4J entity = null;

                TableStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

                List<DynamicTableEntity> listDynamicTableEntity = await TableStorage.query("Log4J", "PartitionKey", QueryComparisons.Equal, key);

                if (listDynamicTableEntity != null && listDynamicTableEntity.Any())
                {
                    List<Logs4J> listLog4J = TableStorage.getListEntity<Logs4J>(listDynamicTableEntity, typeof(Logs4J));

                    Logs4J entity = listLog4J.FirstOrDefault();

                    entity.estatus = "1";

                    await TableStorage.Insert("Log4J", entity);

                }

                response.code = 200;
                response.model = true;

            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = e.Message;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Personas>> ObtenerPersonas()
        {
            var datos = await this.IPersonasDataMapper.ObtenerPersonas();

            List<Personas> lista = datos.Item1;

            return lista;
        }

        public async Task<JsonResponse> COPEC_MANAGE_SAP_GENESIS(Entrada entity)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                var datos = await this.IPersonasDataMapper.COPEC_MANAGE_SAP_GENESIS(entity);

                response.code = 200;
                response.model = true;
            }
            catch(Exception e)
            {
                response.code = 500;
                response.message = e.Message;
            }

            return response;

        }






















        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="httpRequest"></param>
        ///// <returns></returns>
        //public async Task<List<Documentos>> CargarDocumentos(HttpRequest httpRequest)
        //{
        //    List<Documentos> listDocumentos = new List<Documentos>();

        //    IFormCollection forma = httpRequest.Form;

        //    //Iterar parametros Adicionales a los archivos

        //    Dictionary<string, object> datos = new Dictionary<string, object>();

        //    foreach (string key in forma.Keys)
        //    {
        //        datos.Add(key, forma[key]);
        //    }

        //    //Iterar archivos

        //    if (httpRequest.Form.Files.Count > 0)
        //    {
        //        foreach (IFormFile item in httpRequest.Form.Files)
        //        {
        //            IFormFile postedFile = item;

        //            byte[] arrBytes = null;

        //            using (var binaryReader = new BinaryReader(postedFile.OpenReadStream()))
        //            {
        //                arrBytes = binaryReader.ReadBytes(Convert.ToInt16(postedFile.Length));

        //                listDocumentos.Add(new Documentos() { datos = datos, arrayByte = arrBytes, fileName = postedFile.FileName });

        //            }
        //        }
        //    }

        //    return listDocumentos;
        //}



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public async Task<int?> AgregarPersonas(Personas entity)
        //{
        //    int? primaryKey = null;

        //    using (TransactionScope transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        await this.RegistrarLog(entity, null);

        //        var datos = await this.IPersonasDataMapper.AgregarPersonas(entity);

        //        primaryKey = datos.Item1;

        //        transaccion.Complete();
        //    }
        //    return primaryKey;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="connectionStrings"></param>
        ///// <param name="listDocumentos"></param>
        ///// <returns></returns>
        //public async Task<List<string>> Fotografia(List<Documentos> listDocumentos)
        //{
        //    List<string> urls = new List<string>();

        //    if (listDocumentos != null && listDocumentos.Any())
        //    {
        //        foreach (Documentos row in listDocumentos)
        //        {
        //            BlobStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

        //            string url = await BlobStorage.save(row.arrayByte, "pfotos", row.fileName);

        //            urls.Add(url);
        //        }
        //    }

        //    return urls;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="listDocumentos"></param>
        ///// <returns></returns>
        //public async Task<List<Personas>> ExcelRead(List<Documentos> listDocumentos)
        //{
        //    List<Personas> listPersonas = new List<Personas>();

        //    if (listDocumentos != null && listDocumentos.Any())
        //    {
        //        foreach (Documentos row in listDocumentos)
        //        {
        //            ExcelNPOI poi = new ExcelNPOI();

        //            listPersonas = poi.readExcel(new MemoryStream(row.arrayByte));

        //        }
        //    }

        //    return listPersonas;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="connectionStrings"></param>
        ///// <param name="listPersonas"></param>
        ///// <returns></returns>
        //public async Task<string> ExcelWrite(List<Personas> listPersonas)
        //{
        //    string url = null;

        //    if (listPersonas != null && listPersonas.Any())
        //    {
        //        ExcelNPOI poi = new ExcelNPOI();

        //        byte[] arrayByte = poi.writeExcel(listPersonas, ExcelNPOI.EXTENSION.XLSX);

        //        BlobStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

        //        url = await BlobStorage.save(arrayByte, "pfotos", "Excel_(1).xlsx");

        //    }

        //    return url;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listPersonas"></param>
        /// <returns></returns>
        //public async Task<string> Reportes(List<Personas> listPersonas)
        //{
        //    string url = null;

        //    if (listPersonas != null)
        //    {

        //        Dictionary<string, object> dataSources = new Dictionary<string, object>();
        //        dataSources.Add("ListaPersonas", listPersonas);

        //        Dictionary<string, object> parameters = new Dictionary<string, object>();
        //        parameters.Add("Parametro01", DateTime.Now.ToString());

        //        {
        //            ServicioReportes servicioReportes = new ServicioReportes();

        //            string path = servicioReportes.getPathReportePersonas(); //@"D:\Desarrollo\Workspace\Net\Visual Studio\2022\API\REPORTES\Template\ReportPersonas.rdlc"

        //            string documentName = string.Format("ReportePersonas.{0}", IReports.EXTENSION.PDF);

        //            byte[] arrayByte = IReports.create(dataSources, parameters, path, IReports.REPORT_TYPE.PDF);

        //            url = await BlobStorage.save(arrayByte, "pfotos", documentName);
        //        }
        //    }

        //    return url;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public async Task<List<Log4J>> ObtenerLog4J()
        //{
        //    List<Log4J> listLog4J = new List<Log4J>();

        //    TableStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

        //    List<DynamicTableEntity> listDynamicTableEntity = await TableStorage.query("Log4J");

        //    List<DynamicTableEntity> listDynamicTableEntity2 = await TableStorage.query("Log4J", "idUsuario", QueryComparisons.Equal, "98");

        //    if (listDynamicTableEntity != null && listDynamicTableEntity.Any())
        //    {
        //        listLog4J = TableStorage.getListEntity<Log4J>(listDynamicTableEntity, typeof(Log4J));

        //        /*foreach (DynamicTableEntity row in listDynamicTableEntity)
        //        {
        //            Log4J log4J = TableStorage.getEntity<Log4J>(row, typeof(Log4J));
        //            listLog4J.Add(log4J);
        //        }*/
        //    }

        //    return listLog4J;
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        //private async Task RegistrarLog(object obj, string message = null)
        //{
        //    Logs4J table = new Logs4J()
        //    {
        //        app = "LOG",
        //        json = JsonConvert.SerializeObject(obj),
        //        message = message == null ? "" : message,
        //        estatus = "0"
        //    };

        //    //string connectionString = ConfigurationManager.ConnectionStrings["BitacoraStorage"].ConnectionString;

        //    TableStorage.setConnectionStrings(ServicioOctopus.StorageConnectionStrings);

        //    await TableStorage.Insert("Log4J", table);
        //}



    }
}