using System;
using System.Collections.Generic;

using Xamarin.Forms;

using BanderasApp.ViewModels;

namespace BanderasApp.Views
{
    public partial class PaisView : ContentPage
    {
        PaisViewModel vm;

        public PaisView()
        {
            InitializeComponent();

            vm = new PaisViewModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await vm.CargarDatos();
        }
    }
}
