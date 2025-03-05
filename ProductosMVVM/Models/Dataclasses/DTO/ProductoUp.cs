using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Dataclasses.DTO
{
    public class ProductoUp 
    {
        private string nombre;
        private string descripcion;
        private double precio;
        private int idcategoria;
        private string imagen;

        public string Nombre { get => nombre; set => nombre = value; }

        public string Descripcion { get => descripcion; set => descripcion = value; }

        public double Precio { get => precio; set => precio = value; }

        [JsonPropertyName("id_categoria")]
        public int Idcategoria { get => idcategoria; set => idcategoria = value; }

        [JsonPropertyName("uri_imagen")]
        public string Imagen { get => imagen; set => imagen = value; }

    }
}
