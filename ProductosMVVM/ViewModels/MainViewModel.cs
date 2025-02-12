
using ProductosMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductosMVVM.Models.Services;
using ProductosMVVM.Models.Dataclasses;
using Microsoft.Identity.Client;
using SkiaSharp;
using ProductosMVVM.Models.RestApi;

namespace ProductosMVVM.ViewModels
{
    partial class MainViewModel(SettinsService settins, GraphicsService graphics, IServices<Producto> prodacts, IServices<Categoria> categoris): ObservableObject
    {


        [ObservableProperty]
        private object _ActiveView;

        public HomeViewModel HomeViewModel { get; } = new HomeViewModel(categoris, prodacts);

        public ViewOneModel ViewOneModel { get; } = new ViewOneModel(prodacts);

        public ViewTwoModel ViewTwoModel { get; } = new ViewTwoModel(categoris);

        public SettingsViewModel ViewSettings { get; } = new SettingsViewModel(settins);

        public GraphicsViewModel GraphicsView { get; } = new GraphicsViewModel(graphics);




        [RelayCommand]
        private void ActivateHomeView() => ActiveView = HomeViewModel;
             
        [RelayCommand]
        private void ActiveOneView() => ActiveView = ViewOneModel;

        [RelayCommand]
        private void ActiveTwoView() => ActiveView = ViewTwoModel;

        [RelayCommand]
        private void UnactivateView() => ActiveView = null;

        [RelayCommand]
        private void ActivateSettingsView() => ActiveView = ViewSettings;

        [RelayCommand]
        private void ActivateGraphicsView() => ActiveView = GraphicsView; 




    }
}
    