namespace TipCalc
{
    public class TipCalculator
    {
        public decimal Total { get; set; }

        public decimal Tip { get; set; }

        public decimal TipPercent { get; set; }

        public decimal GrandTotal { get; private set; }

        public TipCalculator() { }

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

        private void UpdateGrandTotal()
        {
            GrandTotal = Total + Tip;
        }
    }
}
