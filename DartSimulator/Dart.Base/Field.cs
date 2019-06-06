using System.Collections.Generic;
using System.Drawing;

namespace Dart.Base
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

	/// <summary>
	/// Represents one Field on the Dartboard.
	/// </summary>
	public class Field
	{
		public Field(int value, FieldEnum type)
		{
			this.Value = value;
			this.Type = type;
		}
		#region properties
		/// <summary>
		/// The Points you get when hit this Field. 
		/// </summary>
		public int Value { get; set; }
		/// <summary>
		/// The Type of the Field. 
		/// </summary>
		public FieldEnum Type { get; internal set; }
		/// <summary>
		/// The Left Neighbor of the Field which has the same Type of this Field. Note: The only Neighbor of Single Bull is Double Bull.
		/// </summary>
		public Field LeftNeighbor { get; internal set; }
		/// <summary>
		/// The Right Neighbor of the Field which has the same Type of this Field. Note: The only Neighbor of Single Bull is Double Bull.
		/// </summary>
		public Field RightNeighbor { get; internal set; }
		/// <summary>
		/// X/Y-Coordinates of the Point when target this Field. [0,0] is the middle of the dartboard.
		/// </summary>
		public Point Target { get; set; }
		/// <summary>
		/// List of Hits to this Field. 
		/// </summary>
		public List<Point> Hits { get; private set; } = new List<Point>();
		/// <summary>
		/// Color of the Field. Used for Dartboard to identify the Field on Image.
		/// </summary>
		internal Color Color { get; set; }
		/// <summary>
		/// Some Value a developer can set additionally to this Field.
		/// </summary>
		public object AdditionalValue { get; set; }
		#endregion

		#region methods

		/// <summary>
		/// Get the Field as String.
		/// Single 20 = 20
		/// Double 20 = D20
		/// Triple 20 = T20
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			switch (Type)
			{
				case FieldEnum.Double:
				case FieldEnum.DoubleBull:
					return "D" + Value / 2;
				case FieldEnum.Triple:
					return "T" + Value / 3;
				default:
					return Value.ToString();
			}
		}

		/// <summary>
		/// Clear the Hits on this Field.
		/// </summary>
		public void ClearHits()
		{
			Hits = new List<Point>();
		}
		#endregion

	}
}
