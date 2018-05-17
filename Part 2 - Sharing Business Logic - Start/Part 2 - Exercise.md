### Part 2 - Sharing Business Logic

1. Add a new class named "TipCalculator" to the .NET standard TipCalc project.

2. Add the following constructor to the TipCalculator Class
 
       public TipCalculator() { }

3. Add the following class properties to the TipCalculator class:
   
       public decimal Total { get; set; }
       public decimal Tip { get; set; }
       public decimal TipPercent { get; set; }
       public decimal GrandTotal { get; private set; }

4. Add the following method named UpdateGrandTotal to the TipCalculator class:

       private void UpdateGrandTotal()  
       {  
            GrandTotal = Total + Tip;  
       }

5. Add the following method named CalcTip to the TipCalculator class:

        public void CalcTip()  
        {  
            if (TipPercent > 0)  
            {  
                Tip = Total * (TipPercent / 100);  
            }  
            else  
            {  
                Tip = 0;  
            }  
            UpdateGrandTotal();  
        }  

6. Add the following method named CalcTipPercentage to the TipCalculator class:

       public void CalcTipPercentage()
       {
           if (Total > 0)
           {
               TipPercent = (Tip / Total) * 100;
           }
           else
           {
               TipPercent = 0;
           }
           UpdateGrandTotal();
       }

*Note - This is the complete TipCalculator Class

    public class TipCalculator
    {
        public decimal Total { get; set; }

        public decimal Tip { get; set; }

        public decimal TipPercent { get; set; }

        public decimal GrandTotal { get; private set; }

        public TipCalculator() { }

        private void UpdateGrandTotal()
        {
            GrandTotal = Total + Tip;
        }

        public void CalcTip()
        {
            if (TipPercent > 0)
            {
                Tip = Total * (TipPercent / 100);
            }
            else
            {
                Tip = 0;
            }
            UpdateGrandTotal();
        }

        public void CalcTipPercentage()
        {
            if (Total > 0)
            {
                TipPercent = (Tip / Total) * 100;
            }
            else
            {
                TipPercent = 0;
            }

            UpdateGrandTotal();
        }        
    }

7. Modify the MainPage partial class in the codebehind of the MainPage (MainPage.xaml.cs) so that is matches the following code:

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

8. Replace the following xaml in the MainPage.xaml file:

       <StackLayout>
            <!-- Place new controls here -->
            <Label Text="Welcome to Xamarin.Forms!" 
                HorizontalOptions="Center"
                VerticalOptions="CenterAndExpand" />
       </StackLayout>

   with the below code:

        <StackLayout>
            <!-- Place new controls here -->
            <StackLayout Orientation="Horizontal">
                <Label Text="Total Bill: "></Label>
                <Label Text="{Binding Total, Mode=OneWay}"></Label>
            </StackLayout>
            <StackLayout Orientation="Horizontal">
                <Label Text="Tip %: "></Label>
                <Label Text="{Binding TipPercent, Mode=OneWay}"></Label>
            </StackLayout>
            <StackLayout Orientation="Horizontal">
                <Label Text="Grand Total: "></Label>
                <Label Text="{Binding GrandTotal, Mode=OneWay}"></Label>
            </StackLayout>
        </StackLayout>

9. Execute the program and you should see the output of utilizing the tipCalculator class to calculate an 18% tip on a $100 total.

   Total Bill: 100  
   Tip %: 18  
   Grand Total: 118.00  