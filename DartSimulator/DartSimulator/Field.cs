using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public enum FieldEnum
	{
		SingleIn,
		SingleOut,
		Double,
		Triple,
		SingleBull,
		DoubleBull,
		Outside
	}

	public class Field
	{
		//private double doubleHitQuote;
		public Field(int value, FieldEnum type)
		{
			this.Value = value;
			this.Type = type;
			this.Neighbours = new List<Field>();
		}
		public int Value { get; set; }
		public FieldEnum Type { get; set; }
		public List<Field> Neighbours { get; set; }
		//public double HitQuote => GetHitQuote();

		//private double GetHitQuote()
		//{
		//	if (this.doubleHitQuote <= 0.0)
		//		return 0.0;
		//	var calculated = this.doubleHitQuote/Measurements.GetFactor(FieldEnum.Double) * Measurements.GetFactor(this.Type);
		//	return calculated > 95 ? 95 : calculated;
		//}

		public override string ToString()
		{
			switch (Type)
			{
				case FieldEnum.Double:
				case FieldEnum.DoubleBull:
					return "D" + (Value/2).ToString();
				case FieldEnum.Triple:
					return "T" + (Value/3).ToString();
				default:
					return Value.ToString();
			}
		}
	}
}
