using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgram.Models.Shared
{
	public class BaseResponse
	{
		public int StatusCode { get; set; }
		public string StatusMessage { get; set; }
		public object Data { get; set; }

		public BaseResponse(object data,int statusCode=200, string statusMessage="success")
		{
			StatusCode = statusCode;
			StatusMessage = statusMessage;
			Data = data;
		}
	}
}