namespace PremiumInvitationGenerator.Tests
{
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Contracts;
    using Moq;
    using PremiumInvitationGenerator.API;
    using TextGenerator;

    [TestClass]
    public class InvitationServiceTest
    {
        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenGenerateRenewalInvitationLetters()
        {
            // Arrange
            var mockPremiumCalculatorBuilder = new Mock<IPremiumCalculatorBuilder>();
            var mockInvitationGenerator = new Mock<IInvitationGenerator>();
            var mockInvitationServiceLogger = new Mock<ILogger<InvitationService>>();

            var customer = new Customer() { ID = 1, Title = "Mr", FirstName = "John", Surname = "Smith", ProductName = "Enhanced Cover", PayoutAmount = 190820, AnnualPremium = 50 };
            mockInvitationGenerator.Setup(x => x.Generate(It.IsAny<string>(), It.IsAny<string>()));
            mockInvitationGenerator.Setup(x => x.Generate(It.IsAny<string>(), It.IsAny<string>()));
            mockPremiumCalculatorBuilder.Setup(x => x.SetCustomer(It.IsAny<Customer>())).Returns(mockPremiumCalculatorBuilder.Object);
            mockPremiumCalculatorBuilder.Setup(x => x.CalculateAverageMonthlyPremium()).Returns(mockPremiumCalculatorBuilder.Object);
            mockPremiumCalculatorBuilder.Setup(x => x.CalculateCreditCharge()).Returns(mockPremiumCalculatorBuilder.Object);
            mockPremiumCalculatorBuilder.Setup(x => x.CalculateInitialMonthlyPaymentAmount()).Returns(mockPremiumCalculatorBuilder.Object);
            mockPremiumCalculatorBuilder.Setup(x => x.CalculateOtherMonthlyPaymentsAmount()).Returns(mockPremiumCalculatorBuilder.Object);
            mockPremiumCalculatorBuilder.Setup(x => x.CalculateTotalPremium()).Returns(mockPremiumCalculatorBuilder.Object);
            var invitationService = new InvitationService(
                mockPremiumCalculatorBuilder.Object, mockInvitationGenerator.Object, mockInvitationServiceLogger.Object);

            // Act
            invitationService.GenerateInvitationLetter(customer);

            // Assert
            mockPremiumCalculatorBuilder.Verify(c => c.SetCustomer(It.IsAny<Customer>()), Times.Once);
            mockInvitationGenerator.Verify(c => c.Generate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}