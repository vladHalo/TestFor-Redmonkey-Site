using System;
using System.Collections.Generic;
using UsersDataProvider;

namespace Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var usersDataProvider = new UsersDataProvider.UsersDataProvider();
			List<RolesModel> rolesModel = new List<RolesModel>()
			{
				new RolesModel() { Id = 1, Role = "1", Description = "1" },
				new RolesModel() { Id = 2, Role = "2", Description = "2" },
				new RolesModel() { Id = 3, Role = "3", Description = "3" },
				new RolesModel() { Id = 4, Role = "4", Description = "4" },
				new RolesModel() { Id = 5, Role = "5", Description = "5" }
			};
			usersDataProvider.SaveRoles(new RolesModel() { Role = "Vlad", Description = "1" });//.ForEach(i =>System.Console.WriteLine($"{i.Id} {i.Role} {i.Description}"));
			System.Console.ReadKey();
		}
	}
}
