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
    internal class Rcategoria(AppDbContext contexto) : IRepository<Categoria>
    {

        private readonly AppDbContext _contexto = contexto;
        public void Add(Categoria item)
        {
            _contexto.Add(item);
            _contexto.SaveChanges();
        }
        public Categoria? Get(int id)
        {
            bool encontrado = false;
            Categoria? item = null;
            foreach (Categoria i in _contexto.Categories.ToList())
            {
                if (i.Id == id)
                {
                    encontrado = true;
                    item = i;
                }
            }

            return item;
        }
        public List<Categoria> GetAll()
        {
            return _contexto.Categories.ToList();
        }
        public void Remove(Categoria item)
        {
            _contexto.Remove(item);
            _contexto.SaveChanges();
        }
        public void Update(Categoria item)
        {
            _contexto.Entry(item).State = EntityState.Modified;
            _contexto.SaveChanges();
        }


    }
}


