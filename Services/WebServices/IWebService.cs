using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebServices
{
    public interface IWebService
    {
        Task<string> PostAsync(string url, string data, IDictionary<string, string> headers, string contentType = "application/json; charset=utf-8", string method = "POST");

        Task<string> PostAsync(string url, string data, string contentType = "application/json; charset=utf-8", string method = "POST");

        Task<string> PostWithoutDataAsync(string url, IDictionary<string, string> headers, string contentType = "application/json; charset=utf-8", string method = "POST");

        Task<string> PostWithoutDataAsync(string url, string contentType = "application/json; charset=utf-8", string method = "POST");

        Task<string> GetAsync(string url, IDictionary<string, string> headers);

        Task<string> GetAsync(string url);

        string StringToJSONString(object obj);

        dynamic JSONStringToDynamicObject(string JSON);
    }
}
