using ExcelDataReader;
using MODELO;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace UTIL
{
    public class ExcelNPOI
    {
        public enum EXTENSION
        {
            XLSX,
            XLS
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private byte[] WriteExcelWithNPOI(DataTable dt, EXTENSION extension)
        {
            IWorkbook workbook = null;

            if (extension.ToString().ToUpper() == "XLSX")
            {
                workbook = new XSSFWorkbook();
            }

            if (extension.ToString().ToUpper() == "XLS")
            {
                workbook = new HSSFWorkbook();
            }

            if (workbook == null)
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Listado");

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            int contador = 0;

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());

                    if (contador == 0)
                    {
                        sheet1.AutoSizeColumn(j);
                    }

                }

                contador++;
            }

            MemoryStream exportData = null;

            using (exportData = new MemoryStream())
            {
                workbook.Write(exportData);
            }

            return exportData.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public List<Personas> readExcel(MemoryStream stream)
        {
            List<Personas> lista = new List<Personas>();

            try
            {

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                
                var excelReader = ExcelReaderFactory.CreateReader(stream);

                //var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = excelReader.AsDataSet(conf);

                var dataTable = dataSet.Tables[0];

                var dataRowCollection = dataTable.Rows[0][0];

                for (var rows = 0; rows < dataTable.Rows.Count; rows++)
                {
                    Personas dto = new Personas()
                    {
                        idPersona = Convert.ToInt32(dataTable.Rows[rows][0].ToString()),
                        nombre = dataTable.Rows[rows][1].ToString(),
                        apellidoPaterno = dataTable.Rows[rows][2].ToString(),
                        apellidoMaterno = dataTable.Rows[rows][3].ToString(),
                        edad = Convert.ToInt32(dataTable.Rows[rows][4].ToString())
                    };

                    lista.Add(dto);
                }
            }
            catch (Exception e)
            {
                lista = null;
            }

            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listEntity"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public byte[] writeExcel(List<Personas> listEntity, EXTENSION extension)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdPersona", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellido Paterno", typeof(string));
            dt.Columns.Add("Apellido Materno", typeof(string));
            dt.Columns.Add("Edad", typeof(string));

            foreach (Personas entity in listEntity)
            {

                List<object> listRows = new List<object>();

                {
                    object value = entity.idPersona;

                    if (value != null)
                    {
                        listRows.Add(value);
                    }

                }

                {
                    object value = entity.nombre;

                    if (value != null)
                    {
                        listRows.Add(value);
                    }

                }

                {
                    object value = entity.apellidoPaterno;

                    if (value != null)
                    {
                        listRows.Add(value);
                    }

                }

                {
                    object value = entity.apellidoMaterno;

                    if (value != null)
                    {
                        listRows.Add(value);
                    }

                }

                {
                    object value = entity.edad;

                    if (value != null)
                    {
                        listRows.Add(value);
                    }

                }

                dt.Rows.Add(listRows.ToArray());

            }

            byte[] result = WriteExcelWithNPOI(dt, extension);

            return result;
        }



    }
}
