using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DartBot.Common;

namespace DartBot
{
	public class DartBoard
	{
		private static DartBoard dartBoard;
		private DartBoard()
		{
			this.Fields = new List<Field>();
			var bounce = new Field(0, FieldEnum.Outside) {Target = new Point(0, 0)};
			this.Fields.Add(bounce);
			for (int i = 1; i <= 20; i++)
			{
				this.Fields.Add(new Field(i,FieldEnum.SingleOut){Target = SingleDictionary[i]});
				this.Fields.Add(new Field(i, FieldEnum.SingleIn) { Target = SingleInDictionary[i] });
				this.Fields.Add(new Field(i*2, FieldEnum.Double) { Target = DoubleDictionary[i] });
				this.Fields.Add(new Field(i*3, FieldEnum.Triple) { Target = TripleDictionary[i] });
			}

			var singleBull = new Field(25, FieldEnum.SingleBull) {Target = new Point(170, 180)};
			var doubleBull = new Field(50, FieldEnum.DoubleBull) { Target = new Point(170, 170) };
			this.Fields.Add(singleBull);
			this.Fields.Add(doubleBull);
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

			for (int i = 0; i < Fields.Count; i++)
			{
				Fields[i].Color = Color.FromArgb(255, 200 - i, 0, 200 - i);
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

		public Field GetFieldByColor(Color index)
		{
			return Fields.SingleOrDefault(x => x.Color.IsEquivalentTo(index));
		}

		private static Dictionary<int, Point> SingleDictionary = new Dictionary<int, Point>
		{
			{1, new Point(210,40) },
			{2, new Point(250,280) },
			{3, new Point(170,300) },
			{4, new Point(280,90) },
			{5, new Point(130,40) },
			{6, new Point(300,170) },
			{7, new Point(90,280) },
			{8, new Point(40,210) },
			{9, new Point(60,90) },
			{10, new Point(300,210) },
			{11, new Point(40,170) },
			{12, new Point(90,60) },
			{13, new Point(300,130) },
			{14, new Point(40,130) },
			{15, new Point(280,250) },
			{16, new Point(60,250) },
			{17, new Point(210,300) },
			{18, new Point(250,60) },
			{19, new Point(130,300) },
			{20, new Point(170,35) },
		};

		private static Dictionary<int, Point> DoubleDictionary = new Dictionary<int, Point>
		{
			{1, new Point(221,12) },
			{2, new Point(267,303) },
			{3, new Point(170,336) },
			{4, new Point(304,72) },
			{5, new Point(121,11) },
			{6, new Point(336,170) },
			{7, new Point(72,303) },
			{8, new Point(13,222) },
			{9, new Point(37,72) },
			{10, new Point(327,220) },
			{11, new Point(4,170) },
			{12, new Point(72,36) },
			{13, new Point(327,120) },
			{14, new Point(13,118) },
			{15, new Point(304,264) },
			{16, new Point(35,264) },
			{17, new Point(221,327) },
			{18, new Point(267,36) },
			{19, new Point(119,327) },
			{20, new Point(170,4) },
		};

		private static Dictionary<int, Point> TripleDictionary = new Dictionary<int, Point>
		{
			{1, new Point(201,72) },
			{2, new Point(230,252) },
			{3, new Point(170,273) },
			{4, new Point(252,109) },
			{5, new Point(139,72) },
			{6, new Point(272,170) },
			{7, new Point(109,252) },
			{8, new Point(73,202) },
			{9, new Point(88,108) },
			{10, new Point(267,201) },
			{11, new Point(67,170) },
			{12, new Point(110,86) },
			{13, new Point(267,137) },
			{14, new Point(73,137) },
			{15, new Point(254,228) },
			{16, new Point(86,229) },
			{17, new Point(202,267) },
			{18, new Point(230,87) },
			{19, new Point(137,266) },
			{20, new Point(170,67) },
		};

		private static Dictionary<int, Point> SingleInDictionary = new Dictionary<int, Point>
		{
			{1, new Point(193,97) },
			{2, new Point(217,232) },
			{3, new Point(170,246) },
			{4, new Point(232,125) },
			{5, new Point(148,95) },
			{6, new Point(245,170) },
			{7, new Point(120,237) },
			{8, new Point(100,191) },
			{9, new Point(108,123) },
			{10, new Point(241,192) },
			{11, new Point(92,170) },
			{12, new Point(125,105) },
			{13, new Point(240,147) },
			{14, new Point(98,145) },
			{15, new Point(232,214) },
			{16, new Point(106,216) },
			{17, new Point(195,245) },
			{18, new Point(216,106) },
			{19, new Point(145,243) },
			{20, new Point(170,96) },
		};
	}
}
