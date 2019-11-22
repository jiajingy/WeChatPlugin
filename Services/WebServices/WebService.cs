using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebServices
{
    public abstract class WebService : IWebService
    {
        protected string BASE_URL { get; set; }



        public async Task<string> PostAsync(string url, string data, IDictionary<string, string> headers, string contentType = "application/json; charset=utf-8", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;
            foreach (var header in headers)
                request.Headers.Add(header.Key, header.Value);


            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }

        public async Task<string> PostAsync(string url, string data, string contentType = "application/json; charset=utf-8", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;


            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }

        public async Task<string> PostWithoutDataAsync(string url, IDictionary<string, string> headers, string contentType = "application/json; charset=utf-8", string method = "POST")
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentType = contentType;
            request.Method = method;
            foreach (var header in headers)
                request.Headers.Add(header.Key, header.Value);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }

        public async Task<string> PostWithoutDataAsync(string url, string contentType = "application/json; charset=utf-8", string method = "POST")
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentType = contentType;
            request.Method = method;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }

        public async Task<string> GetAsync(string url, IDictionary<string, string> headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            foreach (var header in headers)
                request.Headers.Add(header.Key, header.Value);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();

        }

        public async Task<string> GetAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();

        }

        public string StringToJSONString(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public dynamic JSONStringToDynamicObject(string JSON)
        {
            return JsonConvert.DeserializeObject<dynamic>(JSON);
        }
    }
}
