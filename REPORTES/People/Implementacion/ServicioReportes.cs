using REPORTES.Util;
using System.Collections.Generic;
using System.IO;

namespace REPORTES.People.Implementacion
{
    public class ServicioReportes : IServicioReportes
    {
        private const string TemplateReporte = "{0}{1}";
        private const string ReportPersonas = "ReportPersonas.rdlc";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getPathReportePersonas()
        {
            string tempPath = Path.GetTempPath();
          
            FileInfo fileInfo;
            
            byte[] template;
          
            string templatePath = string.Format(TemplateReporte, tempPath, ReportPersonas);
            
            fileInfo = new FileInfo(templatePath);

            if (!fileInfo.Exists || fileInfo.Length != Resource.ReportPersonas.Length)
            {
                template = Resource.ReportPersonas;
                using (BinaryWriter writer = new BinaryWriter(File.Open(templatePath, FileMode.Create, FileAccess.Write)))
                {
                    writer.Write(template);
                }
            }

            return templatePath;
        }

    }
}
