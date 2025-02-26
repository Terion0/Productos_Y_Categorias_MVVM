using ProductosMVVM.Models.Dataclasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductosMVVM.Models.Dataclasses
{

    public class Categoria : CategoriaUp
    {
        private int id;
       

        [JsonPropertyName("id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
       


        override public string ToString()
        {
            return  Nombre + "\n" +
                   " Id: " + Id;

        }
    }

}
