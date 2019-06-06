using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Size = System.Windows.Size;

namespace Dart.Base
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
			Focus();
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

			var imagePixels = new List<List<Color>>();
			for (int i = 0; i < img.Width; i++)
			{
				var row = new List<Color>();
				for (int j = 0; j < img.Height; j++)
				{
					var pixel = img.GetPixel(j, i);
					row.Add(pixel);
				}
				imagePixels.Add(row);
			}

			var pixels = new List<List<Color>>();
			for (int i = 0; i < imagePixels.Count+20; i++)
			{
				var row = new List<Color>();
				for (int j = 0; j < imagePixels[0].Count+20; j++)
				{
					row.Add(Color.White);
				}
				pixels.Add(row);
			}

			for (int i = 0; i < imagePixels.Count; i++)
			{
				for (int j = 0; j < imagePixels[i].Count; j++)
				{
					pixels[i + 10][j + 10] = imagePixels[i][j];
				}
			}

			return pixels;
		}

		public List<List<Color>> GetEmptyImage()
		{
			return _bytes;
		}

		public List<List<Color>> GetColoredImage(List<Field> fields)
		{
			var bytes = new List<List<Color>>(_bytes);
			ColorDartboard(bytes, fields);
			return bytes;
		}

		private void ColorDartboard(List<List<Color>> pixels, List<Field> fields)
		{
			foreach (var field in fields)
			{
				FloodFill(pixels, field.Color, TargetToPixelCoords(field.Target, pixels.First().Count, pixels.Count));
			}
		}

		private Point TargetToPixelCoords(Point target, int width, int height)
		{
			return new Point(target.X + width/2, target.Y * -1 + height/2);
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
