using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountSettingsController : ControllerBase
    {
        private readonly IAccountSettingsService _accountSettingsService;
        public AccountSettingsController(IAccountSettingsService accountSettingsService)
        {
            _accountSettingsService = accountSettingsService;
        }
    }
}
