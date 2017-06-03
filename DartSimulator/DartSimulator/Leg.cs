using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator
{
	public class Leg
	{
		public Leg()
		{
			this.Runden = new ObservableCollection<string>();
		}
		public ObservableCollection<string> Runden;
	}
}
