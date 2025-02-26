using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Models.Dataclasses.DTO;
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

        public async Task Create(Producto objeto)
        {
            Uri uri = new Uri("http://localhost:70/productos"); 
            try
            {         
                string jsonContent = JsonSerializer.Serialize(objeto);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);  
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public async Task<Producto> Get(int id)
        {
            Producto deAPI = new();
            Uri uri = new Uri("http://localhost:70/productos/" + id);
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    deAPI = JsonSerializer.Deserialize<Producto>(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return deAPI;
        }
        public async Task<List<Producto>> GetAll()
        {
            List<Producto> deAPI = new List<Producto>();
            Uri uri = new Uri("http://localhost:70/productos");
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    deAPI = JsonSerializer.Deserialize<List<Producto>>(content);
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
            Uri uri = new Uri("http://localhost:70/productos/" + objeto.IdProducto);
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async Task Update(Producto objeto)
        {
            Uri uri = new Uri("http://localhost:70/productos/" + objeto.IdProducto);

            try
            {
                ProductoUp ProductoUpdate = new();
                ProductoUpdate.Nombre = objeto.Nombre;
                ProductoUpdate.Precio = objeto.Precio;
                ProductoUpdate.Descripcion = objeto.Descripcion;
                ProductoUpdate.Imagen = objeto.Imagen;
                ProductoUpdate.IdCategoria = objeto.IdCategoria;
                string jsonContent = JsonSerializer.Serialize(ProductoUpdate);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PatchAsync(uri, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");

            }

        }
    }
}
