using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Quickstart.UI.Models;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspNetIdentity.Repositories;
using System.Dynamic;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServerWithAspNetIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        ILogger<HomeController> _logger;
        ViewRenderService _viewRender;

        public HomeController(IIdentityServerInteractionService interaction, ILogger<HomeController> logger, ViewRenderService viewRender)
        {
            _interaction = interaction;
            _logger = logger;
            _viewRender = viewRender;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            //ViewModel is of type dynamic - just for testing
            dynamic x = new ExpandoObject();
            x.Test = "Yes";
            var viewWithViewModel = _viewRender.Render("eNotify/Confirm.cshtml", x);
            var viewWithoutViewModel = _viewRender.Render("MyFeature/Test.cshtml");
            return viewWithViewModel + viewWithoutViewModel;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}
