using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRestAPIPadreFamiliaUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablaEvaluacion : ContentPage
    {
        public TablaEvaluacion()
        {
            InitializeComponent();
        }
        async void ir(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TablaEvaluacion());
        }
    }
}