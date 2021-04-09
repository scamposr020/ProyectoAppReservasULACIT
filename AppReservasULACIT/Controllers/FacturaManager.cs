using AppReservasULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace AppReservasULACIT.Controllers
{
    public class FacturaManager
    {
        string Url = "http://localhost:49220/api/factura/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Factura>> ObtenerFacturas(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Factura>>(resultado);
        }

        public async Task<Factura> ObtenerFactura(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Factura>(resultado);
        }

        public async Task<Factura> Ingresar(Factura factura, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(factura),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Factura>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Factura> Actualizar(Factura factura, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(factura),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Factura>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
