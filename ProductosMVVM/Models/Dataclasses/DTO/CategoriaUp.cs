using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Dataclasses.DTO
{
    public  class CategoriaUp
    {
        private string nombre;

        public string? Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
