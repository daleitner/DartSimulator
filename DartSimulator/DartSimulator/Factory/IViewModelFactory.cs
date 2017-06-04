using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.Factory
{
	public interface IViewModelFactory
	{
		MainViewModel CreateMainViewModel();
	}
}
