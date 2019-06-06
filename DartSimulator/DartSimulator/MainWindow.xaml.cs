using DartSimulator.Factory;

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
		}
	}
}
