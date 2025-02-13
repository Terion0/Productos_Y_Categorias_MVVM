using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Services
{

    internal class ProductoServicios(IAPIRest<Producto> Rprod) : IServices<Producto>
    {

        public void Add(Producto item)
        {

        }

        public Task<Producto> Get(int id)
        {
            return Rprod.Get(id);
        }

        public Task<List<Producto>> GetAll()
        {
            return Rprod.GetAll();
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