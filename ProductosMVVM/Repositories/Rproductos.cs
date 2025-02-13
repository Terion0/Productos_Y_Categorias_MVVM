using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Models.Repositories.DataAcces;
using ProductosMVVM.Data;


namespace ProductosMVVM.Models.Repositories
{
    internal class Rproductos(AppDbContext _contexto) : IRepository<Producto>
    {
       


        public void Add(Producto item)
        {
            _contexto.Add(item);
            _contexto.SaveChanges();

        }
        public Producto? Get(int id)
        {


            bool encontrado = false;
            Producto? item = null;
            foreach (Producto i in _contexto.Items.ToList())
            {
                if (i.IdProducto == id)
                {
                    encontrado = true;
                    item = i;
                }
            }

            return item;
        }
        public List<Producto> GetAll()
        {
            return _contexto.Items.ToList();
        }
        public void Remove(Producto item)
        {
            _contexto.Remove(item);
            _contexto.SaveChanges();
        }
        public void Update(Producto item)
        {
            _contexto.Entry(item).State = EntityState.Modified;
            _contexto.SaveChanges();
        }
    }
}

