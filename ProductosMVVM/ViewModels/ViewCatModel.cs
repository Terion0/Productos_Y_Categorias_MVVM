using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Models.Services;
namespace ProductosMVVM.ViewModels
{
    partial class ViewTwoModel: ObservableObject
    {
        private readonly IServices<Categoria> servicesC;
        [ObservableProperty]
        public Categoria _CategoriaSeleccionado;
        [ObservableProperty]
        public ObservableCollection<Categoria> _ListaCategorias;


        [ObservableProperty]
        public string _NombreCat = "";
        [ObservableProperty]
        public string _IdCat = "";


        public ViewTwoModel(IServices<Categoria> servicesC)
        {
            this.servicesC = servicesC;
            Sinchronice();
        }
        private async void Sinchronice()
        {
            var aDevolver = await servicesC.GetAll();

            ListaCategorias = new ObservableCollection<Categoria>(aDevolver);

        }
        partial void OnCategoriaSeleccionadoChanged(Categoria value)
        {
            if (value != null)
                MostrarInfo();
        }

        [RelayCommand]
        private void EliminarCategoria()
        {
            if (CategoriaSeleccionado != null)
            {
                servicesC.Remove(CategoriaSeleccionado);
                limpiar();
            }
        }
        [RelayCommand]
        private void ModificarCategoria()
        {
            if (CategoriaSeleccionado != null)
            {
          
                CategoriaSeleccionado.Nombre = NombreCat;
                servicesC.Update(CategoriaSeleccionado);
                limpiar();
             
              
            }
            else { MessageBox.Show("Categoria no editada"); }
        }
    

        [RelayCommand]
        private void AñadirCategoria()
        {
            Categoria c = new Categoria();
            c.Nombre=NombreCat;
         //   servicesC.Add(c);   
            CategoriaSeleccionado = null;
            limpiar();       
        }
        public async void limpiar()
        {
           string white = "";
            NombreCat = white;
            IdCat = white;
            ListaCategorias.Clear();
            ListaCategorias = new ObservableCollection<Categoria>(await servicesC.GetAll());
        }
        public void MostrarInfo()
        {
          IdCat = CategoriaSeleccionado.Id.ToString();
          NombreCat = CategoriaSeleccionado.Nombre;
          
        }
    }
    }

