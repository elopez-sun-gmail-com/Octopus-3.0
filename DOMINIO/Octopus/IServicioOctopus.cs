using MODELO;
using MODELO.Comun.Data;

namespace DOMINIO.Octopus
{
    public interface IServicioOctopus
    {
        Task<JsonResponse> Solicitud(string json);
        Task<JsonResponse> ProcesarSolicitud();
        Task<JsonResponse> ActualizarSolicitud(string key);
        Task<List<Personas>> ObtenerPersonas();
        Task<JsonResponse> COPEC_MANAGE_SAP_GENESIS(Entrada entity);
        
        //Task<int?> AgregarPersonas(Personas entity);
        //Task<List<Documentos>> CargarDocumentos(HttpRequest httpRequest);
        //Task<List<string>> Fotografia(List<Documentos> listDocumentos);
        //Task<List<Personas>> ExcelRead(List<Documentos> listDocumentos);
        //Task<string> ExcelWrite(List<Personas> listPersonas);
        //Task<List<Log4J>> ObtenerLog4J();
        //Task<string> Reportes(List<Personas> listPersonas);
    }
}
