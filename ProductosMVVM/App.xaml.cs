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
using ProductosMVVM.RestApi;

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


            services.AddSingleton<IAPIRest<Producto>, APIProd>();
            services.AddSingleton<IAPIRest<Categoria>, APICat>();
            services.AddSingleton<IServices<Categoria>, CategoriaServicios>();
            services.AddSingleton<IServices<Producto>, ProductoServicios>();
            services.AddSingleton<SettinsService>();
            services.AddSingleton<GraphicsService>();
                
            var serviceProvider = services.BuildServiceProvider();
           

            var view = serviceProvider.GetService<MainWindow>();
            view.DataContext = serviceProvider.GetService<MainViewModel>();

            view.Show();
        }
    }
}
