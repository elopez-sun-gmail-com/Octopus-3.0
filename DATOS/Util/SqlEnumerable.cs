using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DATOS.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class ComunDatos
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public static DataTable getSqlEnumerable(int[] primaryKeys)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("valor", typeof(int));

            if (primaryKeys != null && primaryKeys.Any())
            {
                foreach (int value in primaryKeys)
                {
                    dataTable.Rows.Add(value);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public static DataTable getSqlEnumerable(string[] primaryKeys)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("valor", typeof(string));

            if (primaryKeys != null && primaryKeys.Any())
            {
                foreach (string value in primaryKeys)
                {
                    dataTable.Rows.Add(value);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public static DataTable getSqlEnumerable(byte[] primaryKeys)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("valor", typeof(byte));

            if (primaryKeys != null && primaryKeys.Any())
            {
                foreach (byte value in primaryKeys)
                {
                    dataTable.Rows.Add(value);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public static DataTable getSqlEnumerable(decimal[] primaryKeys)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("valor", typeof(decimal));

            if (primaryKeys != null && primaryKeys.Any())
            {
                foreach (decimal value in primaryKeys)
                {
                    dataTable.Rows.Add(value);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //private static object getValue(Type type)
        //{
        //    object value = null;

        //    if (type != null)
        //    {
        //        if (type == typeof(string))
        //        {
        //            value = "";
        //        }
        //        else
        //        {
        //            if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64) || type == typeof(Double))
        //            {
        //                value = 0;
        //            }
        //            else
        //            {
        //                if (type == typeof(Boolean))
        //                {
        //                    value = false;
        //                }
        //                else
        //                {
        //                    if (type == typeof(DateTime))
        //                    {
        //                        value = DBNull.Value;
        //                    }
        //                    else
        //                    {
        //                        Type answer = Nullable.GetUnderlyingType(type);

        //                        value = ComunDatos.getValue(answer);
        //                    }

        //                }

        //            }
        //        }
        //    }

        //    return value;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public static DataTable getSqlEnumerable<T>(List<T> list, List<string> propertys)
        {
            List<InfoDataTable<object>> listInfoDataTable = new List<InfoDataTable<object>>();

            List<object[]> dataTable = new List<object[]>();

            if (list != null && list.Any())
            {
                int cont = 0;

                foreach (T dto in list)
                {
                    List<object> values = new List<object>();

                    if (propertys != null && propertys.Any())
                    {
                        UTIL.Reflections reflections = new UTIL.Reflections();

                        foreach (string property in propertys)
                        {
                            PropertyInfo[] propertyInfo = reflections.getPropertys(dto);

                            PropertyInfo obj = propertyInfo.Where(row => row.Name == property).ToList().FirstOrDefault();

                            Type type = obj.PropertyType;

                            Type answer = Nullable.GetUnderlyingType(type);

                            if (answer != null)
                            {
                                type = answer;
                            }

                            object value = reflections.getPropertyValue(dto, property);

                            if (value == null)
                            {
                                value = DBNull.Value;  //ComunDatos.getValue(type);
                                values.Add(value);
                            }
                            else
                            {
                                values.Add(value);
                            }

                            if (cont == 0)
                            {
                                listInfoDataTable.Add(new InfoDataTable<object>() { property = property, type = type, value = value });
                            }

                        }

                        cont++;
                    }

                    dataTable.Add(values.ToArray());
                }
            }

            return ComunDatos.getSqlEnumerable(dataTable.ToArray(), listInfoDataTable);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public static List<object[]> getSqlEnumerableValues<T>(List<T> list, List<string> propertys)
        {
            List<InfoDataTable<object>> listInfoDataTable = new List<InfoDataTable<object>>();

            List<object[]> dataTable = new List<object[]>();

            if (list != null && list.Any())
            {
                int cont = 0;

                foreach (T dto in list)
                {
                    List<object> values = new List<object>();

                    if (propertys != null && propertys.Any())
                    {
                        UTIL.Reflections reflections = new UTIL.Reflections();

                        foreach (string property in propertys)
                        {
                            PropertyInfo[] propertyInfo = reflections.getPropertys(dto);

                            PropertyInfo obj = propertyInfo.Where(row => row.Name == property).ToList().FirstOrDefault();

                            Type type = obj.PropertyType;

                            Type answer = Nullable.GetUnderlyingType(type);

                            if (answer != null)
                            {
                                type = answer;
                            }

                            object value = reflections.getPropertyValue(dto, property);

                            if (value == null)
                            {
                                value = DBNull.Value;  //ComunDatos.getValue(type);
                                values.Add(value);
                            }
                            else
                            {
                                values.Add(value);
                            }

                            if (cont == 0)
                            {
                                listInfoDataTable.Add(new InfoDataTable<object>() { property = property, type = type, value = value });
                            }

                        }

                        cont++;
                    }

                    dataTable.Add(values.ToArray());
                }
            }

            return dataTable;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        //public static DataTable getSqlEnumerable(object[][] table)
        //{
        //    DataTable dataTable = new DataTable();

        //    if (table != null && table.Length > 0)
        //    {

        //        object[] row = table[0];

        //        for (int x = 0; x < row.Length; x++)
        //        {
        //            var columna = row[x];

        //            if (columna.GetType() == typeof(string))
        //            {
        //                dataTable.Columns.Add(x + "", typeof(string));
        //            }

        //            if (columna.GetType() == typeof(System.Int16) || columna.GetType() == typeof(System.Int32) || columna.GetType() == typeof(System.Int64))
        //            {
        //                dataTable.Columns.Add(x + "", typeof(int));
        //            }

        //            if (columna.GetType() == typeof(System.Double))
        //            {
        //                dataTable.Columns.Add(x + "", typeof(double));
        //            }

        //            if (columna.GetType() == typeof(System.DateTime))
        //            {
        //                dataTable.Columns.Add(x + "", typeof(DateTime));
        //            }

        //            if (columna.GetType() == typeof(System.Boolean))
        //            {
        //                dataTable.Columns.Add(x + "", typeof(bool));
        //            }

        //            if (columna.GetType() == typeof(DBNull))
        //            {
        //                dataTable.Columns.Add(x + "", typeof(object));
        //            }

        //        }

        //        foreach (object[] value in table)
        //        {
        //            DataRow filas = dataTable.Rows.Add();

        //            filas.ItemArray = value;

        //        }

        //    }

        //    return dataTable;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="listInfoDataTable"></param>
        /// <returns></returns>
        public static DataTable getSqlEnumerable(object[][] table, List<InfoDataTable<object>> listInfoDataTable)
        {
            DataTable dataTable = new DataTable();

            if (table != null && table.Length > 0)
            {

                if (listInfoDataTable != null && listInfoDataTable.Any())
                {
                    int x = 0;

                    foreach (InfoDataTable<object> _row in listInfoDataTable)
                    {
                        dataTable.Columns.Add(string.Format("{0}_{1}", x, _row.property), _row.type);

                        x++;
                    }

                }

                foreach (object[] value in table)
                {
                    DataRow filas = dataTable.Rows.Add();

                    filas.ItemArray = value;

                }

            }

            return dataTable;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InfoDataTable<T>
    {
        public string property { get; set; }
        public Type type { get; set; }
        public T value { get; set; }
    }

}
