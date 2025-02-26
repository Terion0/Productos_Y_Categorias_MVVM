using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Services
{
    internal class CategoriaServicios(IAPIRest<Categoria> Rcat) : IServices<Categoria>
    {

        public void Add(Categoria item)
        {
            Rcat.Create(item);
        }

        public Task<Categoria> Get(int id)
        {
            return Rcat.Get(id);
        }

        public Task<List<Categoria>> GetAll()
        {
            return Rcat.GetAll();
        }

        public void Remove(Categoria item)
        {
            Rcat.Remove(item);
        }

        public void Update(Categoria item)
        {
            Rcat.Update(item);
        }
    }
}
    