using MODELO;

namespace DATOS.Octopus
{
    public interface IOctopusDataMapper : IDisposable
    {
        Task<Tuple<List<Personas>>> ObtenerPersonas();
        Task<Tuple<int>> COPEC_MANAGE_SAP_GENESIS(Entrada entity);
    }
}
