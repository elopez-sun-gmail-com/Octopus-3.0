using Microsoft.Reporting.WebForms;

namespace REPORTES.Util
{
    public class IReports
    {
        public enum EXTENSION
        {
            PDF
          , DOC
          , DOCX
          , XLS
          , XLSX
          , JPG
        }

        public enum REPORT_TYPE
        {
            PDF
            , WORD_97_2003
            , WORD
            , EXCEL_97_2003
            , EXCEL
            , IMAGE
        }

        public enum REPORT_TYPE_OTHER
        {
            WORD
           , WORDOPENXML
           , EXCEL
           , EXCELOPENXML
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSources"></param>
        /// <param name="parameters"></param>
        /// <param name="fileRdlc"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public static byte[] create(Dictionary<string, object> dataSources, Dictionary<string, object> parameters, string fileRdlc, IReports.REPORT_TYPE reportType)
        {
            byte[] bytes = null;

            using (var reportViewer = new LocalReport())
            {

                reportViewer.ReportPath = fileRdlc;

                reportViewer.EnableHyperlinks = true;

                if (parameters != null && parameters.Any())
                {
                    List<ReportParameter> listReportParameter = new List<ReportParameter>();

                    foreach (string key in parameters.Keys)
                    {
                        listReportParameter.Add(new ReportParameter(key, parameters[key].ToString()));
                    }

                    reportViewer.SetParameters(listReportParameter.ToArray());
                }

                ReportDataSource reportDataSource = null;

                if (dataSources != null)
                {
                    foreach (string key in dataSources.Keys)
                    {
                        reportDataSource = new ReportDataSource
                        {
                            Name = key,
                            Value = dataSources[key]
                        };

                        reportViewer.DataSources.Add(reportDataSource);
                    }

                }

                string _reportType = null;

                switch (reportType)
                {
                    case REPORT_TYPE.PDF:
                        _reportType = REPORT_TYPE.PDF.ToString();
                        break;
                    case REPORT_TYPE.WORD_97_2003:
                        _reportType = REPORT_TYPE_OTHER.WORD.ToString();
                        break;
                    case REPORT_TYPE.WORD:
                        _reportType = REPORT_TYPE_OTHER.WORDOPENXML.ToString();
                        break;
                    case REPORT_TYPE.EXCEL_97_2003:
                        _reportType = REPORT_TYPE_OTHER.EXCEL.ToString();
                        break;
                    case REPORT_TYPE.EXCEL:
                        _reportType = REPORT_TYPE_OTHER.EXCELOPENXML.ToString();
                        break;
                    case REPORT_TYPE.IMAGE:
                        _reportType = REPORT_TYPE.IMAGE.ToString();
                        break;
                }

                bytes = reportViewer.Render(_reportType.ToString());

            }

            return bytes;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSources"></param>
        /// <param name="parameters"></param>
        /// <param name="UrlRdlc"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public static byte[] create(Dictionary<string, object> dataSources, Dictionary<string, object> parameters, Uri UrlRdlc, IReports.REPORT_TYPE reportType)
        {
            byte[] bytes = null;

            using (var reportViewer = new LocalReport())
            {

                byte[] byteArray = new System.Net.WebClient().DownloadData(UrlRdlc.AbsoluteUri);

                reportViewer.LoadReportDefinition(new MemoryStream(byteArray));

                if (parameters != null && parameters.Any())
                {
                    List<ReportParameter> listReportParameter = new List<ReportParameter>();

                    foreach (string key in parameters.Keys)
                    {
                        listReportParameter.Add(new ReportParameter(key, parameters[key].ToString()));
                    }

                    reportViewer.SetParameters(listReportParameter.ToArray());
                }

                ReportDataSource reportDataSource = null;

                if (dataSources != null)
                {
                    foreach (string key in dataSources.Keys)
                    {
                        reportDataSource = new ReportDataSource
                        {
                            Name = key,
                            Value = dataSources[key]
                        };

                        reportViewer.DataSources.Add(reportDataSource);
                    }

                }

                string _reportType = null;

                switch (reportType)
                {
                    case REPORT_TYPE.PDF:
                        _reportType = REPORT_TYPE.PDF.ToString();
                        break;
                    case REPORT_TYPE.WORD_97_2003:
                        _reportType = REPORT_TYPE_OTHER.WORD.ToString();
                        break;
                    case REPORT_TYPE.WORD:
                        _reportType = REPORT_TYPE_OTHER.WORDOPENXML.ToString();
                        break;
                    case REPORT_TYPE.EXCEL_97_2003:
                        _reportType = REPORT_TYPE_OTHER.EXCEL.ToString();
                        break;
                    case REPORT_TYPE.EXCEL:
                        _reportType = REPORT_TYPE_OTHER.EXCELOPENXML.ToString();
                        break;
                    case REPORT_TYPE.IMAGE:
                        _reportType = REPORT_TYPE.IMAGE.ToString();
                        break;
                }

                bytes = reportViewer.Render(_reportType.ToString());

            }

            return bytes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSources"></param>
        /// <param name="parameters"></param>
        /// <param name="byteArray"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public static byte[] create(Dictionary<string, object> dataSources, Dictionary<string, object> parameters, byte[] byteArray, IReports.REPORT_TYPE reportType)
        {
            byte[] bytes = null;

            using (var reportViewer = new LocalReport())
            {

                //byte[] byteArray = new System.Net.WebClient().DownloadData(UrlRdlc.AbsoluteUri);

                reportViewer.LoadReportDefinition(new MemoryStream(byteArray));

                if (parameters != null && parameters.Any())
                {
                    List<ReportParameter> listReportParameter = new List<ReportParameter>();

                    foreach (string key in parameters.Keys)
                    {
                        listReportParameter.Add(new ReportParameter(key, parameters[key].ToString()));
                    }

                    //reportViewer.SetParameters(listReportParameter.ToArray());
                }

                ReportDataSource reportDataSource = null;

                if (dataSources != null)
                {
                    foreach (string key in dataSources.Keys)
                    {
                        reportDataSource = new ReportDataSource
                        {
                            Name = key,
                            Value = dataSources[key]
                        };

                        reportViewer.DataSources.Add(reportDataSource);
                    }

                }

                string _reportType = null;

                switch (reportType)
                {
                    case REPORT_TYPE.PDF:
                        _reportType = REPORT_TYPE.PDF.ToString();
                        break;
                    case REPORT_TYPE.WORD_97_2003:
                        _reportType = REPORT_TYPE_OTHER.WORD.ToString();
                        break;
                    case REPORT_TYPE.WORD:
                        _reportType = REPORT_TYPE_OTHER.WORDOPENXML.ToString();
                        break;
                    case REPORT_TYPE.EXCEL_97_2003:
                        _reportType = REPORT_TYPE_OTHER.EXCEL.ToString();
                        break;
                    case REPORT_TYPE.EXCEL:
                        _reportType = REPORT_TYPE_OTHER.EXCELOPENXML.ToString();
                        break;
                    case REPORT_TYPE.IMAGE:
                        _reportType = REPORT_TYPE.IMAGE.ToString();
                        break;
                }

                bytes = reportViewer.Render(_reportType.ToString());

            }

            return bytes;

        }
        //public void example()
        //{
        //    Dictionary<string, object> parameters = new Dictionary<string, object>();

        //    parameters.Add("TOTAl_SALARIO", "35000");

        //    {

        //        string documentName = string.Format("ReporteUsuarios_dos.{0}", IReports.EXTENSION.PDF);

        //        byte[] bytes = IReports.create<RUTA.MODELO.Excel.Data.DatosPersonales>("ListDatosPersonales", listaDatosPersonales, parameters, "DatosPersonales", IReports.REPORT_TYPE.EXCEL);

        //        File.WriteAllBytes(string.Format(@"C:\Desarrollo\{0}", documentName), bytes);

        //    }
        //}

    }
}
