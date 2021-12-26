using FerryData.IS4.ViewModels.AccountViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FerryData.IS4.Controllers
{
    [AllowAnonymous]
    [Route("/")]
    public class HelloController : Controller
    {
        private Serilog.ILogger _logger;
        public HelloController(Serilog.ILogger logger )
        {
            _logger= logger;
        }

        [HttpGet]
        public string HeloMessage(string returnUrl)
        {
            _logger.Information($"Said Hello");
            return "Hi, this is IS4 v.2";
        }
    }
}
