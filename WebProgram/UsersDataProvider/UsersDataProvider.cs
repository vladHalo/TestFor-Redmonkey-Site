using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UsersDataProvider
{
	public class UsersDataProvider
	{
		//static string connectionLine = "Data Source=tcp:ondrashdev.database.windows.net,1433;Initial Catalog=TestDb;" +
		//		"Persist Security Info=True; User ID=ondrashdev;Password=Is=4820017000025";
		static string connectionLine = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Calculator;Integrated Security=True";

		public List<RolesModel> GetRoles()
		{
			string SqlQuetion = "GetRoles";
			var listRoles = new List<RolesModel>();
			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var roles = new RolesModel();
							roles.Id = reader.GetInt32(0);
							roles.Role = reader.GetString(1);
							roles.Description = reader.GetString(2);
							listRoles.Add(roles);
						}
					}
				}
			}
			return listRoles;
		}

		public void SaveRoles(RolesModel roles)
		{
			string SqlQuetion = "SaveRoles";
			
			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					var paramRole = new SqlParameter("@Role", roles.Role);
					var paramDescription = new SqlParameter("@Description", roles.Description);

					command.Parameters.Add(paramRole);
					command.Parameters.Add(paramDescription);

					command.ExecuteNonQuery();
				}
			}
		}

		public List<UserRolesModel> GetUserRoles(int userId)
		{
			string SqlQuetion = "GetUserInRoles";
			var listUserRoles = new List<UserRolesModel>();
			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}
				
				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					var UserIdParam = new SqlParameter("@UserId", userId);
					command.Parameters.Add(UserIdParam);
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var userRoles = new UserRolesModel();
							userRoles.Id = reader.GetInt32(0);
							userRoles.UserId = reader.GetInt32(1);
							userRoles.RoleId = reader.GetInt32(2);
							listUserRoles.Add(userRoles);
						}
					}
				}
			}
			return listUserRoles;
		}

		public List<UserRolesModel> SaveUserRoles(int userId, List<int> roles)
		{
			var listUserRolesModel= new List<UserRolesModel>();
			string SqlQuetion = "SaveUserInRoles";
			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				DataTable dataTable = new DataTable();
				dataTable.Columns.Add(new DataColumn("RoleId", typeof(int)));
				roles.ForEach(role => dataTable.Rows.Add(role));

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter UserIdParam = new SqlParameter("@UserId", userId);
					command.Parameters.Add(UserIdParam);
					SqlParameter rolesParam = new SqlParameter("@Roles", SqlDbType.Structured)
					{
						Value = dataTable,
						TypeName = "dbo.IntTable"
					};
					command.Parameters.Add(rolesParam);
					command.ExecuteNonQuery();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var userRolesModel = new UserRolesModel();
							userRolesModel.Id = reader.GetInt32(0);
							userRolesModel.UserId = reader.GetInt32(1);
							userRolesModel.RoleId = reader.GetInt32(2);
							listUserRolesModel.Add(userRolesModel);
						}
					}
				}
			}
			return listUserRolesModel;
		}

		public List<UserModel> GetUserPage(string FindName, int @pageNumber, int rowsOfPage, out double totalUsers)
		{
			string SqlQuetion = "GetUserPage";

			var listUserModel = new List<UserModel>();

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					var paramstartRows = new SqlParameter("@pageIndex", pageNumber * rowsOfPage);
					command.Parameters.Add(paramstartRows);
					var paramRowsOfPage = new SqlParameter("@rowsIndex", rowsOfPage);
					command.Parameters.Add(paramRowsOfPage);

					if (FindName == "")
						FindName = null;
					bool optionSearch = Int32.TryParse(FindName, out int result);

					var paramFindName = new SqlParameter("@FindName", FindName);
					command.Parameters.Add(paramFindName);
					var paramOptionSearch = new SqlParameter("@optionSearch", optionSearch);
					command.Parameters.Add(paramOptionSearch);

					var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Float) { Direction = ParameterDirection.ReturnValue };
					command.Parameters.Add(returnValue);


					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var userModel = new UserModel();
							userModel.Id = reader.GetInt32(0);
							userModel.Name = reader.GetString(1);
							userModel.Email = reader.GetString(2);
							userModel.Password = reader.GetString(3);
							userModel.CreatedDate = reader.GetDateTime(4);
							userModel.UpdatedDate = reader.GetDateTime(5);
							//userModel.UserRolesModels = GetUserRoles(userModel.Id);
							listUserModel.Add(userModel);
						}
					}
					totalUsers = Convert.ToDouble(returnValue.Value);
				}
			}
			return listUserModel;
		}

		//public List<RolesModel> GetRoles()
		//{
		//	string SqlQuetion = "GetRoles";
		//	var listUserRolesModel = new List<RolesModel>();

		//	using (var connection = new SqlConnection(connectionLine))
		//	{
		//		if (connection.State != ConnectionState.Open)
		//		{
		//			connection.Open();
		//		}

		//		using (var command = new SqlCommand(SqlQuetion, connection))
		//		{
		//			command.CommandType = CommandType.StoredProcedure;

		//			using (var reader = command.ExecuteReader())
		//			{
		//				while (reader.Read())
		//				{
		//					var RolesModel = new RolesModel();
		//					RolesModel.Id = reader.GetInt32(0);
		//					RolesModel.Roles = reader.GetString(1);
		//					RolesModel.Description = reader.GetString(2);
		//					listUserRolesModel.Add(RolesModel);
		//				}
		//			}
		//		}
		//	}
		//	return listRolesModel;
		//}

		public static UserModel CurrentUser()
		{
			var usersDataProvider = new UsersDataProvider();
			var valueUser = usersDataProvider.GetByIdUser(CookiesUser.CookiesGet()._Id);
			if (HttpContext.Current.Request.Cookies.AllKeys.Contains("cookieUser"))
				if (CookiesUser.CookiesGet() != null)
					if (valueUser.Name == CookiesUser.CookiesGet()._Name)
						return new UserModel() {
							Id = CookiesUser.CookiesGet()._Id,
							Name = valueUser.Name,
							Email = valueUser.Email,
							UserRolesModels = usersDataProvider.GetUserRoles(CookiesUser.CookiesGet()._Id)
						};
					else return null;
			return null;
		}

		public UserModel GetByIdUser(int Id)
		{
			string SqlQuetion = "GetByIdUser";
			var userModel = new UserModel();

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					var paramId = new SqlParameter("@Id", Id);
					command.Parameters.Add(paramId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							userModel.Name = reader.GetString(0);
							userModel.Email = reader.GetString(1);
						}
					}
				}
			}
			return userModel;
		}

		public UserModel Login(string Email, string Password, out bool checkLogin)
		{
			string SqlQuetion = "LoginUser";

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							if (reader.GetString(2) == Email && reader.GetString(3) == Password)
							{
								checkLogin = true;
								return new UserModel()
								{
									Id = reader.GetInt32(0),
									Name = reader.GetString(1),
									Email = reader.GetString(2)
								};
							}
						}
					}
				}
			}
			checkLogin = false;
			return new UserModel();
		}

		public List<UserModel> Get()
		{
			string SqlQuetion = "GetUser";
			var listUserModel = new List<UserModel>();

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var userModel = new UserModel();
							userModel.Id = reader.GetInt32(0);
							userModel.Name = reader.GetString(1);
							userModel.Email = reader.GetString(2);
							userModel.Password = reader.GetString(3);
							userModel.CreatedDate = reader.GetDateTime(4);
							userModel.UpdatedDate = reader.GetDateTime(5);
							listUserModel.Add(userModel);
						}
					}
				}
			}
			return listUserModel;
		}

		public bool Delete(int Id)
		{
			string SqlQuetion = "DeleteUser";

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					var paramId = new SqlParameter("@Id", Id);
					command.Parameters.Add(paramId);
					if (command.ExecuteNonQuery() > 0)
						return true;
				}
			}
			return false;
		}

		public UserModel Update(UserModel userModel)
		{
			string SqlQuetion = "UpdateUser";

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					var paramId = new SqlParameter("@Id", userModel.Id);
					command.Parameters.Add(paramId);

					var paramName = new SqlParameter("@Name", userModel.Name);
					command.Parameters.Add(paramName);

					var paramEmail = new SqlParameter("@Email", userModel.Email);
					command.Parameters.Add(paramEmail);

					var paramPassword = new SqlParameter("@Password", userModel.Password);
					command.Parameters.Add(paramPassword);

					var paramUpdatedDate = new SqlParameter("@UpdatedDate", DateTime.Now);
					command.Parameters.Add(paramUpdatedDate);
					command.ExecuteNonQuery();
				}
			}
			return userModel;
		}

		public UserModel Create(string Name, string Email, string Password)
		{
			string SqlQuetion = "CreateUser";
			UserModel userModel;

			using (var connection = new SqlConnection(connectionLine))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				using (var command = new SqlCommand(SqlQuetion, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					var paramName = new SqlParameter("@Name", Name);
					var paramEmail = new SqlParameter("@Email", Email);
					var paramPassword = new SqlParameter("@Password", Password);
					var paramCreatedDate = new SqlParameter("@CreatedDate", DateTime.Now);
					var paramUpdatedDate = new SqlParameter("@UpdatedDate", DateTime.Now);

					command.Parameters.Add(paramName);
					command.Parameters.Add(paramEmail);
					command.Parameters.Add(paramPassword);
					command.Parameters.Add(paramCreatedDate);
					command.Parameters.Add(paramUpdatedDate);

					command.ExecuteNonQuery();

					userModel = new UserModel()
					{
						Name = Name,
						Email = Email,
						Password = Password,
						CreatedDate = DateTime.Now,
						UpdatedDate = DateTime.Now
					};
				}
			}
			return userModel;
		}
	}
}