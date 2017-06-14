using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using DartSimulator;

namespace DartSimulatorTests
{
	[TestClass]
	[UseReporter(typeof(TortoiseImageDiffReporter), typeof(ClipboardReporter))]
	public class GuiTests
	{
		[TestMethod]
		public void CheckDefaultGui()
		{
			var window = new MainWindow();
			WpfApprovals.Verify(window);
		}

		[TestMethod]
		public void CheckGuiWhenFilledValues()
		{
			var window = new MainWindow();
			var viewModel = window.DataContext as MainViewModel;
			viewModel.DoubleQuote = 20;
			viewModel.SingleQuote = 50;
			viewModel.TripleQuote = 10;
			WpfApprovals.Verify(window);
		}
	}
}
