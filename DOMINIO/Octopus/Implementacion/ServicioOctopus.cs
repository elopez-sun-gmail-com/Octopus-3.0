using DATOS.Octopus;
using Microsoft.WindowsAzure.Storage.Table;
using MODELO;
using MODELO.Comun.Data;
using Newtonsoft.Json;
using System.Transactions;
using UTIL;


namespace DOMINIO.Octopus.Implementacion
{
    public class ServicioOctopus : IServicioOctopus
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
        public async Task<JsonResponse> ObtenerPersonas()
        {
            JsonResponse response = new JsonResponse();

            try
            {
                List<Personas> lista = new List<Personas>();

                using (TransactionScope transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var datos = await this.IPersonasDataMapper.ObtenerPersonas();

                    lista = datos.Item1;

                    transaccion.Complete();
                }

                response.code = 200;
                response.model = lista;
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<JsonResponse> COPEC_MANAGE_SAP_GENESIS(Entrada entity)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var datos = await this.IPersonasDataMapper.COPEC_MANAGE_SAP_GENESIS(entity);

                    transaccion.Complete();
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

    }
}