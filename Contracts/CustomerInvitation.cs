namespace Contracts
{
    public class CustomerInvitation
    {
        public decimal CreditCharge { get; set; }

        public decimal TotalPremium { get; set; }

        public decimal AverageMonthlyPremium { get; set; }

        public decimal InitialMonthlyPaymentAmount { get; set; }

        public decimal OtherMonthlyPaymentsAmount { get; set; }
    }
}
