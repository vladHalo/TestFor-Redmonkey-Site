using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using UsersDataProvider;
using WebProgram.Filters;

namespace WebProgram.Filters
{
	public class CustomActionFilter : ActionFilterAttribute, IActionFilter
	{
		void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
		{
			var attributs = filterContext.ActionDescriptor.GetCustomAttributes(false);
			bool check = true;
			foreach (var i in attributs)
				if (i.GetType() == typeof(AllowAnonymousUser))
					check = false;

			//var usersDataProvider = new UsersDataProvider.UsersDataProvider();

			//var userRoles = usersDataProvider.GetUserRoles(CookiesUser.CookiesGet()._Id);
			//if (userRoles[0].RoleId !=1)
			//{
			//	filterContext.Result = new RedirectResult("/Users/AdminCheck");
			//}
			
			if(check)
				if (UsersDataProvider.UsersDataProvider.CurrentUser() == null)
					filterContext.Result = new RedirectResult("/Users/Login");

			//Models models = new Models()
			//{
			//	Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
			//	Action = string.Concat(filterContext.ActionDescriptor.ActionName, " (Logged By: Custom Action Filter)"),
			//	IP = filterContext.HttpContext.Request.UserHostAddress,
			//	DateTime = filterContext.HttpContext.Timestamp
			//};
			//base.OnActionExecuting(filterContext);
		}
	}

	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	sealed class AllowAnonymousUser : Attribute
	{
	}

	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly int role;
		public CustomAuthorizeAttribute(int role)
		{
			this.role = role;
		}
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			var user = UsersDataProvider.UsersDataProvider.CurrentUser();
			foreach (var i in user.UserRolesModels)
				if (i.RoleId == role)
					return true;
			return false;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new RedirectToRouteResult(new
			RouteValueDictionary(new { controller = "Users", action = "AdminCheck" }));
		}
	}
}