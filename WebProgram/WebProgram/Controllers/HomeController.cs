using System.Web.Mvc;
using WebProgram.Filters;
using WebProgram.Models.Home;

namespace WebProgram.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymousUser]
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
            return View(model);
        }
        [AllowAnonymousUser]
        public ActionResult About()
        {
            var model = new IndexViewModel();
            model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
            return View(model);
        }

        [AllowAnonymousUser]
        [CustomAuthorize(0)]
        public ActionResult Contact()
        {
            var model = new IndexViewModel();
            model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
            return View(model);
        }
    }
}