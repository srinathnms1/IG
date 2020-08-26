namespace Contracts
{
    public class Customer: CustomerInvitation
	{
		[CsvEntry(HeaderName = "ID")]
		public int ID { get; set; }

		[CsvEntry(HeaderName = "Title")]
		public string Title { get; set; }

		[CsvEntry(HeaderName = "FirstName")]
		public string FirstName { get; set; }

		[CsvEntry(HeaderName = "Surname")]
		public string Surname { get; set; }

		[CsvEntry(HeaderName = "ProductName")]
		public string ProductName { get; set; }

		[CsvEntry(HeaderName = "PayoutAmount")]
		public decimal PayoutAmount { get; set; }

		[CsvEntry(HeaderName = "AnnualPremium")]
		public decimal AnnualPremium { get; set; }
	}
}
