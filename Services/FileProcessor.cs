using File_Processor.Extensions;
using File_Processor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Processor.Services
{
    public static class FileProcessor
    {
        // Lê o arquivo CSV processa os segredos e gera a lista de secrets

        public static List<Secret> ProcessImport2(string filePath)
        {
            List<Secret> secrets = new List<Secret>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return secrets;
            }

            string[] lines = File.ReadAllLines(filePath);

            // Pula a primeira linha

            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');
                string secret1 = parts[0];
                string secret2 = parts[1];

                // Decodifica o secret misturado usando o método do kata BuildSecret
                string secretValue = secret1.BuildSecret(secret2);

                // Cria o objeto Secret com todas as propriedades calculadas
                Secret secretObj = new Secret
                {
                    Value = secretValue,
                    Encrypted = secretValue.EncryptSecret(),
                    LongestSubstring = secretValue.FindLongestSubstring(),
                    DuplicateCount = secretValue.CountDuplicates(),
                    AlmostPalindrome = secretValue.IsAlmostPalindrome()
                };

                secrets.Add(secretObj);

            }

            return secrets;

        }


        // Lê o arquivo CSV 1 e processa os nomes/tempos/m35 e gera a lista de SecretName
        public static List<SecretName> ProcessImport1(string filePath)
        {
            List<SecretName> secretNames = new List<SecretName>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return secretNames;
            }

            string[] lines = File.ReadAllLines(filePath);

            // Pula o cabeçalho (secret, name, m35, time)
            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(",");
                string secret = parts[0];
                string rawName = parts[1];
                int m35Value = int.Parse(parts[2]);
                int timeSeconds = int.Parse(parts[3]);

                SecretName secretNameObj = new SecretName
                {
                    Secret = secret,
                    Name = rawName.ToCamelCase(),
                    CalculatedM35 = m35Value.CalculateM35(),
                    Time = timeSeconds.ToReadableTime()
                };
                secretNames.Add(secretNameObj);
            }

            return secretNames;
        }

        public static bool TryMoveToProcessed(string originalPath, out string destinationPath)
        {
            try
            {
                string directory = Path.GetDirectoryName(originalPath) ?? "Files";
                string processedFolder = Path.Combine(directory, "processed");

                if (!Directory.Exists(processedFolder))
                {
                    Directory.CreateDirectory(processedFolder);
                }

                destinationPath = Path.Combine(processedFolder, Path.GetFileName(originalPath));

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                File.Move(originalPath, destinationPath);
                return true;

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error to move the file: {ex.Message}");
                destinationPath = string.Empty;
                return false;
            }
        }


    }
}
