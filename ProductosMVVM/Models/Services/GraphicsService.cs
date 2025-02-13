using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Repositories;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosMVVM.Models.Services
{
    internal class GraphicsService(IRepository<Categoria> Rcat, IRepository<Producto> Rprod)
    {
        public ISeries[] DevolverPieSeries()
        {
            double totalProductos= Rprod.GetAll().Count();
            var categoriasAgrupadas = Rcat.GetAll().GroupBy(c => c.Id).Select(group => new { Id = group.Key, Nombre = group.First().Nombre, Count = group.Count() });
            ISeries[] Tarta = categoriasAgrupadas.Select(cat => new PieSeries<double>
            {
                Name = cat.Nombre,
                Values = new[] { 
                    
                      ((double)Rprod.GetAll().Count(p => p.IdCategoria == cat.Id) / totalProductos) * 100 }
            }).ToArray();


            return Tarta;
        }

        public Axis[] DevolverEjeX()
        {
            Axis[] rtc = new Axis[1];
            Axis d = new Axis
            {
                Labels = Rcat.GetAll().ConvertAll(x => x.Nombre).ToArray(),
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
 
        public ISeries[] DevolverValores() {
            int[] cantidad = new int[Rcat.GetAll().Count];
            for (int i = 0; i < cantidad.Length;i++) {
                cantidad[i] = Rprod.GetAll().Count(p => p.IdCategoria == Rcat.GetAll()[i].Id);
            }
            ISeries[] Values = 
                [
                new ColumnSeries<int>
                { 
                    Name="Cantidades",
                    Values = cantidad
                 }
                ];
            return Values;
            }

          
    }
}
