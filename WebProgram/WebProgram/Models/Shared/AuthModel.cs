﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDataProvider;
using WebProgram.Models.Users;

namespace WebProgram.Models.Shared
{
	public class AuthModel
	{
		public UserModel CurrentUser { get; set; }
	}
}