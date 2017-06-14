using System.Collections.Generic;
using System.Linq;

namespace DartBot
{
	public class DartBoard
	{
		private static DartBoard dartBoard;
		private DartBoard()
		{
			this.Fields = new List<Field>();
			this.Fields.Add(new Field(0, FieldEnum.Outside));
			for (int i = 1; i <= 20; i++)
			{
				this.Fields.Add(new Field(i,FieldEnum.SingleOut));
				this.Fields.Add(new Field(i, FieldEnum.SingleIn));
				this.Fields.Add(new Field(i*2, FieldEnum.Double));
				this.Fields.Add(new Field(i*3, FieldEnum.Triple));
			}
			this.Fields.Add(new Field(25, FieldEnum.SingleBull));
			this.Fields.Add(new Field(50, FieldEnum.DoubleBull));
			foreach (var field in this.Fields.Where(x => x.Type == FieldEnum.Double))
			{
				field.Neighbours.Add(GetOutside());
				field.Neighbours.Add(GetSingleOutField(field.Value/2));
			}
			foreach (var field in this.Fields.Where(x=> x.Type == FieldEnum.SingleOut))
			{
				field.Neighbours.Add(GetDoubleField(field.Value*2));
				field.Neighbours.Add(GetTripleField(field.Value*3));
			}
			foreach (var field in this.Fields.Where(x => x.Type == FieldEnum.Triple))
			{
				field.Neighbours.Add(GetSingleOutField(field.Value/3));
				field.Neighbours.Add(GetSingleInField(field.Value/3));
			}
			foreach (var field in this.Fields.Where(x => x.Type == FieldEnum.SingleIn))
			{
				field.Neighbours.Add(GetTripleField(field.Value * 3));
				field.Neighbours.Add(GetSingleBull());
				GetSingleBull().Neighbours.Add(field);
			}
			GetSingleBull().Neighbours.Add(GetDoubleBull());
			GetDoubleBull().Neighbours.Add(GetSingleBull());

			var order = new List<int>();
			order.Add(20);
			order.Add(1);
			order.Add(18);
			order.Add(4);
			order.Add(13);
			order.Add(6);
			order.Add(10);
			order.Add(15);
			order.Add(2);
			order.Add(17);
			order.Add(3);
			order.Add(19);
			order.Add(7);
			order.Add(16);
			order.Add(8);
			order.Add(11);
			order.Add(14);
			order.Add(9);
			order.Add(12);
			order.Add(5);

			for (int i = 0; i < order.Count; i++)
			{
				if (i > 0)
				{
					GetSingleOutField(order[i]).Neighbours.Add(GetSingleOutField(order[i-1]));
					GetSingleInField(order[i]).Neighbours.Add(GetSingleInField(order[i - 1]));
					GetDoubleField(order[i]*2).Neighbours.Add(GetDoubleField(order[i - 1]*2));
					GetTripleField(order[i]*3).Neighbours.Add(GetTripleField(order[i - 1]*3));
				}
				else
				{
					GetSingleOutField(order[i]).Neighbours.Add(GetSingleOutField(order[order.Count-1]));
					GetSingleInField(order[i]).Neighbours.Add(GetSingleInField(order[order.Count - 1]));
					GetDoubleField(order[i]*2).Neighbours.Add(GetDoubleField(order[order.Count - 1]*2));
					GetTripleField(order[i]*3).Neighbours.Add(GetTripleField(order[order.Count - 1]*3));
				}
				if (i < order.Count-1)
				{
					GetSingleOutField(order[i]).Neighbours.Add(GetSingleOutField(order[i + 1]));
					GetSingleInField(order[i]).Neighbours.Add(GetSingleInField(order[i + 1]));
					GetDoubleField(order[i]*2).Neighbours.Add(GetDoubleField(order[i + 1]*2));
					GetTripleField(order[i]*3).Neighbours.Add(GetTripleField(order[i + 1]*3));
				}
				else
				{
					GetSingleOutField(order[i]).Neighbours.Add(GetSingleOutField(order[0]));
					GetSingleInField(order[i]).Neighbours.Add(GetSingleInField(order[0]));
					GetDoubleField(order[i]*2).Neighbours.Add(GetDoubleField(order[0]*2));
					GetTripleField(order[i]*3).Neighbours.Add(GetTripleField(order[0]*3));
				}
			}
		}

		public Field GetOutside()
		{
			return this.Fields.First(x => x.Type == FieldEnum.Outside);
		}

		public static DartBoard GetInstance()
		{
			return dartBoard ?? (dartBoard = new DartBoard());
		}

		public List<Field> Fields { get; set; }

		public Field GetDoubleBull()
		{
			return this.Fields.First(x => x.Type == FieldEnum.DoubleBull);
		}
		public Field GetSingleBull()
		{
			return this.Fields.First(x => x.Type == FieldEnum.SingleBull);
		}

		public Field GetSingleInField(int i)
		{
			return this.Fields.First(x => x.Type == FieldEnum.SingleIn && x.Value == i);
		}

		public Field GetSingleOutField(int i)
		{
			return this.Fields.First(x => x.Type == FieldEnum.SingleOut && x.Value == i);
		}

		public Field GetDoubleField(int i)
		{
			return this.Fields.FirstOrDefault(x => x.Type == FieldEnum.Double && x.Value == i);
		}

		public Field GetTripleField(int i)
		{
			return this.Fields.First(x => x.Type == FieldEnum.Triple && x.Value == i);
		}
	}
}
