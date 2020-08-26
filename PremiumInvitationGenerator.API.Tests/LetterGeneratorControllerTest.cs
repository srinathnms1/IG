namespace PremiumInvitationGenerator.Tests
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using CsvReader;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using PremiumInvitationGenerator.API;
    using PremiumInvitationGenerator.Controllers;

    [TestClass]
    public class LetterGeneratorControllerTest
    {
        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsPresentThenGenerateRenewalInvitationLetters()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            var mockCsvReader = new Mock<ICsvReader>();

            var customers = this.GetCustomersMock();
            mockCsvReader.Setup(x => x.Parse<Customer>(It.IsAny<string>())).Returns(customers);
            mockInvitationService.Setup(x => x.GenerateInvitationLetter(It.IsAny<Customer>()));
            var controller = new LetterGeneratorController(mockCsvReader.Object, mockInvitationService.Object);

            // Act
            var result = controller.Get();

            // Assert
            mockCsvReader.Verify(c => c.Parse<Customer>(It.IsAny<string>()), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerIsNullThenReturnNullReferenceException()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            var mockCsvReader = new Mock<ICsvReader>();

            mockCsvReader.Setup(x => x.Parse<Customer>(It.IsAny<string>()));
            var controller = new LetterGeneratorController(mockCsvReader.Object, mockInvitationService.Object);

            // Act
            Action actual = () => controller.Get();

            // Assert
            Assert.ThrowsException<NullReferenceException>(actual);
        }

        private List<Customer> GetCustomersMock()
        {
            var customers = new List<Customer>
            {
                new Customer() { ID = 1, Title = "Mr", FirstName = "John", Surname = "Smith", ProductName = "Enhanced Cover", PayoutAmount = 190820, AnnualPremium = 50 }
            };
            return customers;
        }
    }
}
