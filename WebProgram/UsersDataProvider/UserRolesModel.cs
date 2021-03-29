using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersDataProvider
{
	public class UserRolesModel
	{
		public int Id { get; set; }
		public int RoleId { get; set; }
		public int UserId { get; set; }
	}
}