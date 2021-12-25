using System;
using System.Net.Http;


namespace BoligWebApp.Helper
{
    public class HttpClientHelperApi
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:44350/"); //50651
            return Client;
        }

    }
}
