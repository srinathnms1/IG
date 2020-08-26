namespace CsvReader.Tests
{
    using System;
    using System.IO;

    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static Contracts.Constants;

    [TestClass]
    public class CsvReaderTest
    {
        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsPresentThenReturnCustomerList()
        {
            // Arrange
            var customerDataFilePath = Path.Combine(Environment.CurrentDirectory, CustomerDataFileName);
            var csvReader = new CsvReader();

            // Act
            var result = csvReader.Parse<Customer>(customerDataFilePath);

            // Assert   
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerCsvIsWrongThenReturnFileNotFoundException()
        {
            // Arrange
            var csvReader = new CsvReader();

            // Act
            Action actual = () => csvReader.Parse<Customer>(null);

            // Assert
            Assert.ThrowsException<FileNotFoundException>(actual);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsNullThenReturnArgumentNullException()
        {
            // Arrange
            var customerNullDataFilePath = Path.Combine(Environment.CurrentDirectory, "CustomerNoData.csv");
            var csvReader = new CsvReader();

            // Act
            Action actual = () => csvReader.Parse<Customer>(customerNullDataFilePath);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(actual);
        }
    }
}
