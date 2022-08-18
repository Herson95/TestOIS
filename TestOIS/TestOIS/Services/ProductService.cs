namespace TestOIS.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;
    using Constans;
    using Newtonsoft.Json;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using Xamarin.Essentials;
    using Plugin.Connectivity;

    public class ProductService : INetworkService, IProductService
    {
        #region Constructor
        public ProductService()
        {
            if (httpClient == null)
            {
                try
                {
                    httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(Constans.Url),
                        MaxResponseContentBufferSize = 999999,
                        Timeout = TimeSpan.FromSeconds(60),
                    };

                }
                catch (Exception)
                {
                    httpClient = null;
                }
            }
        }
        #endregion

        #region Attributes
        private readonly HttpClient httpClient;
        #endregion

        #region Methods
        public async Task<Response> CheckConnectionAsync()
        {
            /*the maximum port number is 65535 and here is a list of known service port (The list is updated frequently).
             * Try using a lower port number.
             * https://stackoverflow.com/questions/15657266/java-lang-illegalargumentexception-port-out-of-range67001 */
            Response response = new();
            string host = "google.com";
            int port = 80;
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    response.IsSuccess = false;
                    response.Message = "Please turn on your internet";
                    return response;
                }

                bool isReachable = false;
                
                if (port <= 65535)
                {
                    isReachable = await CrossConnectivity.Current.IsRemoteReachable(host, port);

                    if (!isReachable)
                    {
                        isReachable = await CrossConnectivity.Current.IsRemoteReachable(Constans.Url);
                    }

                  
                   if (!isReachable)
                    {
                        response.IsSuccess = false;
                        response.Message = "Check you internet connection";
                        return response;
                    }
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok"
                };
            }
            catch (Exception)
            {
                
               
            }
            response.Message = "Error connection";
            return response;

        }

        public async Task<Response> GetProductsAsync()
        {
            try
            {
                string url = string.Format("{0}", "Products");
                using HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader readStream = new StreamReader(stream, Encoding.UTF8);
                if (response.IsSuccessStatusCode)
                {
                    List<Product> model = JsonConvert.DeserializeObject<List<Product>>(await readStream.ReadToEndAsync());
                    stream.Close();
                    readStream.Close();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = model,
                    };
                }
                else
                {
                    Response error = JsonConvert.DeserializeObject<Response>(await readStream.ReadToEndAsync());
                    stream.Close();
                    readStream.Close();
                    error.IsSuccess = false;
                    return error;
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        #endregion



    }
}

