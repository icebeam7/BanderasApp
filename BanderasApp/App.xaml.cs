using Xamarin.Forms;

namespace BanderasApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.PaisView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
