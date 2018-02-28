namespace CQRSExample.Infrastructure.Requests
{
    public class BaseAuthenticatedRequest: BaseRequest
    {
        public string Username { get; set; }
    }
}
