namespace NovaPochta.Helpers
{
    public static class HttpClientHelper
    {
        public static string ApiKey { get; set; } = string.Empty;
        public static string GetApiKey(this HttpClient client)
        {
            return ApiKey;
        }

        public static void SetApiKey(this HttpClient client, string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
