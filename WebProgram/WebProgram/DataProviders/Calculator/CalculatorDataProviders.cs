using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebProgram.Models.Calculators;

namespace WebProgram.Models
{
	public class CalculatorDataProviders
	{
		//static string connectionLine = "Data Source=tcp:ondrashdev.database.windows.net,1433;Initial Catalog=TestDb;" +
		//		"Persist Security Info=True; User ID=ondrashdev;Password=Is=4820017000025";
		static string connectionLine = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Calculator;Integrated Security=True";

		public int GetIdentCalc()
		{
			string nameStorePr = "GetIdentCalc";
			using (SqlConnection connection = new SqlConnection(connectionLine))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(nameStorePr, connection);
				command.CommandType = CommandType.StoredProcedure;
				return Convert.ToInt32(command.ExecuteScalar());
			}
		}

		public List<CalculatorDataModel> GetByIdCalc(int UserId)
		{
			string nameStorePr = "GetByIdCalc";
			CalculatorDataModel modelData;
			List<CalculatorDataModel> modelDatas = new List<CalculatorDataModel>();

			using (SqlConnection connection = new SqlConnection(connectionLine))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(nameStorePr, connection);
				command.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUserId = new SqlParameter("@UserId", UserId);
				command.Parameters.Add(paramUserId);

				var executeReader = command.ExecuteReader();
				while (executeReader.Read())
				{
					modelData = new CalculatorDataModel();
					modelData.Id = executeReader.GetInt32(0);
					modelData.JsonModel = executeReader.GetString(2);
					modelData.CreatedDate = executeReader.GetDateTime(3);
					modelData.UpdatedDate = executeReader.GetDateTime(4);
					modelDatas.Add(modelData);
				}
				return modelDatas;
			}
		}

		public void CreateCalc(int UserId, string JsonModel)
		{
			string nameStorePr = "CreateCalc";
			using (SqlConnection connection = new SqlConnection(connectionLine))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(nameStorePr, connection);
				command.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUserId = new SqlParameter("@UserId", UserId);
				SqlParameter paramJsonBackend = new SqlParameter("@JsonModel", JsonModel);
				SqlParameter paramCreatedDate = new SqlParameter("@CreatedDate", DateTime.Now);
				SqlParameter paramUpdatedDate = new SqlParameter("@UpdatedDate", DateTime.Now);

				command.Parameters.Add(paramUserId);
				command.Parameters.Add(paramJsonBackend);
				command.Parameters.Add(paramCreatedDate);
				command.Parameters.Add(paramUpdatedDate);

				command.ExecuteNonQuery();
			}
		}

		public void UpdateCalc(int Id, string JsonModel)
		{
			string nameStorePr = "UpdateCalc";
			using (SqlConnection connection = new SqlConnection(connectionLine))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(nameStorePr, connection);
				command.CommandType = CommandType.StoredProcedure;

				SqlParameter paramId = new SqlParameter("@Id", Id);
				SqlParameter paramJsonModel = new SqlParameter("@JsonModel", JsonModel);
				SqlParameter paramUpdatedDate = new SqlParameter("@UpdatedDate", DateTime.Now);

				command.Parameters.Add(paramId);
				command.Parameters.Add(paramJsonModel);
				command.Parameters.Add(paramUpdatedDate);

				command.ExecuteNonQuery();
			}
		}

		public bool DeleteCalc(int Id)
		{
			string nameStorePr = "DeleteCalc";
			using (SqlConnection connection = new SqlConnection(connectionLine))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(nameStorePr, connection);
				command.CommandType = CommandType.StoredProcedure;

				SqlParameter paramId = new SqlParameter("@Id", Id);
				command.Parameters.Add(paramId);

				if (command.ExecuteNonQuery() > 0)
					return true;
			}
			return false;
		}
	}
}