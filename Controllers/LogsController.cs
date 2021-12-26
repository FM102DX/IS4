using Microsoft.AspNetCore.Mvc;
using FerryData.IS4.ViewModels.AccountViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Serilog;
using IS4.Service;

namespace IS4.Controllers
{

    [AllowAnonymous]
    [Route("[controller]")]
    public class LogsController : Controller
    {

        LogsManager _logsManager;
        public IActionResult Index(LogsManager logsManager)
        {
            _logsManager = logsManager;

            _logsManager.ReadLogs();

            ViewBag.Lines = _logsManager.Lines;

            return View();
        }
    }
}
