using Microsoft.Extensions.DependencyInjection;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.ViewModels;
using ProductosMVVM.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using ProductosMVVM.Data;
using Microsoft.EntityFrameworkCore;
using ProductosMVVM.Models.Services;
using ProductosMVVM.Models.RestApi;

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
            services.AddScoped<IAPIRest<Producto>, APIProd>();
            services.AddScoped<IAPIRest<Categoria>, APICat>();
            services.AddScoped<IServices<Categoria>, CategoriaServicios>();
            services.AddScoped<IServices<Producto>, ProductoServicios>();
            services.AddScoped<SettinsService>();
            services.AddScoped<GraphicsService>();
                
            var serviceProvider = services.BuildServiceProvider();
           

            var view = serviceProvider.GetService<MainWindow>();
            view.DataContext = serviceProvider.GetService<MainViewModel>();

            view.Show();
        }
    }
}
