using Newtonsoft.Json;
using System;
using System.Web;

namespace UsersDataProvaider
{
	public class CookieData
	{
		public int _Id { get; set; }
		public string _Name { get; set; }
		public string _Email { get; set; }
	}

	public class CookiesUser
	{
		public static void CookiesSet(int Id,string Name,string Email)
		{
			string [] allCookie = HttpContext.Current.Request.Cookies.AllKeys;
			foreach(var i in allCookie)
			{
				var cookieId = HttpContext.Current.Response.Cookies.Get(i);
				cookieId.Expires = DateTime.Now.AddDays(-1);
			}
			CookieData cookieData = new CookieData()
			{
				_Id = Id,
				_Name = Name,
				_Email = Email,
			};
			string returnValue = AesOperation.EncryptString(JsonConvert.SerializeObject(cookieData));

			var cookieUser = new HttpCookie("cookieUser",returnValue);
			cookieUser.Expires = DateTime.Now.AddHours(1);
			HttpContext.Current.Response.Cookies.Add(cookieUser);
		} 

		public static CookieData CookiesGet()
		{
			CookieData cookieData = new CookieData();
			var cookieUser = HttpContext.Current.Request.Cookies.Get("cookieUser");
			string cookieValue=null;
			if (cookieUser != null)
			{
				cookieValue = cookieUser.Value;
			}
			if (!string.IsNullOrEmpty(cookieValue))
			{
				try
				{
					string returnValue = AesOperation.DecryptString(cookieValue);
					cookieData = JsonConvert.DeserializeObject<CookieData>(returnValue);
				}
				catch { cookieData=null; }
			}
			return cookieData;
		}

		public static void CookiesDelete()
		{
			var cookieId=HttpContext.Current.Response.Cookies.Get("cookieUser");
			cookieId.Expires = DateTime.Now.AddDays(-1);
		}
	}
}