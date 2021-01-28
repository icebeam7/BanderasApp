using System;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;
using Xamarin.Essentials;

using BanderasApp.Models;

namespace BanderasApp.Services
{
    public class ServicioApi
    {
        private const string UrlGeonamesApi = "http://api.geonames.org/";
        private const string key = "icebeam";

        private static readonly HttpClient cliente = CrearCliente(UrlGeonamesApi);

        private static HttpClient CrearCliente(string url)
        {
            var c = new HttpClient();
            c.DefaultRequestHeaders
                .Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            c.BaseAddress = new Uri(url);

            return c;
        }

        // 
        public async static Task<List<Pais>> ObtenerPaises()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var url = $"countryInfoJSON?username={key}";

                var respuesta = await cliente.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<InfoPaises>(json);

                    return info.Geonames;
                }
            }

            return new List<Pais>();
        }
    }
}
