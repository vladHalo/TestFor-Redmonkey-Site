using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDataProvider;
using WebProgram.Models.Shared;
using WebProgram.Models.Users;

namespace WebProgram.Models.Users
{
	public class IndexViewModel
	{
		public UserModel CurrentUser {get;set;}
		public List<UserModel> ListUsers { get; set; }
		public int PageIndex { get; set; }
		public int CountPage { get; set; }
		public int PageSize { get; set; }
		public List<UserRolesModel> AllUserRoles { get; set; }
		public List<RolesModel> AllRoles { get; set; }
	}
}