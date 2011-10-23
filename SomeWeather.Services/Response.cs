namespace SomeWeather.Services
{
    public class Response
    {
        public bool Success { get; set; }

        public string FailureReason { get; set; }

        public static implicit operator bool(Response response)
        {
            return response.Success;
        }
    }
}