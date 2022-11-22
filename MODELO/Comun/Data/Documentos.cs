using System.Collections.Generic;

namespace MODELO.Comun.Data
{
    public class Documentos
    {
        public Dictionary<string, object> datos { get; set; }
        public string fileName { get; set; }
        public byte[] arrayByte { get; set; }
    }
}
