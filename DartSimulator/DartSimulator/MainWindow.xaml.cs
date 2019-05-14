using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using DartSimulator.Factory;
using Color = System.Drawing.Color;

namespace DartSimulator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var factory = new ViewModelFactory();
			var viewModel = factory.CreateMainViewModel();
			this.DataContext = viewModel;
			var control = new CanvasUserControl();
			var bytes = control.GetBytes();
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
	}
}
