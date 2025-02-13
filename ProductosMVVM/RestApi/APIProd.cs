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
    internal class APIProd : IAPIRest<Producto>
    {
       private HttpClient _httpClient = new HttpClient();
       JsonSerializerOptions _serializerOptions;


        public APIProd() 
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }

        public Task<Producto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producto>> GetAll()
        {
            List<Producto> deAPI = new();
       
            Uri uri = new Uri(string.Format("https://api.escuelajs.co/api/v1/products", string.Empty));
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
                            string imagenFiltrar = jsonProduct.GetProperty("images")[0].GetString();
                            imagenFiltrar = imagenFiltrar.Replace("[", "").Replace("]", "").Replace("\\", "").Replace("\"", "");
                            Producto producto = new Producto
                                {
                                    Nombre = jsonProduct.GetProperty("title").GetString(),
                                    Descripcion = jsonProduct.GetProperty("description").GetString(),
                                    Precio = jsonProduct.GetProperty("price").GetInt32(),
                                    IdCategoria = jsonProduct.GetProperty("category").GetProperty("id").GetInt32(),
                                    IdProducto = jsonProduct.GetProperty("id").GetInt32(),
                                    Imagen = imagenFiltrar
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
        

        public async Task Remove(Producto objeto)
        {
            Uri uri = new Uri($"https://api.escuelajs.co/api/v1/products/{objeto.IdProducto}");
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Producto eliminado correctamente.");
                }     
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excepción al eliminar el producto: {ex.Message}");
            }
        }

        public async Task Update(Producto objeto)
        {
            throw new NotImplementedException();

        }
    }
}
