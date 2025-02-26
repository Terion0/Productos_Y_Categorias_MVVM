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
            // Crear la URL para el endpoint
            Uri uri = new Uri("http://localhost:70/categorias"); // Ajusta la URL de la API según corresponda

            try
            {
                // Serializar el objeto Categoria a JSON
                string jsonContent = JsonSerializer.Serialize(categoria);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud POST
                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

                // Comprobar si la respuesta es exitosa
                if (!response.IsSuccessStatusCode)
                {
                    // Si la respuesta no fue exitosa, puedes manejar el error aquí
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores (como problemas de conexión)
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        public async Task<Categoria> Get(int id)
        {
            Categoria deAPI = new();

            Uri uri = new Uri("http://localhost:70/categorias/"+id);
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                  
                    string content = await response.Content.ReadAsStringAsync();

                   
                    deAPI = JsonSerializer.Deserialize<Categoria>(content);
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

            Uri uri = new Uri("http://localhost:70/categorias");
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                   
                    string content = await response.Content.ReadAsStringAsync();

                    deAPI = JsonSerializer.Deserialize<List<Categoria>>(content);
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


            Uri uri = new Uri("http://localhost:70/categorias/" + objeto.Id);
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Update(Categoria objeto)
        {
            // La URL de la API para actualizar la categoría
            Uri uri = new Uri("http://localhost:70/categorias/" + objeto.Id);  // Ajusta la URL según tu configuración

            try
            {
                CategoriaUp categoriaUpdate = new();
                categoriaUpdate.Nombre = objeto.Nombre;
                // Serializamos el objeto CategoriaUpdate a JSON
                string jsonContent = JsonSerializer.Serialize(categoriaUpdate);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realizamos la solicitud PATCH
                HttpResponseMessage response = await _httpClient.PatchAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    // Si la respuesta no es exitosa, lanzar excepción o manejar error
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar categoría: {response.StatusCode}, {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Excepción: {ex.Message}");
                throw;
            }
        }
    }
}


