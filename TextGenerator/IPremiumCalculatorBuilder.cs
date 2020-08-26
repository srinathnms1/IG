namespace TextGenerator
{
    using Contracts;

    public interface IPremiumCalculatorBuilder
    {
        IPremiumCalculatorBuilder SetCustomer(Customer customer);
        IPremiumCalculatorBuilder CalculateCreditCharge();
        IPremiumCalculatorBuilder CalculateTotalPremium();
        IPremiumCalculatorBuilder CalculateAverageMonthlyPremium();
        IPremiumCalculatorBuilder CalculateInitialMonthlyPaymentAmount();
        IPremiumCalculatorBuilder CalculateOtherMonthlyPaymentsAmount();
    }
}
