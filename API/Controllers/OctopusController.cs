using DATOS.Octopus.Implementacion;
using DOMINIO.Octopus;
using DOMINIO.Octopus.Implementacion;
using Microsoft.AspNetCore.Mvc;
using MODELO;
using MODELO.Comun.Data;
using Newtonsoft.Json;
using System.Net;
using System.Web.Http.Cors;

namespace API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("externo/api/[controller]")]
    [ApiController]
    public class OctopusController : ControllerBase
    {
        private readonly IServicioOctopus IServicioPersonas;
        public string StorageConnectionStrings = null;

        /// <summary>
        /// 
        /// </summary>
        public OctopusController(IConfiguration configuration)
        {
            string connectionStrings = configuration.GetValue<string>("connectionStrings:AzureConnection");

            this.IServicioPersonas = new ServicioOctopus(new OctopusDataMapper(connectionStrings));

            this.StorageConnectionStrings = configuration.GetValue<string>("connectionStrings:BitacoraStorage");

            ServicioOctopus.StorageConnectionStrings = this.StorageConnectionStrings;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Route("Solicitud")]
        [HttpPost]
        public async Task<JsonResponse> Solicitud(string json)
        {
            HttpRequest httpRequest = HttpContext.Request;

            JsonResponse response = await this.IServicioPersonas.Solicitud(json);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ProcesarSolicitud")]
        [HttpPost]
        public async Task<JsonResponse> ProcesarSolicitud()
        {
            HttpRequest httpRequest = HttpContext.Request;

            JsonResponse response = await this.IServicioPersonas.ProcesarSolicitud();

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("ActualizarSolicitud")]
        [HttpPost]
        public async Task<JsonResponse> ActualizarSolicitud(string key)
        {
            HttpRequest httpRequest = HttpContext.Request;

            JsonResponse response = await this.IServicioPersonas.ActualizarSolicitud(key);

            return response;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ObtenerPersonas")]
        [HttpGet]
        public async Task<JsonResponse> ObtenerPersonas()
        {
            JsonResponse response = new JsonResponse();

            try
            {
                var resultado = await this.IServicioPersonas.ObtenerPersonas();

                response.code = (int)HttpStatusCode.OK;

                response.model = resultado;
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = JsonConvert.SerializeObject(e);
            }

            return response;

        }

        [Route("COPEC_MANAGE_SAP_GENESIS")]
        [HttpPost]
        public async Task<JsonResponse> COPEC_MANAGE_SAP_GENESIS(Entrada entity)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                var resultado = await this.IServicioPersonas.COPEC_MANAGE_SAP_GENESIS(entity);

                response.code = (int)HttpStatusCode.OK;

                response.model = resultado;
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = JsonConvert.SerializeObject(e);
            }

            return response;

        }
       
    }
}
