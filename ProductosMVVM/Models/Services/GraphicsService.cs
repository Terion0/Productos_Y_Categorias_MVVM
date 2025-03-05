using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.RestApi;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Services
{
    internal class GraphicsService
    {
        private readonly IAPIRest<Categoria> _categoriesApi;
        private readonly IAPIRest<Producto> _productsApi;

        public GraphicsService(IAPIRest<Categoria> categoriesApi, IAPIRest<Producto> productsApi)
        {
            _categoriesApi = categoriesApi;
            _productsApi = productsApi;
        }

        public async Task<ISeries[]> DevolverPieSeries()
        {
            var productos = await _productsApi.GetAll();
            var categorias = await _categoriesApi.GetAll();
            double totalProductos = productos.Count;
            var categoriasAgrupadas = categorias.GroupBy(c => c.Id).Select(group => new { Id = group.Key, Nombre = group.First().Nombre, Count = group.Count() });
            ISeries[] Tarta = categoriasAgrupadas.Select(cat => new PieSeries<double>
            {
                Name = cat.Nombre,
                Values = new[] { ((double)productos.Count(p => p.Idcategoria == cat.Id) / totalProductos) * 100 }
            }).ToArray();

            return Tarta;
        }

        public async Task<Axis[]> DevolverEjeX()
        {
            var categorias = await _categoriesApi.GetAll();
            Axis[] rtc = new Axis[1];
            Axis d = new Axis
            {
                Labels = categorias.ConvertAll(x => x.Nombre).ToArray(),
                LabelsRotation = 10,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                ForceStepToMin = true,
                MinStep = 1
            };
            rtc[0] = d;
            return rtc;
        }

        public async Task<ISeries[]> DevolverValores()
        {
            var productos = await _productsApi.GetAll();
            var categorias = await _categoriesApi.GetAll();
            int[] cantidad = new int[categorias.Count];
            for (int i = 0; i < cantidad.Length; i++)
            {
                cantidad[i] = productos.Count(p => p.Idcategoria == categorias[i].Id);
            }
            ISeries[] Values =
            {
                new ColumnSeries<int>
                {
                    Name = "Cantidades",
                    Values = cantidad
                }
            };
            return Values;
        }
    }
}
