using File_Processor.Extensions;

string secret = "abcciaa";

Console.WriteLine($"Secret: {secret}");
Console.WriteLine($"Encrypted: {secret.EncryptSecret()}");
Console.WriteLine($"Longest Substring: {secret.FindLongestSubstring()}");
Console.WriteLine($"Duplicates: {secret.CountDuplicates()}");