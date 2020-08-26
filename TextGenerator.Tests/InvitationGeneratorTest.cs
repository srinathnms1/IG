namespace TextWriter.Tests
{
    using System;
    using System.IO;

    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Contracts;
    using TextGenerator;

    [TestClass]
    public class InvitationGeneratorTest
    {
        private string filePath;

        [TestMethod]
        public void GivenCustomerLetterLocationMailFormatLocationWhenCustomerLetterLocationMailFormatLocationIsPresentThenGenerateInvitationLetter()
        {
            // Arrange
            var customer = this.GetCustomerMock((int)DateTime.Now.Ticks);
            var file = $"{customer.ID}{customer.FirstName}.txt";
            this.filePath = Path.Combine(Environment.CurrentDirectory, file);
            var customerTemplate = "Test Template";
            var mockLogger = new Mock<ILogger<InvitationGenerator>>();

            var invitationGenerator = new InvitationGenerator(mockLogger.Object);

            // Act
            invitationGenerator.Generate(customerTemplate, this.filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            Assert.IsTrue(File.ReadAllLines(filePath).Length > 0);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(this.filePath))
            {
                File.Delete(this.filePath);
            }
        }

        private Customer GetCustomerMock(int id)
        {
            var customer = new Customer() { ID = id, Title = "Mr", FirstName = "John", Surname = "Smith", ProductName = "Enhanced Cover", PayoutAmount = 190820, AnnualPremium = 50 };
            return customer;
        }
    }
}
