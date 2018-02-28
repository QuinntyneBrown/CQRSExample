using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CQRSExample.WebAPI.Services
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AppHub: Hub
    {
    }
}
