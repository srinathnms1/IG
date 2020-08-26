namespace TextGenerator
{
    using Contracts;
    using PremiumCalculator;

    public class PremiumCalculatorBuilder : IPremiumCalculatorBuilder
    {
        private readonly IPremiumCalculator premiumCalculator;
        private Customer customer;

        public PremiumCalculatorBuilder(IPremiumCalculator premiumCalculator)
        {
            this.premiumCalculator = premiumCalculator;
        }

        public IPremiumCalculatorBuilder SetCustomer(Customer customer)
        {
            this.customer = customer;
            return this;
        }

        public IPremiumCalculatorBuilder CalculateCreditCharge()
        {
            this.customer.CreditCharge = this.premiumCalculator.CalculateCreditCharge(this.customer.AnnualPremium);
            return this;
        }

        public IPremiumCalculatorBuilder CalculateTotalPremium()
        {
            this.CalculateCreditCharge();
            this.customer.TotalPremium = this.premiumCalculator.CalculateTotalPremium(this.customer.AnnualPremium);
            return this;
        }

        public IPremiumCalculatorBuilder CalculateAverageMonthlyPremium()
        {
            this.CalculateTotalPremium();
            this.customer.AverageMonthlyPremium = this.premiumCalculator.CalculateAverageMonthlyPremium(this.customer.AnnualPremium, 12);
            return this;
        }

        public IPremiumCalculatorBuilder CalculateInitialMonthlyPaymentAmount()
        {
            this.CalculateAverageMonthlyPremium();
            this.customer.InitialMonthlyPaymentAmount = this.premiumCalculator.CalculateInitialMonthlyPaymentAmount(this.customer.AnnualPremium, 12);
            return this;
        }

        public IPremiumCalculatorBuilder CalculateOtherMonthlyPaymentsAmount()
        {
            this.CalculateAverageMonthlyPremium();
            this.customer.OtherMonthlyPaymentsAmount = this.premiumCalculator.CalculateOtherMonthlyPaymentsAmount(this.customer.AnnualPremium, 12);
            return this;
        }
    }
}
