namespace PremiumInvitationGenerator.API
{
    using System;
    using System.IO;

    using Contracts;
    using static Contracts.Constants;
    using Microsoft.Extensions.Logging;
    using TextGenerator;

    public class InvitationService : IInvitationService
    {
        private readonly IPremiumCalculatorBuilder premiumCalculatorBuilder;
        private readonly IInvitationGenerator invitationGenerator;
        private readonly ILogger<InvitationService> logger;

        public InvitationService(
            IPremiumCalculatorBuilder premiumCalculatorBuilder,
            IInvitationGenerator invitationGenerator,
            ILogger<InvitationService> logger)
        {
            this.premiumCalculatorBuilder = premiumCalculatorBuilder;
            this.invitationGenerator = invitationGenerator;
            this.logger = logger;
        }

        public void GenerateInvitationLetter(Customer customer)
        {
            if (customer == null)
            {
                logger.LogInformation("Renewal invitation letter cannot be generated as customer is null.");
                return;
            }
            if (customer.AnnualPremium == 0)
            {
                logger.LogInformation($"Renewal invitation letter cannot be generated as {customer} as its not a valid record.");
                return;
            }

            logger.LogInformation($"Started Updating the customer details");
            premiumCalculatorBuilder
               .SetCustomer(customer)
               .CalculateCreditCharge()
               .CalculateTotalPremium()
               .CalculateAverageMonthlyPremium()
               .CalculateInitialMonthlyPaymentAmount()
               .CalculateOtherMonthlyPaymentsAmount();

            var templateFilePath = Path.Combine(Environment.CurrentDirectory, RenewalInvitationLetterTemplateFileName);
            var templateContent = GetInvitationContent(customer, templateFilePath);
            var letterFileName = $"{customer.ID}{customer.FirstName}.txt";
            var invitationLetterPath = Path.Combine(Environment.CurrentDirectory, "Renewal Invitation Letters", letterFileName);
            if (invitationLetterPath == null)
            {
                throw new DirectoryNotFoundException($"Path({invitationLetterPath}) doesn't exists.");
            }

            invitationGenerator.Generate(templateContent, invitationLetterPath);

            string GetInvitationContent(Customer customer, string templateFilePath)
            {
                logger.LogInformation($"Generating Renewal invitation letter for {customer.Title} {customer.FirstName} ...");
                invitationGenerator.IsFileExists(templateFilePath);

                var currentDate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var mailTemplate = File.ReadAllText(templateFilePath);
                mailTemplate = string.Format(mailTemplate,
                        currentDate,
                        customer.Title,
                        customer.FirstName,
                        customer.Surname,
                        customer.ProductName,
                        customer.PayoutAmount,
                        customer.AnnualPremium,
                        customer.CreditCharge,
                        customer.TotalPremium,
                        customer.InitialMonthlyPaymentAmount,
                        customer.OtherMonthlyPaymentsAmount
                        );

                return mailTemplate;
            }
        }
    }
}
