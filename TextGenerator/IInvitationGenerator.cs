namespace TextGenerator
{
    public interface IInvitationGenerator
    {
        void Generate(string template, string invitationLetterPath);

        void IsFileExists(string path);
    }
}
