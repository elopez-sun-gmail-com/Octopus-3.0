using MODELO;

namespace DATOS.Octopus
{
    public interface IOctopusDataMapper : IDisposable
    {
        Task<Tuple<List<Personas>>> ObtenerPersonas();
        Task<Tuple<int>> AgregarPersonas(Personas entity);
        Task<Tuple<int>> COPEC_MANAGE_SAP_GENESIS(Entrada entity);
    }
}
