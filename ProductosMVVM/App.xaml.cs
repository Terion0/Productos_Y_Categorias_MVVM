using Microsoft.Extensions.DependencyInjection;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Repositories;
using ProductosMVVM.ViewModels;
using ProductosMVVM.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using ProductosMVVM.Data;
using Microsoft.EntityFrameworkCore;
using ProductosMVVM.Models.Services;
using LiveChartsCore;

namespace ProductosMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceCollection services = new();

            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();

            services.AddTransient<HomeView>();
            services.AddTransient<HomeViewModel>();

            services.AddTransient<ViewOne>();
            services.AddTransient<ViewOneModel>();


            services.AddTransient<ViewTwo>();
            services.AddTransient<ViewTwoModel>();

            services.AddTransient<SettingsWindow>();
            services.AddTransient<SettingsViewModel>();

            services.AddTransient<ViewGraphics>();
            services.AddTransient<GraphicsViewModel>();
            services.AddScoped<IRepository<Categoria>, Rcategoria>();
            services.AddScoped<IRepository<Producto>, Rproductos>();

            services.AddSingleton<IServices<Categoria>, CategoriaServicios>();
            services.AddSingleton<IServices<Producto>, ProductoServicios>();
            services.AddSingleton<SettinsService>();
            services.AddSingleton<GraphicsService>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=localhost,1433;Database=TuBaseDeDatos;User Id=sa;Password=Interfaces-2425;TrustServerCertificate=true;"));
            var serviceProvider = services.BuildServiceProvider();
            // Solo para cargar datos dummy, quitar en aplicación en producción.
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
                if (dbContext.Categories.Count<Categoria>() == 0)
                {
                    dbContext.Categories.Add(new Categoria { Nombre = "Cosas" });
                    dbContext.Categories.Add(new Categoria { Nombre = "Más cosas" });
                    dbContext.Items.Add(new Producto { Nombre = "Cosa 1", Descripcion = "Descripción de la cosa", IdCategoria = 1, Imagen = "https://bcw-media.s3.ap-northeast-1.amazonaws.com/text_to_image_v6_poster_01_f038887d26.jpg" });
                    dbContext.Items.Add(new Producto { Nombre = "Cosa 2", Descripcion = "Descripción de la cosa", IdCategoria = 2, Imagen = "https://bcw-media.s3.ap-northeast-1.amazonaws.com/text_to_image_v6_poster_04_9952550906.jpg" });
                    dbContext.Items.Add(new Producto { Nombre = "Cosa 3", Descripcion = "Descripción de la cosa", IdCategoria = 1, Imagen = "https://via.placeholder.com/150/0000FF/808080?text=Producto+3" });
                    dbContext.Items.Add(new Producto { Nombre = "Cosa 4", Descripcion = "Descripción de la cosa", IdCategoria = 1, Imagen = "https://via.placeholder.com/150/0000FF/808080?text=Producto+4" });
                    dbContext.Items.Add(new Producto { Nombre = "Cosa 5", Descripcion = "Descripción de la cosa", IdCategoria = 2, Imagen = "https://via.placeholder.com/150/0000FF/808080?text=Producto+5" });
                }
                dbContext.SaveChanges();
            }
            //

            var view = serviceProvider.GetService<MainWindow>();
            view.DataContext = serviceProvider.GetService<MainViewModel>();

            view.Show();
        }
    }
}
