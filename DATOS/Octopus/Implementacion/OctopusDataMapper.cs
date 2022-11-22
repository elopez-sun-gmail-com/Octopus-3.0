using Dapper;
using DATOS.Util;
using MODELO;
using System.Data;

namespace DATOS.Octopus.Implementacion
{
    public class OctopusDataMapper : SqlDataMapperBase, IOctopusDataMapper
    {
        private IDbConnection connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        public OctopusDataMapper(string connectionStrings) : base(connectionStrings)
        {
            this.connection = this.getConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<List<Personas>>> ObtenerPersonas()
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@tamanioPagina", 50);
            parametros.Add("@pagina", 1);

            var lector = await this.connection.QueryMultipleAsync("[exruv].[ObtenerPersonas]", parametros, commandType: CommandType.StoredProcedure);

            var rows = await lector.ReadAsync<int>();

            var listPersonas = await lector.ReadAsync<Personas>();

            return Tuple.Create(listPersonas.ToList());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Tuple<int>> COPEC_MANAGE_SAP_GENESIS(Entrada entity)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@numFolioTrans", entity.numFolioTrans);
            parametros.Add("@pais", entity.pais);
            parametros.Add("@indEliminacion", entity.indEliminacion);
            parametros.Add("@indModificacion", entity.indModificacion);
            parametros.Add("@codSociedad", entity.codSociedad);
            parametros.Add("@idLoad", entity.idLoad);
            parametros.Add("@ordCargue", entity.ordCargue);
            parametros.Add("@secuenciaViaje", entity.secuenciaViaje);
            parametros.Add("@idCarrierSAP", entity.idCarrierSAP);
            parametros.Add("@idExpedicionSAP", entity.idExpedicionSAP);
            parametros.Add("@denClaseExpedicion", entity.denClaseExpedicion);
            parametros.Add("@numEtapaTransp", entity.numEtapaTransp);
            parametros.Add("@idOrigenSAP", entity.idOrigenSAP);
            parametros.Add("@idDestinoSAP", entity.idDestinoSAP);
            parametros.Add("@idClaseSAP", entity.idClaseSAP);
            parametros.Add("@idRutaSAP", entity.idRutaSAP);
            parametros.Add("@fechaCarga", entity.fechaCarga);
            parametros.Add("@horaCitaCarga", entity.horaCitaCarga);
            parametros.Add("@fechaDescarga", entity.fechaDescarga);
            parametros.Add("@horaCitaDescarga", entity.horaCitaDescarga);
            parametros.Add("@signatura", entity.signatura);
            parametros.Add("@idViajePeru", entity.idViajePeru);
            parametros.Add("@suplemento1Canc", entity.suplemento1Canc);
            parametros.Add("@denSuplemento1Canc", entity.denSuplemento1Canc);
            parametros.Add("@suplemento2", entity.suplemento2);
            parametros.Add("@denSuplemento2", entity.denSuplemento2);
            parametros.Add("@suplemento3", entity.suplemento3);
            parametros.Add("@denSuplemento3", entity.denSuplemento3);
            parametros.Add("@suplemento4", entity.suplemento4);
            parametros.Add("@denSuplemento4", entity.denSuplemento4);
            parametros.Add("@estatusGlobal", entity.estatusGlobal);
            parametros.Add("@denEstatusGlobal", entity.denEstatusGlobal);
            parametros.Add("@denRutas", entity.denRutas);
            parametros.Add("@nombTransportista", entity.nombTransportista);
            parametros.Add("@placaVehiculo", entity.placaVehiculo);
            parametros.Add("@conductorVehiculo", entity.conductorVehiculo);
            parametros.Add("@codigoConductor1", entity.codigoConductor1);
            parametros.Add("@codigoConductor2", entity.codigoConductor2);
            parametros.Add("@identifConductor", entity.identifConductor);
            parametros.Add("@tractor", entity.tractor);
            parametros.Add("@remolque1", entity.remolque1);
            parametros.Add("@remolque2", entity.remolque2);
            parametros.Add("@idTracking", entity.idTracking);
            parametros.Add("@entregaSaliente", entity.entregaSaliente);
            parametros.Add("@origPedidoSAP", entity.origPedidoSAP);
            parametros.Add("@shipment", entity.shipment);
            parametros.Add("@numNecesidad", entity.numNecesidad);
            parametros.Add("@destPedidoSAP", entity.destPedidoSAP);
            parametros.Add("@stEntregaSal", entity.stEntregaSal);
            parametros.Add("@stEntregaEnt", entity.stEntregaEnt);
            parametros.Add("@stGralEntregaCarga", entity.stGralEntregaCarga);
            parametros.Add("@stGralEntregaDescarga", entity.stGralEntregaDescarga);
            parametros.Add("@movSMSalida", entity.movSMSalida);
            parametros.Add("@movSMEntrada", entity.movSMEntrada);
            parametros.Add("@indTipoTransporte", entity.indTipoTransporte);
            parametros.Add("@numFolioTranspLig", entity.numFolioTranspLig);
            parametros.Add("@posicionEntrega", entity.posicionEntrega);
            parametros.Add("@numSKU", entity.numSKU);
            parametros.Add("@descripcionProducto", entity.descripcionProducto);
            parametros.Add("@ctroLogisticoEntrega", entity.ctroLogisticoEntrega);
            parametros.Add("@almacenEntrega", entity.almacenEntrega);
            parametros.Add("@palletsPlanificados", entity.palletsPlanificados);
            parametros.Add("@cantSKU", entity.cantSKU);
            parametros.Add("@uniMedidaSKU", entity.uniMedidaSKU);
            parametros.Add("@pedido", entity.pedido);
            parametros.Add("@pesoBruto", entity.pesoBruto);
            parametros.Add("@uniMedidaPesoBruto", entity.uniMedidaPesoBruto);
            parametros.Add("@pesoNeto", entity.pesoNeto);
            parametros.Add("@uniMedidaPesoNeto", entity.uniMedidaPesoNeto);
            parametros.Add("@volumen", entity.volumen);
            parametros.Add("@uniMedidaVolumen", entity.uniMedidaVolumen);
            parametros.Add("@cantBultos", entity.cantBultos);
            parametros.Add("@fechaSalida", entity.fechaSalida);
            parametros.Add("@indSE", entity.indSE);
            parametros.Add("@fechaContPlantSal", entity.fechaContPlantSal);
            parametros.Add("@horaContPlantSal", entity.horaContPlantSal);
            parametros.Add("@fechaContAgciaEnt", entity.fechaContAgciaEnt);
            parametros.Add("@horaContAgciaEnt", entity.horaContAgciaEnt);
            parametros.Add("@fechaUltimaModif", entity.fechaUltimaModif);
            parametros.Add("@horaUltimaModif", entity.horaUltimaModif);
            parametros.Add("@fechaSalidaPlanta", entity.fechaSalidaPlanta);
            parametros.Add("@horaSalidaPlanta", entity.horaSalidaPlanta);
            parametros.Add("@distanciaRuta", entity.distanciaRuta);
            parametros.Add("@uniMedidaDistRuta", entity.uniMedidaDistRuta);
            parametros.Add("@costoTransporte", entity.costoTransporte);
            parametros.Add("@monedaCostoTransp", entity.monedaCostoTransp);
            parametros.Add("@fechaContSal", entity.fechaContSal);
            parametros.Add("@fechaContEnt", entity.fechaContEnt);
            parametros.Add("@numTotalEntregas", entity.numTotalEntregas);
            parametros.Add("@fechaGuiaRemision", entity.fechaGuiaRemision);
            parametros.Add("@guiaRemision", entity.guiaRemision);


            var lector = await this.connection.QueryMultipleAsync("[exruv].[AgregarPersonas]", parametros, commandType: CommandType.StoredProcedure);

            var lista = await lector.ReadAsync<int>();

            int primaryKey = lista.FirstOrDefault();

            return Tuple.Create(primaryKey);
        }
       

    }
}
