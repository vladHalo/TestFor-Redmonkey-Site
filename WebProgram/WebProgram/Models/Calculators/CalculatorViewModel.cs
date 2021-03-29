namespace WebProgram.Models.Calculators
{
	public class CalculatorViewModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }

		public double Result { get; set; }
		public string Operation { get; set; }
		public string Number { get; set; }
		public string Digit { get; set; }

		public CalculatorViewModel()
		{
			Id = -1;
			UserId = -1;
			Result = 0;
			Operation = "";
			Number = "";
			Digit = "-1";
		}
	}
}