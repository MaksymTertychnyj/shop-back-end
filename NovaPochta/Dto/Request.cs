namespace NovaPochta.Dto
{
    public class Request
    {
        public string apiKey { get; set; } = string.Empty;
        public string modelName { get; set; } = string.Empty;
        public string calledMethod { get; set; } = string.Empty;
        public object? methodProperties { get; set; }
    }
}
