using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UTIL
{
    /// <summary>
    /// @elopez
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CallService<T>
    {
        public CallService()
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> getEntity(string url)
        {
            object entity = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    entity = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseData);

                }

            }


            return (T)entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T> getEntity(string url, Dictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                url = string.Format("{0}{1}", url, BuildURLParametersString(parameters));
            }

            object entity = null;



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    entity = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseData);

                }

            }


            return (T)entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string BuildURLParametersString(Dictionary<string, string> parameters)
        {
            UriBuilder uriBuilder = new UriBuilder();

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var urlParameter in parameters)
            {
                query[urlParameter.Key] = urlParameter.Value;
            }

            uriBuilder.Query = query.ToString();

            return uriBuilder.Query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<T>> getListEntity(string url)
        {

            List<T> entity = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    entity = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(responseData);

                }

            }

            return (List<T>)entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> getObject(string url)
        {

            string obj = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {
                    obj = responseMessage.Content.ReadAsStringAsync().Result;
                }

            }


            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> PostEntityString(string url, object entity)
        {
            string result = "";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var param = JsonConvert.SerializeObject(entity);

                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(url, contentPost);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<string>(responseData);
                }

            }


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PostEntity(string url, object entity)
        {

            object result = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var param = JsonConvert.SerializeObject(entity);

                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(url, contentPost);

                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<T>(responseData);
                }

            }


            return (T)result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T> PostEntity(string url, Dictionary<string, string> parameters)
        {
            url = string.Format("{0}{1}", url, BuildURLParametersString(parameters));

            object result = null;



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // var param = Newtonsoft.Json.JsonConvert.SerializeObject(entity);

                HttpContent contentPost = new StringContent("", Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(url, contentPost);

                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseData);
                }

            }


            return (T)result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T> DeleteEntity(string url, Dictionary<string, string> parameters)
        {
            url = string.Format("{0}{1}", url, BuildURLParametersString(parameters));

            object result = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // var param = Newtonsoft.Json.JsonConvert.SerializeObject(entity);

                // HttpContent contentPost = new StringContent("", Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.DeleteAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseData);
                }

            }


            return (T)result;
        }

    }
}
