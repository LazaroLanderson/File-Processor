using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using File_Processor.Models;

namespace File_Processor.Services
{
    internal class XmlExporter
    {

        // Controle para duplicação
        private static readonly HashSet<string> ExportedSecretIds = new HashSet<string>();

        // Junta a lista de Secrets com a lista de SecrentNames e gera o arquivo XML formatado

        public static void ExportToXml(List<Secret> secrets, List<SecretName> secretNames, string outputPath)
        {
            // Criando a estrutura XML raiz <information>
            XElement root = new XElement("information");

            foreach (Secret secret in secrets)
            {
                if (ExportedSecretIds.Contains(secret.Value))
                {
                    continue; // Pula o secret se já tiver sido exportado antes
                }

                // Buscamos usando LINQ todos os nomes que pertencem a este secret
                var matchingNames = secretNames.Where(sn => sn.Secret == secret.Value);

                // Criamos o elemento <secret id="...">

                XElement secretElement = new XElement(

                    "Secret",
                    new XAttribute("id", secret.Value),
                    new XElement("longest_substring", secret.LongestSubstring),
                    new XElement("duplicates_count", secret.DuplicateCount),
                    new XElement("almost_palindrome", secret.AlmostPalindrome.ToString().ToLower()),
                    new XElement("encrypted", secret.Encrypted),
                    new XElement("names", matchingNames.Select(sn => new XElement("Name",
                    new XAttribute("id", sn.Name),
                    new XElement("time", sn.Time),
                    new XElement("calculated_m35", sn.CalculatedM35)

                    ))

                    )
                    );


                root.Add(secretElement);
                ExportedSecretIds.Add(secret.Value);
            }

            // Criando o arquivo e salvando
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            doc.Save(outputPath);

            Console.WriteLine($"Export XML sucessfuly in: {outputPath}");


        }

    }
}
