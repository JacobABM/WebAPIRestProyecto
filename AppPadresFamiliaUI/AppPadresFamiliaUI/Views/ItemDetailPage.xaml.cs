using AppPadresFamiliaUI.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppPadresFamiliaUI.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}