using Xamarin.Forms;
using TipCalc.ViewModels;
using TipCalc_core.Models;

namespace TipCalc
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel VM { get; }

        public MainPage()
        {
            InitializeComponent();
            VM = new MainPageViewModel(new TipCalculator());
            BindingContext = VM;
        }
    }
}
