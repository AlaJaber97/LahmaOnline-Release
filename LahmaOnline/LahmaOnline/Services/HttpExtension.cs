using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LahmaOnline.Services
{
    public class HttpExtension<T> where T : class
    {
        public async Task<T> Get(string Controller, string Parameter = "")
        {
            HttpResponseMessage Respones;
            if (!string.IsNullOrWhiteSpace(Parameter))
                Respones = await HttpClinetRespones.Get(HttpMethod.Get, $"{Controller}/{Parameter}");
            else
                Respones = await HttpClinetRespones.Get(HttpMethod.Get, $"{Controller}");

            if (Respones.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    return await Respones.Content.ReadAsAsync<T>();
                }
                catch
                {
                    _ = await Respones.Content.ReadAsStringAsync();
                    throw;
                }
            }
            else
            {
                throw new Exception($"The web service return: {Respones.StatusCode}");
            }
        }
        public async Task<string> GetString(string Controller, string Parameter = "")
        {
            var Respones = await HttpClinetRespones.Get(HttpMethod.Get, $"{Controller}/{ Parameter}");
            if (Respones.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await Respones.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }
        public async Task<T> Send(string Controller)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), "http://afashho-001-site2.atempurl.com/api/" + Controller);
            var Respones = await HttpClinetRespones.Get(HttpMethod.Head, $"{Controller}",request: request);
            if (Respones.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await Respones.Content.ReadAsAsync<T>();
            }
            else
            {
                return null;
            }
        }
        public async Task<T> Post(string Controller, object model, string Parameter = "")
        {
            var Respones = await HttpClinetRespones.Get(HttpMethod.Post, $"{Controller}/{ Parameter}", model);
            if (Respones.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await Respones.Content.ReadAsAsync<T>();
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostReturnStatusCode(string Controller, object model, string Parameter = "")
        {
            var Respones = await HttpClinetRespones.Get(HttpMethod.Post, $"{Controller}/{ Parameter}", model);
            return Respones.StatusCode;
        }
    }
}