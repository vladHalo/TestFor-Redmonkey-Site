using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using UsersDataProvider;
using WebProgram.Models;
using WebProgram.Models.Calculators;
using WebProgram.Models.Shared;

namespace WebProgram.Controllers
{
	public class CalculatorsController : Controller
	{
		public ActionResult Index()
		{
			var calcDataProv = new CalculatorDataProviders();
			var calcLists = new List<CalculatorViewModel>();
			var model = new IndexViewModel();

			foreach (var calc in calcDataProv.GetByIdCalc(CookiesUser.CookiesGet()._Id))
				calcLists.Add(JsonConvert.DeserializeObject<CalculatorViewModel>(calc.JsonModel));

			model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
			model.ListCalcViewModel = calcLists;
			return View(model);
		}

		//public JsonResult CalcListJson(string numbOrOper)
		//{
		//	var model = new IndexViewModel();
		//	var calcDataProv = new CalculatorDataProviders();
		//	var listCalcUser = calcDataProv.GetByIdCalc(CookiesUser.CookiesGet()._Id);

		//	for (int i= 0; i< listCalcUser.Count;i++)
		//	{	
		//		if (listCalcUser[i].Id == indexViewModel.UserId)
		//		{
		//			CalcLogic calcLogic = JsonConvert.DeserializeObject<CalcLogic>(listCalcUser[i].JsonModel);
		//			calcLogic.Resulting(indexViewModel.Digit, indexViewModel.Operation);

		//			var calcLogicUpdate = JsonConvert.SerializeObject(calcLogic);
		//			calcDataProv.UpdateCalc(listCalcUser[i].Id, calcLogicUpdate);
		//		}
		//	}

		//	var listCalc = new List<CalculatorViewModel>();
		//	foreach (var i in calcDataProv.GetByIdCalc(CookiesUser.CookiesGet()._Id))
		//	{
		//		CalcLogic calcLogic = JsonConvert.DeserializeObject<CalcLogic>(i.JsonModel);
		//		listCalc.Add(new CalculatorViewModel() { Number = calcLogic.Number, Result = calcLogic.Result });
		//	}
		//	model.CurrentUser = UsersDataProvider.UsersDataProvider.CurrentUser();
		//	model.ListCalcViewModel = listCalc;
		//	return View(model);
		//}

		public JsonResult AddCalcJson()
		{
			var calcDataProv = new CalculatorDataProviders();
			var calcViewModel = new CalculatorViewModel()
			{
				Id= calcDataProv.GetIdentCalc()+1,
				UserId = CookiesUser.CookiesGet()._Id
			};
			var convertCalc = JsonConvert.SerializeObject(calcViewModel);
			calcDataProv.CreateCalc(CookiesUser.CookiesGet()._Id, convertCalc);
			BaseResponse baseResponse = new BaseResponse(calcViewModel);
			return Json(baseResponse);
		}

		public JsonResult RemoveCalcJson(int id)
		{
			var calcDataProv = new CalculatorDataProviders();
			BaseResponse baseResponse = new BaseResponse(calcDataProv.DeleteCalc(id));
			return Json(baseResponse);
		}
	}
}