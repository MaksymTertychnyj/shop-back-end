namespace Shop.Domain.Dto.NovaPochta
{
    public class RequestDto
    {
        public string ModelName { get; set; } = string.Empty;
        public string CalledMethod { get; set; } = string.Empty;
        public object? MethodProperties { get; set; }
    }
}
