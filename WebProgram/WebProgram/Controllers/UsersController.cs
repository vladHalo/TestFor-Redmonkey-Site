using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using UsersDataProvider;
using WebProgram.Filters;
using WebProgram.Models.Shared;
using WebProgram.Models.Users;

namespace WebProgram.Controllers
{
	public class UsersController : BaseController
	{
		[HttpGet]
		[AllowAnonymousUser]
		public ActionResult Login()
		{
			var indexViewModel = new UserViewModel();
			if (UsersDataProvider.UsersDataProvider.CurrentUser() != null)
				return RedirectToAction("AcceptGood");
			return View(indexViewModel);
		}

		[HttpPost]
		[AllowAnonymousUser]
		public ActionResult Login(UserViewModel indexViewModel)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();
			var userInfo = usersDataProvider.Login(indexViewModel.Email, indexViewModel.Password, out bool checkLogin);

			if (checkLogin)
			{
				CookiesUser.CookiesSet(userInfo.Id, userInfo.Name, userInfo.Email);
				return RedirectToAction("AcceptGood");
			}
			return View(indexViewModel);
		}

		[HttpGet]
		[AllowAnonymousUser]
		public ActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymousUser]
		public ActionResult Registration(UserViewModel indexViewModel)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();

			bool checkAgain = true;
			foreach (var i in usersDataProvider.Get())
				if (i.Name == indexViewModel.Name || i.Email == indexViewModel.Email)
					checkAgain = false;

			if (checkAgain)
				usersDataProvider.Create(indexViewModel.Name, indexViewModel.Email, indexViewModel.Password);
			return RedirectToAction("Login");
		}

		public ActionResult Index(int pageIndex = 1, int pageSize = 50)
		{
			var model = new IndexViewModel();
			model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();

			model.PageIndex = pageIndex;
			model.PageSize = pageSize;
			model.ListUsers = usersDataProvider.GetUserPage(null, pageIndex - 1, pageSize, out double totalUsers);
			model.CountPage = Convert.ToInt32(Math.Ceiling(totalUsers / pageSize));
			model.AllRoles = usersDataProvider.GetRoles();
			return View(model);
		}

		public JsonResult UserListJson(string nameUser, int pageIndex = 1, int pageSize = 50)
		{
			var model = new IndexViewModel();
			model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();

			model.PageIndex = pageIndex;
			model.PageSize = pageSize;
			model.ListUsers = usersDataProvider.GetUserPage(nameUser, pageIndex - 1, pageSize, out double totalUsers);
			model.CountPage = Convert.ToInt32(Math.Ceiling(totalUsers / pageSize));
			
			BaseResponse baseResponse = new BaseResponse(model);
			return Json(baseResponse, JsonRequestBehavior.AllowGet);
		}

		public JsonResult RemoveUserJson(int id)
		{
			var userDataProvider = new UsersDataProvider.UsersDataProvider();
			BaseResponse baseResponse = new BaseResponse(userDataProvider.Delete(id));
			return Json(baseResponse, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[AllowAnonymousUser]
		public ActionResult Logout()
		{
			CookiesUser.CookiesDelete();
			return View();
		}

		[HttpGet]
		public ActionResult CreateUser()
		{
			var model = new IndexViewModel();
			model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
			return View(model);
		}

		[HttpPost]
		public ActionResult CreateUser(UserViewModel indexUserViewModel)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();
			usersDataProvider.Create(indexUserViewModel.Name, indexUserViewModel.Email, indexUserViewModel.Password);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult CreateUserJson()
		{
			return View();
		}

		[HttpPost]
		public JsonResult CreateUserJson(UserViewModel indexUserViewModel)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();
			var deleted = usersDataProvider.Create(indexUserViewModel.Name, indexUserViewModel.Email, indexUserViewModel.Password);
			var response = new BaseResponse(deleted);
			return Json(response);
		}

		[HttpGet]
		public ActionResult UpdateUser()
		{
			var model = new IndexViewModel();
			model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
			return View(model);
		}

		[HttpPost]
		public ActionResult UpdateUser(UserViewModel indexUserViewModel)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();
			UserModel userModel = new UserModel()
			{
				Id = indexUserViewModel.Id,
				Name = indexUserViewModel.Name,
				Email = indexUserViewModel.Email,
				Password = indexUserViewModel.Password
			};
			usersDataProvider.Update(userModel);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult UpdateUserJson()
		{
			return View();
		}

		[HttpPost]
		public JsonResult UpdateUserJson(UserViewModel indexUserViewModel)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();
			UserModel userModel = new UserModel()
			{
				Id = indexUserViewModel.Id,
				Name = indexUserViewModel.Name,
				Email = indexUserViewModel.Email,
				Password = indexUserViewModel.Password
			};
			var update = usersDataProvider.Update(userModel);
			var response = new BaseResponse(update);
			return Json(response);
		}

		[HttpGet]
		public ActionResult AcceptGood()
		{
			return View(CookiesUser.CookiesGet());
		}

		[HttpGet]
		public ActionResult AdminCheck()
		{
			return View();
		}

		public JsonResult SaveRoles(int userId,List<int> listRole)
		{
			if (listRole == null)
			{
				return Json(new BaseResponse(null,400,"List"));
			}
			var usersDataProvaider = new UsersDataProvider.UsersDataProvider();
			usersDataProvaider.SaveUserRoles(userId, listRole);
			var roles = usersDataProvaider.GetUserRoles(userId);
			var response = new BaseResponse(roles);
			return Json(response);
		}

		public JsonResult GetRoles(int userId)
		{
			var usersDataProvaider = new UsersDataProvider.UsersDataProvider();
			var roles = usersDataProvaider.GetUserRoles(userId);
			var response = new BaseResponse(roles);
			return Json(response);
		}
	}
}