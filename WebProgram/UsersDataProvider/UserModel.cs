using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersDataProvider
{
	public class UserModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public List<UserRolesModel> UserRolesModels { get; set; }

		public UserModel()
		{
			Id = -1;
			Name = "";
			Email = "";
			Password = "";
			CreatedDate = DateTime.Now;
			UpdatedDate = DateTime.Now;
			UserRolesModels = null;
		}
	}
}