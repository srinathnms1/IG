namespace PremiumInvitationGenerator.Controllers
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc;

    using Contracts;
    using CsvReader;
    using PremiumInvitationGenerator.API;
    using static Contracts.Constants;

    [ApiController]
    [Route("api/[controller]")]
    public class LetterGeneratorController : ControllerBase
    {
        private readonly ICsvReader csvReader;
        private readonly IInvitationService invitationService;

        public LetterGeneratorController(
            ICsvReader csvReader,
            IInvitationService invitationService)
        {
            this.csvReader = csvReader;
            this.invitationService = invitationService;
        }

        [HttpGet]
        public bool Get()
        {
            var customerFileLocation = Path.Combine(Environment.CurrentDirectory, CustomerDataFileName);
            var customers = csvReader.Parse<Customer>(customerFileLocation);
            if (customers == null || customers.Count == 0)
            {
                throw new NullReferenceException($"Customer should not be null.");
            }

            customers.ForEach(invitationService.GenerateInvitationLetter);

            return true;
        }
    }
}
