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
            JsonResponse response = await this.IServicioPersonas.ObtenerPersonas();
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Route("COPEC_MANAGE_SAP_GENESIS")]
        [HttpPost]
        public async Task<JsonResponse> COPEC_MANAGE_SAP_GENESIS(Entrada entity)
        {
            JsonResponse response = await this.IServicioPersonas.COPEC_MANAGE_SAP_GENESIS(entity);
            return response;
        }
       
    }
}
