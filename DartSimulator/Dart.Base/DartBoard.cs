using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dart.Base
{
	public class DartBoard
	{
		private List<List<Color>> _coloredPixels;
		private static DartBoard _dartBoard;

		#region initialize
		private DartBoard()
		{
			InitializeFields();
			InitializePixels();
		}

		private void InitializeFields()
		{
			//create fields and set targets
			Fields = new List<Field>();
			var bounce = new Field(0, FieldEnum.Outside) {Target = new Point(170, 170)};
			Fields.Add(bounce);
			for (int i = 1; i <= 20; i++)
			{
				Fields.Add(new Field(i, FieldEnum.SingleOut) {Target = SingleDictionary[i]});
				Fields.Add(new Field(i, FieldEnum.SingleIn) {Target = SingleInDictionary[i]});
				Fields.Add(new Field(i * 2, FieldEnum.Double) {Target = DoubleDictionary[i]});
				Fields.Add(new Field(i * 3, FieldEnum.Triple) {Target = TripleDictionary[i]});
			}

			var singleBull = new Field(25, FieldEnum.SingleBull) {Target = new Point(0, -10)};
			var doubleBull = new Field(50, FieldEnum.DoubleBull) {Target = new Point(0, 0)};
			Fields.Add(singleBull);
			Fields.Add(doubleBull);

			//set left/right neighbors
			GetSingleBull().LeftNeighbor = GetDoubleBull();
			GetDoubleBull().RightNeighbor = GetSingleBull();

			var order = new List<int>
			{
				20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5
			};

			for (int i = 0; i < order.Count; i++)
			{
				if (i > 0)
				{
					GetSingleOutField(order[i]).LeftNeighbor = GetSingleOutField(order[i - 1]);
					GetSingleInField(order[i]).LeftNeighbor = GetSingleInField(order[i - 1]);
					GetDoubleField(order[i] * 2).LeftNeighbor = GetDoubleField(order[i - 1] * 2);
					GetTripleField(order[i] * 3).LeftNeighbor = GetTripleField(order[i - 1] * 3);
				}
				else
				{
					GetSingleOutField(order[i]).LeftNeighbor = GetSingleOutField(order[order.Count - 1]);
					GetSingleInField(order[i]).LeftNeighbor = GetSingleInField(order[order.Count - 1]);
					GetDoubleField(order[i] * 2).LeftNeighbor = GetDoubleField(order[order.Count - 1] * 2);
					GetTripleField(order[i] * 3).LeftNeighbor = GetTripleField(order[order.Count - 1] * 3);
				}

				if (i < order.Count - 1)
				{
					GetSingleOutField(order[i]).RightNeighbor = GetSingleOutField(order[i + 1]);
					GetSingleInField(order[i]).RightNeighbor = GetSingleInField(order[i + 1]);
					GetDoubleField(order[i] * 2).RightNeighbor = GetDoubleField(order[i + 1] * 2);
					GetTripleField(order[i] * 3).RightNeighbor = GetTripleField(order[i + 1] * 3);
				}
				else
				{
					GetSingleOutField(order[i]).RightNeighbor = GetSingleOutField(order[0]);
					GetSingleInField(order[i]).RightNeighbor = GetSingleInField(order[0]);
					GetDoubleField(order[i] * 2).RightNeighbor = GetDoubleField(order[0] * 2);
					GetTripleField(order[i] * 3).RightNeighbor = GetTripleField(order[0] * 3);
				}
			}

			//set colors
			for (int i = 0; i < Fields.Count; i++)
			{
				Fields[i].Color = Color.FromArgb(255, 200 - i, 0, 200 - i);
			}
		}
		private void InitializePixels()
		{
			var canvas = new CanvasUserControl();
			_coloredPixels = canvas.GetColoredImage(Fields);
		}
		#endregion

		/// <summary>
		/// Singleton Instance of Dartboard
		/// </summary>
		public static DartBoard Instance => _dartBoard ?? (_dartBoard = new DartBoard());

		#region Get Fields
		private List<Field> Fields { get; set; }
		/// <summary>
		/// Get Field with value 0
		/// </summary>
		/// <returns></returns>
		public Field GetOutside()
		{
			return Fields.First(x => x.Type == FieldEnum.Outside);
		}

		/// <summary>
		/// Returns Double Bull Field.
		/// </summary>
		/// <returns></returns>
		public Field GetDoubleBull()
		{
			return Fields.First(x => x.Type == FieldEnum.DoubleBull);
		}

		/// <summary>
		/// Returns Single Bull Field.
		/// </summary>
		/// <returns></returns>
		public Field GetSingleBull()
		{
			return Fields.First(x => x.Type == FieldEnum.SingleBull);
		}

		/// <summary>
		/// Returns Single-In Field with corresponding value.
		/// </summary>
		/// <param name="value">Value of Field.</param>
		/// <returns></returns>
		public Field GetSingleInField(int value)
		{
			return Fields.First(x => x.Type == FieldEnum.SingleIn && x.Value == value);
		}

		/// <summary>
		/// Returns Single-Out Field with corresponding value.
		/// </summary>
		/// <param name="value">Value of Field.</param>
		/// <returns></returns>
		public Field GetSingleOutField(int value)
		{
			return Fields.First(x => x.Type == FieldEnum.SingleOut && x.Value == value);
		}

		/// <summary>
		/// Returns Double Field with corresponding value.
		/// </summary>
		/// <param name="value">Value of Field.</param>
		/// <returns></returns>
		public Field GetDoubleField(int value)
		{
			return Fields.FirstOrDefault(x => x.Type == FieldEnum.Double && x.Value == value);
		}

		/// <summary>
		/// Returns Triple Field with corresponding value.
		/// </summary>
		/// <param name="value">Value of Field.</param>
		/// <returns></returns>
		public Field GetTripleField(int value)
		{
			return Fields.First(x => x.Type == FieldEnum.Triple && x.Value == value);
		}

		/// <summary>
		/// returns Field which is on Position [X,Y]. [0,0] is the middle of the dartboard.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Field GetFieldByXY(int x, int y)
		{
			var target = TargetToPixelCoords(new Point(x, y), _coloredPixels.First().Count, _coloredPixels.Count);
			var result = GetFieldByColor(_coloredPixels[target.Y][target.X]);
			if (result != null)
				return result;

			var points = new List<Point>
			{
				new Point(target.X + 1, target.Y),
				new Point(target.X - 1, target.Y),
				new Point(target.X, target.Y + 1),
				new Point(target.X, target.Y - 1),
				new Point(target.X + 2, target.Y),
				new Point(target.X - 2, target.Y),
				new Point(target.X, target.Y + 2),
				new Point(target.X, target.Y - 2)
			};
			foreach (var point in points)
			{
				result = GetFieldByColor(_coloredPixels[point.Y][point.X]);
				if (result != null)
					return result;
			}

			return GetOutside();
			//throw new InvalidDataException("No Field found on Position [" + x + "," + y + "]! Color:" + _coloredPixels[target.Y][target.X]);
		}

		private Point TargetToPixelCoords(Point target, int width, int height)
		{
			var newX = target.X + width / 2;
			var newY = target.Y * -1 + height / 2;

			if (newX < 0)
				newX = 0;
			else if (newX >= width)
				newX = width - 1;

			if (newY < 0)
				newY = 0;
			else if (newY >= height)
				newY = height - 1;

			return new Point(newX, newY);
		}

		private Field GetFieldByColor(Color index)
		{
			return Fields.SingleOrDefault(x => x.Color.IsEquivalentTo(index));
		}

		/// <summary>
		/// Get all Fields with corresponding type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IEnumerable<Field> GetFieldsByType(FieldEnum type)
		{
			return Fields.Where(x => x.Type == type);
		}

		/// <summary>
		/// Get all Fields.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Field> GetAllFields()
		{
			return new List<Field>(Fields);
		}
		#endregion

		/// <summary>
		/// Hit a dart on Position [x,y]. This Point will be marked in Dartboard Image.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void Hit(int x, int y)
		{
			var field = GetFieldByXY(x, y);
			field.Hits.Add(new Point(x,y));
		}

		/// <summary>
		/// Clear all Hits on the dartboard.
		/// </summary>
		public void ClearDartboard()
		{
			foreach (var field in Fields)
			{
				field.ClearHits();
			}
		}

		/// <summary>
		/// returns a Bitmap Image of the dartboard where all hits are marked.
		/// </summary>
		/// <returns></returns>
		public Bitmap GetDartboardImage()
		{
			var canvas = new CanvasUserControl();
			var pixels = canvas.GetEmptyImage();
			var image = new Bitmap(pixels.First().Count, pixels.Count);
			for (int i = 0; i < pixels.Count; i++)
			{
				for (int j = 0; j < pixels[i].Count; j++)
				{
					image.SetPixel(j, i, pixels[i][j]);
				}
			}

			foreach (var field in Fields)
			{
				foreach (var hit in field.Hits)
				{
					var hitPoint = TargetToPixelCoords(hit, image.Width, image.Height);
					image.SetPixel(hitPoint.X, hitPoint.Y, Color.Green);
				}
			}
			return image;
		}

		/// <summary>
		/// apply additional values to corresponding all fields included in valueDict.
		/// </summary>
		/// <param name="valueDict"></param>
		public void ApplyAdditionalValues(Dictionary<Field, object> valueDict)
		{
			foreach (var field in Fields)
			{
				if (valueDict.ContainsKey(field))
					field.AdditionalValue = valueDict[field];
			}
		}

		#region target dictionaries
		private static readonly Dictionary<int, Point> SingleDictionary = new Dictionary<int, Point>
		{
			{1, new Point(40,130) },
			{2, new Point(80,-110) },
			{3, new Point(0,-130) },
			{4, new Point(110,80) },
			{5, new Point(-40,130) },
			{6, new Point(130,0) },
			{7, new Point(-80,-110) },
			{8, new Point(-130,-40) },
			{9, new Point(-110,80) },
			{10, new Point(130,-40) },
			{11, new Point(-130,0) },
			{12, new Point(-80,110) },
			{13, new Point(130,40) },
			{14, new Point(-130,40) },
			{15, new Point(110,-80) },
			{16, new Point(-110,-80) },
			{17, new Point(40,-130) },
			{18, new Point(80,110) },
			{19, new Point(-40,-130) },
			{20, new Point(0,135) }
		};

		private static readonly Dictionary<int, Point> DoubleDictionary = new Dictionary<int, Point>
		{
			{1, new Point(51,158) },
			{2, new Point(97,-133) },
			{3, new Point(0,-166) },
			{4, new Point(134,98) },
			{5, new Point(-49,159) },
			{6, new Point(166,0) },
			{7, new Point(-98,-133) },
			{8, new Point(-157,-52) },
			{9, new Point(-133,98) },
			{10, new Point(157,-50) },
			{11, new Point(-166,0) },
			{12, new Point(-98,134) },
			{13, new Point(157,50) },
			{14, new Point(-157,52) },
			{15, new Point(134,-94) },
			{16, new Point(-135,-94) },
			{17, new Point(51,-157) },
			{18, new Point(97,134) },
			{19, new Point(-51,-157) },
			{20, new Point(0,166) },
		};

		private static readonly Dictionary<int, Point> TripleDictionary = new Dictionary<int, Point>
		{
			{1, new Point(31,98) },
			{2, new Point(60,-82) },
			{3, new Point(0,-103) },
			{4, new Point(82,61) },
			{5, new Point(-31,98) },
			{6, new Point(102,0) },
			{7, new Point(-61,-82) },
			{8, new Point(-97,-32) },
			{9, new Point(-82,62) },
			{10, new Point(97,-31) },
			{11, new Point(-103,0) },
			{12, new Point(-60,84) },
			{13, new Point(97,33) },
			{14, new Point(-97,33) },
			{15, new Point(84,-58) },
			{16, new Point(-84,-59) },
			{17, new Point(32,-97) },
			{18, new Point(60,83) },
			{19, new Point(-33,-96) },
			{20, new Point(0,103) },
		};

		private static readonly Dictionary<int, Point> SingleInDictionary = new Dictionary<int, Point>
		{
			{1, new Point(23,73) },
			{2, new Point(47,-62) },
			{3, new Point(0,-76) },
			{4, new Point(62,45) },
			{5, new Point(-22,75) },
			{6, new Point(75,0) },
			{7, new Point(-50,-67) },
			{8, new Point(-70,-21) },
			{9, new Point(-62,47) },
			{10, new Point(71,-22) },
			{11, new Point(-78,0) },
			{12, new Point(-45,65) },
			{13, new Point(70,23) },
			{14, new Point(-72,25) },
			{15, new Point(62,-44) },
			{16, new Point(-64,-46) },
			{17, new Point(25,-75) },
			{18, new Point(46,64) },
			{19, new Point(-25,-73) },
			{20, new Point(0,74) },
		};
		#endregion
	}
}
