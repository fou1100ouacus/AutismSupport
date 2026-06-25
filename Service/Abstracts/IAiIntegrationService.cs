using Data.DTOs;

namespace Service.Abstracts
{
    public interface IAiIntegrationService
    {
        Task<AiAnalysisResponse> AnalyzeVideoAsync(string videoFilePath);
        Task<AiAnalysisResponse> AnalyzeVideoFromStreamAsync(Stream videoStream, string fileName);
    }
}
