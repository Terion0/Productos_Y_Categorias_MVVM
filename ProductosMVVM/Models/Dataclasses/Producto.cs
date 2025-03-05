using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using ProductosMVVM.Models.Dataclasses.DTO;

public class Producto : ProductoUp
    {
        private int id;
       
        public int Id { get =>id; set => id=value; }

        override public string ToString()
        {
            return  Nombre;
                   

        }
    }

