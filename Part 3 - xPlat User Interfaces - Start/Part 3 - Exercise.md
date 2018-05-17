### Part 3 - Cross Platform User Interfaces

1. Add a folder named **ViewModels** in the .NET Standard TipCalc project.
2. Add a new class named **MainPageViewModel** in the new **ViewModels** folder.
3. Update the MainPageViewModel class to implement the **INotifyPropertyChanged** interface. Here is the code:

       using System.ComponentModel;

       namespace TipCalc.ViewModels
       {
            public class MainPageViewModel : INotifyPropertyChanged
            {
                public event PropertyChangedEventHandler PropertyChanged;
            }
       }
4. The MainPageViewModel class will need a TipCalculator to perform the tip calculations.  Update the class to instantiate a TipCalculator in the constructor and assign it to a private member variable.  Below is the updated code:

       public class MainPageViewModel : INotifyPropertyChanged
       {
            readonly TipCalculator _calculator;

            public event PropertyChangedEventHandler PropertyChanged;

            public MainPageViewModel()
            {
                _calculator = new TipCalculator();
            }
       }

5. In order for the viewmodel to interact with the view, the viewmodel must expose its state to the view.  We accomplish this by adding a several properties that the view can access. Add the following code to the **MainPageViewModel**:

       private string totalTxt;
       private string tipTxt;

       public string TotalTxt
       {
            get { return totalTxt; }

            set
            {
                totalTxt = value;

                try
                {
                    string newValue = value;
                    _calculator.Total = decimal.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Total = 0;
                }
                finally
                {
                    CalcTip();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTxt"));
                }
            }
        }

        public string TipTxt
        {
            get { return tipTxt; }
            set
            {
                tipTxt = value;

                try
                {
                    string newValue = value;
                    _calculator.Tip = decimal.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Tip = 0;
                }
                finally
                {
                    CalcTipPercentage();
                }
            }
        }

        public string TipPercentTxt
        {
            get { return _calculator.TipPercent.ToString(); }
        }

        public decimal TipPercent
        {
            get { return _calculator.TipPercent; }
            set
            {
                _calculator.TipPercent = value;
                CalcTip();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            }
        }

        public string GrandTotalTxt
        {
            get { return _calculator.GrandTotal.ToString(); }
        }

6. The viewmodel leverages the TipCalculator object to peform the necessary calcuations when the tip calculator application is in use.  We must add the below code to accomplish this task:

        private void CalcTip()
        {
            _calculator.CalcTip();
            tipTxt = _calculator.Tip.ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }

        private void CalcTipPercentage()
        {
            _calculator.CalcTipPercentage();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }

7. The MainPageViewModel class is now complete.  Here is the code:

       using System;
       using System.ComponentModel;

       namespace TipCalc.ViewModels
       {
            public class MainPageViewModel : INotifyPropertyChanged
            {
                private string totalTxt;
                private string tipTxt;
                readonly TipCalculator _calculator;

                public event PropertyChangedEventHandler PropertyChanged;

                public MainPageViewModel()
                {
                    _calculator = new TipCalculator();
                }

                public string TotalTxt
                {
                    get { return totalTxt; }

                    set
                    {
                        totalTxt = value;

                        try
                        {
                            string newValue = value;
                            _calculator.Total = decimal.Parse(newValue);
                        }
                        catch (Exception)
                        {
                            _calculator.Total = 0;
                        }
                        finally
                        {
                            CalcTip();
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTxt"));
                        }
                    }
                }

                public string TipTxt
                {
                    get { return tipTxt; }
                    set
                    {
                        tipTxt = value;

                        try
                        {
                            string newValue = value;
                            _calculator.Tip = decimal.Parse(newValue);
                        }
                        catch (Exception)
                        {
                            _calculator.Tip = 0;
                        }
                        finally
                        {
                            CalcTipPercentage();
                        }
                    }
                }

                public string TipPercentTxt
                {
                    get { return _calculator.TipPercent.ToString(); }
                }

                public decimal TipPercent
                {
                    get { return _calculator.TipPercent; }
                    set
                    {
                        _calculator.TipPercent = value;
                        CalcTip();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
                    }
                }

                public string GrandTotalTxt
                {
                    get { return _calculator.GrandTotal.ToString(); }
                }

                private void CalcTip()
                {
                    _calculator.CalcTip();
                    tipTxt = _calculator.Tip.ToString();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
                }

                private void CalcTipPercentage()
                {
                    _calculator.CalcTipPercentage();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
                }
            }
       }

8. Add a folder named **View** in the .NET Standard TipCalc project.
9. Move the file named **MainPage.xaml** into the newly created **View** folder.
10. In the MVVM design pattern, our view has a single responsibility to manage the user interaction.  As a result, our view's codebehind (MainPage.xaml.cs) is simplified.  Modify the codebehind to match the following code:

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
11. The view's codebehind has been simplified but we are now supporting a much more feature rich tip calculator application.  As a result, the view's xaml has been expanded.  Replace the below code in the MainPage.xaml file:

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

     with the following code:

        <ContentPage.Content>
            <StackLayout Orientation="Vertical" Spacing="20">
                <StackLayout Orientation="Horizontal">
                    <Label x:Name="lblTotal" Text="Total: "></Label>
                    <Entry x:Name="txtTotal" WidthRequest="100" Keyboard="Numeric" Text="{Binding TotalTxt, Mode=TwoWay}"></Entry>
                </StackLayout>
                <StackLayout Orientation="Horizontal">
                    <Label x:Name="lblTipAmount" Text="Tip Amount: "></Label>
                    <Entry x:Name="txtTipAmount" WidthRequest="100" Keyboard="Numeric" Text="{Binding TipTxt, Mode=TwoWay}"></Entry>
                </StackLayout>
                <Slider x:Name="sldTipCalc"
                        Minimum="0"
                        Maximum="100"
                        Value="{Binding TipPercent, Mode=OneWayToSource}"></Slider>
                <StackLayout Orientation="Horizontal">
                    <Label x:Name="lblTipPercent" Text="Tip %: "></Label>
                    <Label x:Name="txtTipPercent" WidthRequest="100" Text="{Binding TipPercentTxt, Mode=OneWay}"></Label>
                </StackLayout>
                <StackLayout Orientation="Horizontal">
                    <Label x:Name="lblGrandTotal" FontSize="Large" Text="Grand Total: "></Label>
                    <Label x:Name="txtGrandTotal" FontSize="Large" WidthRequest="100" Text="{Binding GrandTotalTxt, Mode=OneWay}"></Label>
                </StackLayout>
            </StackLayout>
        </ContentPage.Content>

12. Execute the program.  The tip calculator application is now fully functional.