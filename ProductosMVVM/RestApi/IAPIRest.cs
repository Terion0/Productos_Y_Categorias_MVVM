using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.RestApi
{
    internal interface IAPIRest<T>
    {
         Task<List<T>> GetAll();
         Task<T> Get(int id);
         Task Remove(T objeto);
         Task Update(T objeto);

    }
}
