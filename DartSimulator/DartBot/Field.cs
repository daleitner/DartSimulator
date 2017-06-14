using System.Collections.Generic;

namespace DartBot
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
		public Field(int value, FieldEnum type)
		{
			this.Value = value;
			this.Type = type;
			this.Neighbours = new List<Field>();
		}
		public int Value { get; set; }
		public FieldEnum Type { get; set; }
		public List<Field> Neighbours { get; set; }

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
