using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TipCalc
{
	public partial class MainPage : ContentPage
	{
        public TipCalculator tipCalculator;

		public MainPage()
		{
			InitializeComponent();           

            tipCalculator = new TipCalculator
            {
                Total = 100,
                TipPercent = 18
            };
            tipCalculator.CalcTip();

            BindingContext = tipCalculator;
        }
	}
}
