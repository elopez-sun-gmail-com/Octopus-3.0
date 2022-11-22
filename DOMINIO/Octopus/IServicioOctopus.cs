using MODELO;
using MODELO.Comun.Data;

namespace DOMINIO.Octopus
{
    public interface IServicioOctopus
    {
        Task<JsonResponse> Solicitud(string json);
        Task<JsonResponse> ProcesarSolicitud();
        Task<JsonResponse> ActualizarSolicitud(string key);
        Task<JsonResponse> ObtenerPersonas();
        Task<JsonResponse> COPEC_MANAGE_SAP_GENESIS(Entrada entity);
    }
}
