using File_Processor.Models;
using File_Processor.Services;

// Caminho para o arquivo de teste
string import2Path = Path.Combine("Files", "example_import2.csv");

Console.WriteLine("Processando Import 2 CSV Secrets");
List<Secret> secrets = FileProcessor.ProcessImport2(import2Path);

foreach (Secret s in secrets)
{
    Console.WriteLine($"\nSecret: {s.Value}");
    Console.WriteLine($" Encrypted: {s.Encrypted}");
    Console.WriteLine($" Longest Substring: {s.LongestSubstring}");
    Console.WriteLine($" Duplicates: {s.DuplicateCount}");
    Console.WriteLine($" Almost Palindrome: {s.AlmostPalindrome}");
}