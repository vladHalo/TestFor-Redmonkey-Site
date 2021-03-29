using WebProgram.Models;

namespace WebProgram.Models.Calculators
{
	public class CalcLogic
	{
		public double Result;
		public string Operation;
		public string Number;
		public string Digit;
		string backOperation;

		public void Oper()
		{
			switch (backOperation)
			{
				case "Add":
					Add();
					break;
				case "Min":
					Minus();
					break;
				case "Mul":
					Multiplication();
					break;
				case "Div":
					Division();
					break;
				//case "Clear":
				//	Clear();
				//	break;
				default:
					Result = double.Parse(Number);
					Number = "";
					break;
			}
			backOperation = Operation;
		}

		void Add()
		{
			//if (string.IsNullOrEmpty(Number)) 
			//{
				Result += double.Parse(Number);
				Number = "";
			//}
		}

		void Minus()
		{
			Result -= double.Parse(Number);
			Number = "";
		}

		void Multiplication()
		{
			Result *= double.Parse(Number);
			Number = "";
		}

		void Division()
		{
			Result /= double.Parse(Number);
			Number = "";
		}

		public void Resulting(string digit, string operation)
		{
			if (digit == "-1")
			{
				Operation = operation;
				Oper();
			}
			else
			{
				Digit = digit;
				Number += Digit;
			}
		}
		void Clear()
		{
			Result = 0;
			Number = "";
		}
	}
}