using System;
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
	}
}
