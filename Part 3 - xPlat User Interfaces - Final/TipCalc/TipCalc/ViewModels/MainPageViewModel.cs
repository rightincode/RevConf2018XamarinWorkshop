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
            get { return Math.Round(_calculator.TipPercent,2).ToString(); }
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
            get { return Math.Round(_calculator.GrandTotal,2).ToString(); }
        }

        private void CalcTip()
        {
            _calculator.CalcTip();
            tipTxt = Math.Round(_calculator.Tip,2).ToString();
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
