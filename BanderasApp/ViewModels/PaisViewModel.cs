using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using BanderasApp.Models;
using BanderasApp.Services;

namespace BanderasApp.ViewModels
{
    public class PaisViewModel : BaseViewModel
    {
        private List<Pais> paises;

        public List<Pais> Paises
        {
            get => paises;
            set { paises = value; OnPropertyChanged(); }
        }

        private Pais paisActual;

        public Pais PaisActual
        {
            get => paisActual;
            set { paisActual = value; OnPropertyChanged(); }
        }

        private string bandera;

        public string Bandera
        {
            get => bandera;
            set { bandera = value; OnPropertyChanged(); }
        }

        private string resultadoActual;

        public string ResultadoActual
        {
            get => resultadoActual;
            set { resultadoActual = value; OnPropertyChanged(); }
        }

        private int score;

        public int Score
        {
            get => score;
            set { score = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pais> opciones;

        public ObservableCollection<Pais> Opciones
        {
            get => opciones;
            set { opciones = value; OnPropertyChanged(); }
        }

        private Pais paisSeleccionado;

        public Pais PaisSeleccionado
        {
            get => paisSeleccionado;
            set { paisSeleccionado = value; OnPropertyChanged(); }
        }

        public ICommand ComandoCargarDatos { private set; get; }
        public ICommand ComandoEvaluarSeleccion { private set; get; }

        private readonly Random generador;

        public PaisViewModel()
        {
            generador = new Random();
            PaisActual = new Pais() { CountryCode = string.Empty };

            ComandoCargarDatos = new Command(async () => await CargarDatos());
            ComandoEvaluarSeleccion = new Command(async () => await EvaluarSeleccion());
        }

        public async Task CargarDatos()
        {
            Paises = await ServicioApi.ObtenerPaises();
            await GenerarPregunta();
        }

        async Task GenerarPregunta()
        {
            IsBusy = true;

            var totalPaises = Paises.Count;

            if (totalPaises > 0)
            {
                await Task.Delay(3000);

                var lista = new List<Pais>();
                var correcto = generador.Next(0, 4);

                for (int i = 0; i < 4; i++)
                {
                    var indice = generador.Next(0, totalPaises);
                    var pais = Paises[indice];

                    if (!lista.Any(x => x.GeonameId == pais.GeonameId))
                    {
                        lista.Add(pais);

                        if (i == correcto)
                        {
                            PaisActual = pais;
                            Bandera = $"https://raw.githubusercontent.com/hjnilsson/country-flags/master/png250px/{PaisActual.CountryCode.ToLower()}.png";
                        }
                    }
                    else
                    {
                        i--;
                    }
                }

                Opciones = new ObservableCollection<Pais>(lista);
                ResultadoActual = string.Empty;
            }

            IsBusy = false;
        }

        async Task EvaluarSeleccion()
        {
            if (!IsBusy && PaisSeleccionado != null)
            {
                var puntos = 10;

                if (PaisActual.GeonameId == PaisSeleccionado.GeonameId)
                {
                    Score += puntos;
                    ResultadoActual = "¡Correcto! =)";
                }
                else
                {
                    ResultadoActual = "¡Incorrecto! =(";
                }

                await GenerarPregunta();
            }
        }
    }
}
