using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Services
{
    internal class CategoriaServicios(IRepository<Categoria> Rcat) : IServices<Categoria>
    {

        public void Add(Categoria item)
        {
            Rcat.Add(item);
        }

        public Categoria Get(int id)
        {
           return Rcat.Get(id);
        }

        public List<Categoria> GetAll()
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
