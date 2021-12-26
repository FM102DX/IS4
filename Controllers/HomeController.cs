using FerryData.IS4.ViewModels.AccountViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FerryData.IS4.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/error")]
    public class HomeController : Controller
    {
        public HomeController( )
        {

        }

        [HttpGet]
        public string HeloMessage(string returnUrl)
        {
            return $"returnUrl={returnUrl} GetOnerror";
        }
    }
}
