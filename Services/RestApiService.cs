using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace File_Processor.Services
{
    public static class RestApiService
    {
        // Instância reutilizável do HttpClient nativo do C#
        private static readonly HttpClient httpClient = new HttpClient();

        // Baixa um arquivo de um servidor REST API via HTTP GET.
        public static async Task<bool> DownloadFileFromApiAsync(string fileUrl, string destinationPath)
        {
            try
            {
                Console.WriteLine($"\n[REST API] Downloading file from: {fileUrl}...");

                // Faz a requisição GET assíncrona
                HttpResponseMessage response = await httpClient.GetAsync(fileUrl);

                if (response.IsSuccessStatusCode)
                {
                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(destinationPath, fileBytes);
                    Console.WriteLine($"[REST API] File saved sucessfuly: {destinationPath}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"[REST API] Download failed, code: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[REST API] Request error: {ex.Message}");
                return false;
            }
        }
    }
}