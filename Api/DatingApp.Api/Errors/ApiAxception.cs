namespace DatingApp.Api.Errors
{
    public class ApiAxception
    {
        public ApiAxception(int statusCode, string messages, string details)
        {
            StatusCode = statusCode;
            Messages = messages;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Messages { get; set; }
        public string Details { get; set; }
    }
}
