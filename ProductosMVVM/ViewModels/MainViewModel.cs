
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
    partial class MainViewModel(HomeViewModel homeViewModel, ViewOneModel viewOneModel, ViewTwoModel viewTwoModel, 
        SettingsViewModel settingsViewModel, GraphicsViewModel graphicsViewModel ): ObservableObject
    {


        [ObservableProperty]
        private object _ActiveView;

        public HomeViewModel HomeViewModel { get; } = homeViewModel;

        public ViewOneModel ViewOneModel { get; } = viewOneModel;

        public ViewTwoModel ViewTwoModel { get; } = viewTwoModel;

        public SettingsViewModel ViewSettings { get; } = settingsViewModel;

        public GraphicsViewModel GraphicsView { get; } = graphicsViewModel;




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
    