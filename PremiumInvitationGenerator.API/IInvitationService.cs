namespace PremiumInvitationGenerator.API
{
    using Contracts;

    public interface IInvitationService
    {
        void GenerateInvitationLetter(Customer customer);
    }
}
