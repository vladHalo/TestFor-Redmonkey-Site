using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDataProvider;
using WebProgram.Models.Users;

namespace WebProgram.Models.Calculators
{
	public class IndexViewModel
	{
		public UserModel CurrentUser { get; set; }
		public List<CalculatorViewModel> ListCalcViewModel {get;set;}
	}
}