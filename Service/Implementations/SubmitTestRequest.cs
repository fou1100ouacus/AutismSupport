public class SubmitTestRequest
{
    public int CategoryId { get; set; }
    // السؤال ID : الإجابة (0-4)
    public Dictionary<int, int> Answers { get; set; } = new();
}