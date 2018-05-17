using Xamarin.Forms;
using TipCalc.ViewModels;

namespace TipCalc
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel VM { get; }

        public MainPage()
        {
            InitializeComponent();
            VM = new MainPageViewModel();
            BindingContext = VM;
        }
    }
}
