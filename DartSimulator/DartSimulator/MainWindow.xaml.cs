using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using DartBot;
using DartSimulator.CanvasDialog;
using DartSimulator.Common;
using DartSimulator.Factory;
using Color = System.Drawing.Color;

namespace DartSimulator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			var factory = new ViewModelFactory();
			var viewModel = factory.CreateMainViewModel();
			DataContext = viewModel;
			var control = new CanvasUserControl();
			var bytes = control.GetBytes();
			ColorDartboard(bytes);
			SaveImage(bytes);
		}

		private void SaveImage(List<List<Color>> pixels)
		{
			var img = new Bitmap(pixels.First().Count, pixels.Count);
			for (int i = 0; i < pixels.Count; i++)
			{
				for (int j = 0; j < pixels[i].Count; j++)
				{
					img.SetPixel(j, i, pixels[i][j]);
				}
			}

			img.Save("test2.png", ImageFormat.Png);
		}

		private void ColorDartboard(List<List<Color>> pixels)
		{
			var dartBoard = DartBoard.GetInstance();

			foreach (var field in dartBoard.Fields)
			{
				FloodFill(pixels, field.Color, field.Target);
			}
			FloodFill(pixels, dartBoard.GetOutside().Color, new Point(0, 339));
			FloodFill(pixels, dartBoard.GetOutside().Color, new Point(339, 0));
			FloodFill(pixels, dartBoard.GetOutside().Color, new Point(339, 339));
			//SaveImage(pixels);
		}

		private void FloodFill(List<List<Color>> pixels, Color fill, Point pixel)
		{
			var source = pixels[pixel.Y][pixel.X];
			source = Color.FromArgb(source.A, source.R, source.G, source.B);
			if (source.IsEquivalentTo(fill))
				return;
			pixels[pixel.Y][pixel.X] = fill;
			var stack = new List<Point> { pixel };
			var tolerance = 100;
			while (stack.Any())
			{
				var current = stack.Last();
				if (current.X > 0 && pixels[current.Y][current.X - 1].IsLike(source, tolerance) && !pixels[current.Y][current.X - 1].IsEquivalentTo(fill))
				{
					pixels[current.Y][current.X - 1] = fill;
					stack.Add(new Point(current.X - 1, current.Y));
				}
				if (current.Y > 0 && pixels[current.Y - 1][current.X].IsLike(source, tolerance) && !pixels[current.Y - 1][current.X].IsEquivalentTo(fill))
				{
					pixels[current.Y - 1][current.X] = fill;
					stack.Add(new Point(current.X, current.Y - 1));
				}
				if (current.X < pixels.First().Count - 1 && pixels[current.Y][current.X + 1].IsLike(source, tolerance) && !pixels[current.Y][current.X + 1].IsEquivalentTo(fill))
				{
					pixels[current.Y][current.X + 1] = fill;
					stack.Add(new Point(current.X + 1, current.Y));
				}
				if (current.Y < pixels.Count - 1 && pixels[current.Y + 1][current.X].IsLike(source, tolerance) && !pixels[current.Y + 1][current.X].IsEquivalentTo(fill))
				{
					pixels[current.Y + 1][current.X] = fill;
					stack.Add(new Point(current.X, current.Y + 1));
				}
				stack.Remove(current);
			}
		}
	}
}
