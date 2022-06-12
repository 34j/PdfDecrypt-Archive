using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.Security;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System;
using System.IO;

namespace PdfDecrypt
{
    class Program
    {
        static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Argument(0, Description = "The path of the file to remove the password from."), Required]
        public string FromPath { get; private set; } = "";
        [Argument(1, Description = "The path to save the decrypted file to. (Optional)")]
        public string ToPath { get; private set; } = "";
        [Option(ShortName = "p", LongName = "password", Description = "The password to use to decrypt the file.")]
        public string Password { get; private set; } = "";
        [Option(ShortName = "nc", LongName = "noclose", Description = "Whether not to close the application after decrypting the file.", ShowInHelpText = false)]
        public bool NoClose { get; private set; } = false;
        [Option(ShortName = "na", LongName = "noaddextension", Description = "Whether not to add \".pdf\" to FromPath or ToPath if it does not have extension.")]
        public bool NoAddExtension { get; private set; } = false;
        private void OnExecute()
        {
            try
            {
                Execute();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{e.GetType()}: {e.Message}");
            }
            if (NoClose)
            {
                Console.ReadLine();
            }
        }

        private void Execute()
        {
            // Check FromPath
            FromPath = Path.GetFullPath(FromPath ?? "");            
            if (!Path.HasExtension(FromPath) && !NoAddExtension)
            {
                FromPath = Path.ChangeExtension(FromPath, ".pdf");
            }
            if (!File.Exists(FromPath))
            {
                throw new FileNotFoundException("The file to decrypt does not exist.", FromPath);
            }

            // Check ToPath
            if (string.IsNullOrEmpty(ToPath))
            {
                ToPath = Path.ChangeExtension(FromPath, ".decrypted.pdf");
            }
            else
            {
                ToPath = Path.GetFullPath(ToPath);
                if (!Path.HasExtension(ToPath) && !NoAddExtension)
                {
                    ToPath = Path.ChangeExtension(ToPath, ".pdf");
                }
            }

            // Check Password
            if (string.IsNullOrEmpty(Password))
            {
                Password = Prompt.GetPassword("Password: ");
            }

            // Execute
            RemovePassword(Path.GetFullPath(FromPath), Password ?? "", Path.GetFullPath(ToPath));
        }

        public static void RemovePassword(string fromPath, string password, string toPath)
        {
            bool tried = false;
            PdfDocument document = PdfReader.Open(fromPath, PdfDocumentOpenMode.Modify, args =>
            {
                if (!tried)
                {
                    tried = true;
                    args.Password = password;
                }
                else
                {
                    throw new Exception("Password is incorrect.");
                }
            });

            if (document != null)
            {
                document.SecuritySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.None;
                foreach (PdfPage page in document.Pages)
                {
                    ;
                }
                document.Save(toPath);
            }
            else
            {
                throw new Exception("Could not open file.");
            }
        }
    }
}