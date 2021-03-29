using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgram.Models.Calculators
{
	public class CalculatorDataModel
	{
		public int Id { get; set; }
		//public int UserId { get; set; }
		public string JsonModel { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}