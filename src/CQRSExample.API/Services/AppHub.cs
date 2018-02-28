using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CQRSExample.API.Services
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AppHub: Hub
    {
    }
}
