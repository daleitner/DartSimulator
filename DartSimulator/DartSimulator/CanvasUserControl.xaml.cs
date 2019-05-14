using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DartBot;
using DartSimulator.Common;
using Color = System.Drawing.Color;
using Size = System.Windows.Size;

namespace DartSimulator
{
	/// <summary>
	/// Interaction logic for CanvasUserControl.xaml
	/// </summary>
	public partial class CanvasUserControl
	{
		private readonly List<List<Color>> _bytes;
		public CanvasUserControl()
		{
			InitializeComponent();
			_bytes = ExportToPng(canvas);
		}

		public List<List<Color>> ExportToPng(Canvas surface)
		{
			// Save current canvas transform
			Transform transform = surface.LayoutTransform;
			// reset current transform (in case it is scaled or rotated)
			surface.LayoutTransform = null;

			// Get the size of canvas
			Size size = new Size(surface.Width, surface.Height);
			// Measure and arrange the surface
			// VERY IMPORTANT
			surface.Measure(size);
			surface.Arrange(new Rect(size));

			// Create a render bitmap and push the surface to it
			RenderTargetBitmap renderBitmap =
				new RenderTargetBitmap(
					(int)size.Width,
					(int)size.Height,
					96d,
					96d,
					PixelFormats.Pbgra32);
			renderBitmap.Render(surface);
			Bitmap img;
			// Create a file stream for saving image
			using (MemoryStream outStream = new MemoryStream())
			{
				// Use png encoder for our data
				BitmapEncoder encoder = new BmpBitmapEncoder();
				// push the rendered bitmap to it
				encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
				// save the data to the stream
				encoder.Save(outStream);
				img = new Bitmap(outStream);
				outStream.Close();
			}

			// Restore previously saved layout
			surface.LayoutTransform = transform;

			var pixels = new List<List<Color>>();
			for (int i = 0; i < img.Width; i++)
			{
				var row = new List<Color>();
				for (int j = 0; j < img.Height; j++)
				{
					var pixel = img.GetPixel(j, i);
					row.Add(pixel);
				}
				pixels.Add(row);
			}

			ColorDartboard(pixels);
			return pixels;
		}

		private void ColorDartboard(List<List<Color>> pixels)
		{
			var dartBoard = DartBoard.GetInstance();

			foreach (var field in dartBoard.Fields)
			{
				FloodFill(pixels, field.Color, field.Target);
			}
			FloodFill(pixels, dartBoard.GetOutside().Color, new System.Drawing.Point(0, 339));
			FloodFill(pixels, dartBoard.GetOutside().Color, new System.Drawing.Point(339, 0));
			FloodFill(pixels, dartBoard.GetOutside().Color, new System.Drawing.Point(339, 339));
			//SaveImage(pixels);
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

		private void FloodFill(List<List<Color>> pixels, Color fill, System.Drawing.Point pixel)
		{
			var source = pixels[pixel.Y][pixel.X];
			source = Color.FromArgb(source.A, source.R, source.G, source.B);
			if (source.IsEquivalentTo(fill))
				return;
			pixels[pixel.Y][pixel.X] = fill;
			var stack = new List<System.Drawing.Point> { pixel };
			var tolerance = 100;
			while (stack.Any())
			{
				var current = stack.Last();
				if (current.X > 0 && pixels[current.Y][current.X - 1].IsLike(source, tolerance) && !pixels[current.Y][current.X - 1].IsEquivalentTo(fill))
				{
					pixels[current.Y][current.X - 1] = fill;
					stack.Add(new System.Drawing.Point(current.X - 1, current.Y));
				}
				if (current.Y > 0 && pixels[current.Y - 1][current.X].IsLike(source, tolerance) && !pixels[current.Y - 1][current.X].IsEquivalentTo(fill))
				{
					pixels[current.Y - 1][current.X] = fill;
					stack.Add(new System.Drawing.Point(current.X, current.Y - 1));
				}
				if (current.X < pixels.First().Count - 1 && pixels[current.Y][current.X + 1].IsLike(source, tolerance) && !pixels[current.Y][current.X + 1].IsEquivalentTo(fill))
				{
					pixels[current.Y][current.X + 1] = fill;
					stack.Add(new System.Drawing.Point(current.X + 1, current.Y));
				}
				if (current.Y < pixels.Count - 1 && pixels[current.Y + 1][current.X].IsLike(source, tolerance) && !pixels[current.Y + 1][current.X].IsEquivalentTo(fill))
				{
					pixels[current.Y + 1][current.X] = fill;
					stack.Add(new System.Drawing.Point(current.X, current.Y + 1));
				}
				stack.Remove(current);
			}
		}

		public List<List<Color>> GetBytes()
		{
			return _bytes;
		}
	}
}
