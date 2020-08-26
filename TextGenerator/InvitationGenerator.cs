namespace TextGenerator
{
    using System.IO;

    using Microsoft.Extensions.Logging;

    public class InvitationGenerator : IInvitationGenerator
    {
        private readonly ILogger<InvitationGenerator> logger;

        public InvitationGenerator(ILogger<InvitationGenerator> logger)
        {
            this.logger = logger;
        }

        public void Generate(string templateContent, string invitationLetterPath)
        {
            if (!File.Exists(invitationLetterPath))
            {
                using (var streamWriter = new StreamWriter(invitationLetterPath))
                {
                    streamWriter.Write(templateContent);
                    logger.LogInformation($"Renewal Invitation letter has been generated.");
                    return;
                }
            }
            logger.LogInformation($"Renewal Invitation letter already exists for the customer.");
        }

        public void IsFileExists(string path)
        {
            var isFileExists = File.Exists(path);
            if (!isFileExists)
            {
                var file = Path.GetFileName(path);
                throw new FileNotFoundException($"File({file}) doesn't exists in the path {path}");
            }
        }
    }
}
