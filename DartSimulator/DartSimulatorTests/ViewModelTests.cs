using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace DartSimulatorTests
{
	[TestClass]
	public class ViewModelTests
	{
		[TestMethod]
		public void WhenCreatingNewViewModel_LegsShouldBeTenThousand()
		{
			var viewModel = new MainViewModel();
			viewModel.AmountLegs.ShouldEqual(10000);
		}
	}
}
