using ProductosMVVM.Models.Dataclasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ProductosMVVM.RestApi
{
    internal class APICat : IAPIRest<Categoria>
    {
       private HttpClient _httpClient = new HttpClient();
        JsonSerializerOptions _serializerOptions;


        public APICat()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }

        public Task<Categoria> Get(int id)
        {
            throw new NotImplementedException();
            //asdasdasd
        }

        public async Task<List<Categoria>> GetAll()
        {
            List<Categoria> deAPI = new();

            Uri uri = new Uri(string.Format("https://api.escuelajs.co/api/v1/categories", string.Empty));
            try
            {
               
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    string content = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(content))
                    {
                        JsonElement array = doc.RootElement;
                        foreach (JsonElement jsonProduct in array.EnumerateArray())
                        { 
                                Categoria producto = new Categoria
                                {
                                    Nombre = jsonProduct.GetProperty("name").GetString(),
                                    Id = jsonProduct.GetProperty("id").GetInt32()
                                };
                                deAPI.Add(producto);
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }

            return deAPI;
        }


        public async Task Remove(Categoria objeto)
        {
            Uri uri = new Uri($"https://api.escuelajs.co/api/v1/categories/{objeto.Id}");
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Categoria eliminada correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excepción al eliminar categoria: {ex.Message}");
            }
        }

        public Task Update(Categoria objeto)
        {
            throw new NotImplementedException();
        }
    }
}

