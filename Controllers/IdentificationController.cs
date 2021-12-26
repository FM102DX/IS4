using FerryData.IS4.ViewModels.AccountViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;

namespace FerryData.IS4.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class IdentificationController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private Serilog.ILogger _logger;

        public IdentificationController(
            IIdentityServerInteractionService interaction,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            Serilog.ILogger logger
            )
        {
            _interaction = interaction;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public string HeloMessage(string returnUrl)
        {
            return $"Hi, this is IS4 v.2 IdentificationController";

        }

        [HttpGet("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _logger.Information($"Entering POST[action] {model.ToString()}");


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Remember, true);

            if (!result.Succeeded)
            {
                return View(model);
            }

            return Redirect(model.ReturnUrl);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            _logger.Information($"Entering GET[action] logoutId={logoutId}");
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await _signInManager.SignOutAsync();

            return Redirect(logout.PostLogoutRedirectUri);
        }

        [HttpGet("[action]")]
        public IActionResult Register(string returnUrl)
        {
            _logger.Information($"Entering Register returnUrl={returnUrl}");
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            _logger.Information($"Entering POST[action]-2 {model.ToString()}");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
                await _signInManager.SignInAsync(user, false);

                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }
    }
}
