using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductosMVVM.Models.Dataclasses;
using ProductosMVVM.Models.Services;
namespace ProductosMVVM.ViewModels
{
    partial class ViewOneModel : ObservableObject
    {
        private readonly IServices<Producto> servicesP;
        private readonly IServices<Categoria> servicesC;
        [ObservableProperty]
        public Producto _ProductoSeleccionado;
        [ObservableProperty]
        public Categoria _CategoriaSeleccionada;
        [ObservableProperty]
        public ObservableCollection<Producto> _ListaProductos;


        [ObservableProperty]
        public string _NombreProd = "";
        [ObservableProperty]
        public string _IdProd = "";
        [ObservableProperty]
        public string _IdCatProd = "";
        [ObservableProperty]
        public string _PrecioProd = "";
        [ObservableProperty]
        public string _DescripcionProd = "";
        [ObservableProperty]
        public string _ImagenProd = "";

        public ViewOneModel(IServices<Producto> servicesP, IServices<Categoria> servicesC)
        {   
            this.servicesP = servicesP;
            this.servicesC = servicesC;
            Sinchronice();

        }

        private async  void Sinchronice() 
        {
           var aDevolver = await servicesP.GetAll();
           
            ListaProductos = new ObservableCollection<Producto>(aDevolver);
           
        }

        [RelayCommand]
        private void EliminarProducto()
        {
            if (ProductoSeleccionado != null)
            {
                servicesP.Remove(ProductoSeleccionado);
                ProductoSeleccionado = null;
                limpiar();                     
            }
        }
        [RelayCommand]
        private async void ModificarProducto()
        {
            int cat = ConvertirAInt(IdCatProd);
            if (cat != -1)
            {
                if (ProductoSeleccionado != null)
                {
                  Categoria c =await servicesC.Get(cat);
                  ProductoSeleccionado.IdCategoria = c.Id;
                    ProductoSeleccionado.Nombre = NombreProd;
                    ProductoSeleccionado.Precio= ConvertirADouble(PrecioProd);
                    ProductoSeleccionado.Descripcion=DescripcionProd;
                    servicesP.Update(ProductoSeleccionado);
                    limpiar();
                }
            }
        }
        [RelayCommand]
        private async void AñadirProducto()
        {
            int cat = ConvertirAInt(IdCatProd);
            if (cat != -1)
            {
                Categoria c = await servicesC.Get(cat);
                Producto p = new();
                p.Nombre = NombreProd;
                p.Precio= ConvertirADouble(PrecioProd);
                p.Descripcion = DescripcionProd;
                p.IdCategoria = c.Id;
                p.Imagen = "";
                servicesP.Add(p);   
                ProductoSeleccionado = null;
                limpiar();
               
            }
        }


        [RelayCommand]
        private void MostrarInf(Producto sender) 
        {
            // limpiar();
            ProductoSeleccionado = sender;
            MostrarInfo();
        }

        public async void limpiar()
        {
            string white = "";
            IdProd = white;
            NombreProd = white;
            IdCatProd = white;
            PrecioProd = white;
            DescripcionProd = white;
            ImagenProd = white; 
            ListaProductos.Clear();
            ListaProductos = new ObservableCollection<Producto>( await servicesP.GetAll());
        }
        public void MostrarInfo()
        {
            IdProd = ProductoSeleccionado.IdProducto.ToString();
            NombreProd = ProductoSeleccionado.Nombre;
            IdCatProd = ProductoSeleccionado.IdCategoria.ToString();
            PrecioProd = ProductoSeleccionado.Precio.ToString();
            DescripcionProd = ProductoSeleccionado.Descripcion;
            ImagenProd = ProductoSeleccionado.Imagen;
        }


        private int ConvertirAInt(string integer)
        {
            try
            {
                return int.Parse(integer.Trim());
            }
            catch { MessageBox.Show("Introduce un número válido"); return -1; }

        }
        private double ConvertirADouble(string integer)
        {
            try
            {
                return double.Parse(integer.Trim());
            }
            catch { MessageBox.Show("Introduce un número válido"); return -1; }

        }


    }


}
