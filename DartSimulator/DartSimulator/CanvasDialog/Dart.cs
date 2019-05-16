using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.CanvasDialog
{
	public class Dart
	{
		private bool _isTarget;
		public Dart(int index, bool isTarget = false)
		{
			Index = index;
			_isTarget = isTarget;
			X = 170;
			Y = 170;
		}

		public int Index { get; }
		public int X { get; set; }
		public int Y { get; set; }
		public Color Color => IsSelected ? Color.Blue : (_isTarget ? Color.Red : Color.Green);
		public bool IsSelected { get; set; }
	}
}
