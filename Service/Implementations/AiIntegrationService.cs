using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Data.DTOs;
using Service.Abstracts;

namespace Service.Implementations
{
    public class AiIntegrationService : IAiIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _aiServerUrl = "https://asmaanasr11-asd-motion-detector.hf.space/analyze_video";

        public AiIntegrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(5); // AI analysis can take time
        }

        public async Task<AiAnalysisResponse> AnalyzeVideoAsync(string videoFilePath)
        {
            using var fileStream = File.OpenRead(videoFilePath);
            var fileName = Path.GetFileName(videoFilePath);
            return await AnalyzeVideoFromStreamAsync(fileStream, fileName);
        }

        public async Task<AiAnalysisResponse> AnalyzeVideoFromStreamAsync(Stream videoStream, string fileName)
        {
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(videoStream);
            
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("video/mp4");
            content.Add(fileContent, "video", fileName);

            var response = await _httpClient.PostAsync(_aiServerUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"AI server returned {response.StatusCode}: {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AiAnalysisResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize AI response");
        }
    }
}
