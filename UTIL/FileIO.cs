using System;
using System.IO;
using System.Net;
using System.Web;

namespace UTIL
{
    public class FileIO
    {
        public static string basePath = null;

        public static void copiar(string origen, string destino)
        {
            string[] aux = origen.Split('\\');

            destino += "\\" + aux[aux.Length - 1];

            File.Copy(origen, destino, true);
        }

        public static void copiar(string origen, string destino, string nombreArchivo)
        {
            destino += "\\" + nombreArchivo;

            File.Copy(origen, destino, true);
        }

        public static void copiar(string filePath, byte[] bytes)
        {

            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }

            FileStream fileStream = File.Create(filePath);

            for (int i = 0; i < bytes.Length; i++)
            {
                fileStream.WriteByte(bytes[i]);
            }

            fileStream.Close();

        }

        public static void mover(string origen, string destino)
        {
            string[] aux = origen.Split('\\');

            destino += "\\" + aux[aux.Length - 1];

            if (existeFile(destino))
            {
                borrar(destino);
            }

            File.Move(origen, destino);
        }

        public static void mover(string origen, string destino, string nombreArchivo)
        {

            destino += "\\" + nombreArchivo;

            if (existeFile(destino))
            {
                borrar(destino);
            }

            File.Move(origen, destino);

        }

        public static void borrar(string filePath)
        {
            File.Delete(filePath);
        }

        public static Boolean existeFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public static Boolean existeDirectory(string filePath)
        {
            return Directory.Exists(filePath);
        }

        public static byte[] getBytesFile(string filePath)
        {

            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fileStream = File.OpenRead(filePath);

            try
            {

                byte[] bytes = new byte[fileStream.Length];

                fileStream.Read(bytes, 0, System.Convert.ToInt32(fileStream.Length));

                fileStream.Close();

                return bytes;

            }

            finally
            {

                fileStream.Close();

            }

        }

        public static byte[] getBytesURL(string url)
        {

            byte[] bytes = null;

            try
            {
                WebClient webClient = new WebClient();
                bytes = webClient.DownloadData(url);
            }

            catch (Exception e)
            {
                System.Console.WriteLine("Exception --> {0} ", e.Message);
            }

            return bytes;

        }

        //public static void downloadFile(string filePath, HttpResponse response)
        //{
        //    try
        //    {

        //        /*  string FileName = Path.GetFileName(filePath);
        //          response.Clear();
        //          response.ContentType = "application/octet-stream";
        //          response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
        //          response.WriteFile(filePath);
        //          response.Flush();
        //          response.End();
        //          */

        //        System.IO.FileInfo toDownload = new System.IO.FileInfo(filePath);

        //        if (toDownload.Exists)
        //        {
        //            HttpContext.Current.Response.Clear();
        //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
        //            HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
        //            HttpContext.Current.Response.ContentType = "application/octet-stream";
        //            HttpContext.Current.Response.WriteFile(filePath);
        //            HttpContext.Current.Response.End();
        //        }


        //    }
        //    catch (System.Exception e)
        //    {
        //        System.Console.WriteLine("Exception --> {0} ", e.Message);
        //    }
        //}


        //public static bool downloadUrl(string url, HttpResponseBase response)
        //{
        //    bool resultado = false;
        //    try
        //    {
        //        byte[] buffer = getBytesURL(url);

        //        if (buffer != null)
        //        {
        //            resultado = true;

        //            string[] datos = url.Split('/');

        //            string FileName = datos[4];

        //            response.Clear();
        //            response.ContentType = "application/octet-stream";
        //            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
        //            response.OutputStream.Write(buffer, 0, buffer.Length);
        //            response.Flush();
        //            response.End();

        //        }


        //    }
        //    catch (System.Exception e)
        //    {
        //        System.Console.WriteLine("Exception --> {0} ", e.Message);
        //    }

        //    return resultado;
        //}

        public static void crearDirectorio(string path)
        {
            Directory.CreateDirectory(path);
        }

        //public static string getBasePath()
        //{
        //    string strAppDir = HttpContext.Current.Server.MapPath("./");

        //    if (basePath == null)
        //    {
        //        basePath = strAppDir;
        //    }

        //    return strAppDir;

        //}
    }
}
