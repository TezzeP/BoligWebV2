using System;
using System.Net.Http;


namespace WebAppMVC.Helper
{
    public class HttpClientHelperApi
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:34139/"); //50651
            return Client;
        }

    }
}
