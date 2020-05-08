using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LahmaOnline.Services
{
    public static class HttpClinetRespones
    {
        static string urlApi = "http://testwebapi.justdevelopmentjo.com/api/";
        public async static Task<HttpResponseMessage> Get(HttpMethod method, string url, object model = null, HttpRequestMessage request = null)
        {
            HttpResponseMessage response;
            var URL = new Uri(urlApi + url);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("lang", AppStatics.Language.ToString());
            client.DefaultRequestHeaders.Authorization = await GetHeaderValue(method.Method.ToString());
            switch (method.Method.ToString())
            {
                case "GET":
                    response = await client.GetAsync(URL);
                    break;
                case "POST":
                    response = await client.PostAsJsonAsync(URL, model);
                    break;
                case "HEAD":
                    response = await client.SendAsync(request);
                    break;
                default:
                    response = null;
                    break;
            }
            return response;
        }

        static string TokenStatic;
        private async static Task<AuthenticationHeaderValue> GetHeaderValue(string MethodType)
        {
            if (string.IsNullOrWhiteSpace(TokenStatic))
            {
                var DataPasswordEncrpty = Helper.Rfc2898.Encrypt("123456");
                var model = new BLL.M.Identity.Login()
                {
                    Mail = "a.jad@sama-amoun.com",
                    EncryptionKey = DataPasswordEncrpty.KeyUsed,
                    Password = DataPasswordEncrpty.Encrypt
                };
                HttpResponseMessage response;
                var url = "User/CheckUserByEmail";
                var URL = new Uri(urlApi + url);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", AppStatics.Language.ToString());
                response = await client.PostAsJsonAsync(URL, model);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var Respones = await response.Content.ReadAsAsync<BLL.M.Identity.ResponseLogin>();
                    TokenStatic = Respones.Token;
                }
            }
            return new AuthenticationHeaderValue("Bearer", TokenStatic);
        }
    }
}

