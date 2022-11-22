using System;

namespace MODELO.Comun.Data
{
    public class Log4J
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string app { get; set; }
        //public string idUsuario { get; set; }
        public string json { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string estatus { get; set; }
       
    }
}
