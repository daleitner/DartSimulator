using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Size = System.Windows.Size;

namespace DartSimulator.CanvasDialog
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
			
			return pixels;
		}

		public List<List<Color>> GetBytes()
		{
			return _bytes;
		}
	}
}
