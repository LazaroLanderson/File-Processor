using File_Processor.Models;
using File_Processor.Services;

// Caminho para os arquivos de teste
string import1Path = Path.Combine("Files", "example_import1.csv");
string import2Path = Path.Combine("Files", "example_import2.csv");
string outputPath = Path.Combine("Files", "secret_xml.xml");


// IMPORT 1
Console.WriteLine("Processing Import 1 CSV Secret Names");
List<SecretName> secretNames = FileProcessor.ProcessImport1(import1Path);
Console.WriteLine($"Loads {secretNames.Count} secrets names.");


foreach (SecretName sn in secretNames)
{
    Console.WriteLine($"\nSecret: {sn.Secret}");
    Console.WriteLine($"Name: {sn.Name}");
    Console.WriteLine($"M35: {sn.CalculatedM35}");
    Console.WriteLine($"Time: {sn.Time}");
}


Console.WriteLine("------------------------------------------------------------");


// IMPORT 2
Console.WriteLine("Processing Import 2 CSV Secrets");
List<Secret> secrets = FileProcessor.ProcessImport2(import2Path);
Console.WriteLine($"Loads: {secrets.Count} secrets");


foreach (Secret s in secrets)
{
    Console.WriteLine($"\nSecret: {s.Value}");
    Console.WriteLine($" Encrypted: {s.Encrypted}");
    Console.WriteLine($" Longest Substring: {s.LongestSubstring}");
    Console.WriteLine($" Duplicates: {s.DuplicateCount}");
    Console.WriteLine($" Almost Palindrome: {s.AlmostPalindrome}");
}


Console.WriteLine($"Export to XML: ");
XmlExporter.ExportToXml(secrets, secretNames, outputPath);


// Archive the files before the loading

if (FileProcessor.TryMoveToProcessed(import1Path, out string newPath1))
{
    Console.WriteLine($"File 1 moved to: {newPath1}");
}

if (FileProcessor.TryMoveToProcessed(import2Path, out string newPath2))
{
    Console.WriteLine($"File 2 moved to: {newPath2}");
}