using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Repositories
{
    internal interface IRepository<T>
    {
        public T Get(int id);
        public List<T> GetAll();
        public void Add(T item);
        public void Remove(T item);
        public void Update(T item);
      
    }
}
