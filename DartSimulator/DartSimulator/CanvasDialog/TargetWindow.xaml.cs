using System.Windows.Controls;
using System.Windows.Input;

namespace DartSimulator.CanvasDialog
{
	/// <summary>
	/// Interaction logic for TargetWindow.xaml
	/// </summary>
	public partial class TargetWindow
	{
		public TargetWindow()
		{
			InitializeComponent();
			MouseLeftButtonDown += TargetWindow_MouseLeftButtonDown;
		}

		private void TargetWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var point = e.GetPosition(img);
			if(point.X > 0 && point.X < 680 && point.Y > 0 && point.Y < 680)
				ViewModel.SetPosition(point.X/2, point.Y/2);
		}

		public TargetViewModel ViewModel => ((TargetViewModel) DataContext);
	}
}
