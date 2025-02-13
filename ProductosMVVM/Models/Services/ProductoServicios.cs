using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Services
{
    internal class ProductoServicios(IRepository<Producto> Rprod) : IServices<Producto>
    {
        public List<Producto> SacarProductos()
        {
            return Rprod.GetAll();
        }
        public Producto SacarProducto(int id) 
        {
          return Rprod.Get(id);
        }
        public Producto Get(int id)
        {
           return Rprod.Get(id);
        }

        public List<Producto> GetAll()
        {
            return Rprod.GetAll();
        }

        public void Add(Producto item)
        {
          Rprod.Add(item);
        }

        public void Remove(Producto item)
        {
           Rprod.Remove(item);
        }

        public void Update(Producto item)
        {
           Rprod.Update(item);
        }
    }
}
