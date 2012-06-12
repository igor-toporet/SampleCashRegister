namespace SampleCashRegister.Core
{
    public class Coupon
    {
        public decimal AmountOff { get; set; }

        public decimal Threshold { get; set; }

        public bool HasBeenApplied { get; set; }
    }
}