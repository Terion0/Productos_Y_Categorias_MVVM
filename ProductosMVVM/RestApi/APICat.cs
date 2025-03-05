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

        public async Task Create(Categoria categoria)
        {
            Uri uri = new Uri(string.Format("http://localhost:70/categorias", string.Empty));
            try
            {
                string jsonContent = JsonSerializer.Serialize(categoria,_serializerOptions);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error en la creación: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async Task<Categoria> Get(int id)
        {
            Categoria deAPI = new();

            Uri uri = new Uri(string.Format("http://localhost:70/categorias/"+id, string.Empty));
            try
            {
               
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {      
                    string content = await response.Content.ReadAsStringAsync();
                    deAPI = JsonSerializer.Deserialize<Categoria>(content, _serializerOptions);
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return deAPI;
        }

        public async Task<List<Categoria>> GetAll()
        {
            List<Categoria> deAPI = new List<Categoria>();
            Uri uri = new Uri(string.Format("http://localhost:70/categorias", string.Empty));
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {  
                    string content = await response.Content.ReadAsStringAsync();
                    deAPI = JsonSerializer.Deserialize<List<Categoria>>(content,_serializerOptions);
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
            Uri uri = new Uri(string.Format("http://localhost:70/categorias/"+objeto.Id,string.Empty));
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error al eliminar la categoría: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Update(Categoria objeto)
        {
            Uri uri = new Uri(string.Format("http://localhost:70/categorias/"+objeto.Id,string.Empty));
            try
            {
                CategoriaUp categoriaUpdate = new() ;
                categoriaUpdate.Nombre = objeto.Nombre;
                string jsonContent = JsonSerializer.Serialize(categoriaUpdate, _serializerOptions);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PatchAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error al actualizar la categoría: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


