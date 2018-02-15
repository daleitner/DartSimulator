using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace DartSimulator
{
	public class RoundCount : ViewModelBase
	{
		private string _round;
		private int _height;
		private int _count;
		public RoundCount(string round)
		{
			_round = round;
		}

		public string Round
		{
			get { return _round; }
			set
			{
				_round = value;
				OnPropertyChanged(nameof(Round));
			}
		}

		public int Height
		{
			get { return _height; }
			set
			{
				_height = value;
				OnPropertyChanged(nameof(Height));
			}
		}

		public int Count
		{
			get { return _count; }
			set
			{
				_count = value;
				OnPropertyChanged(nameof(Count));
			}
		}

		public override string ToString()
		{
			return "Round: " + Round + ", Height: " + Height + ", Count: " + Count;
		}
	}
}
