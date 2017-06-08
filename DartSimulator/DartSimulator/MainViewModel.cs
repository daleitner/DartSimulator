using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base;
using DartSimulator.Controller;

namespace DartSimulator
{
	public class MainViewModel : ViewModelBase
	{
		#region members
		private RelayCommand startCommand = null;
		private int amountLegs = 10000;
		private int singleQuote;
		private int doubleQuote;
		private int tripleQuote;
		private double average;
		private double dartAverage;
		private int bestLeg;
		private int worstLeg;
		private int hundrets;
		private int hundretFourties;
		private int hundretEighties;
		private List<Leg> legs;
		private List<Round> runden; 
		private string simulatedDoubleQuote; 
		private Leg selectedLeg = null;
		private Result result;
		private bool isSortByDarts = false;
		private readonly ISimulationController controller;
		#endregion

		#region ctors
		public MainViewModel(ISimulationController controller)
		{
			this.controller = controller;
			this.simulatedDoubleQuote = "0% (0/0)";
			this.result = new Result();
		}
		#endregion

		#region properties

		public double Average
		{
			get
			{
				return this.average; 
			}
			set
			{
				this.average = value;
				OnPropertyChanged(nameof(this.Average));
			}
		}

		public double DartAverage
		{
			get { return this.dartAverage; }
			set
			{
				this.dartAverage = value;
				OnPropertyChanged(nameof(this.DartAverage));
			}
		}

		public ICommand StartCommand
		{
			get
			{
				if (this.startCommand == null)
				{
					this.startCommand = new RelayCommand(
						param => Start(),
						param => CanStart()
					);
				}
				return this.startCommand;
			}
		}

		public bool IsSortByDarts
		{
			get { return this.isSortByDarts; }
			set
			{
				this.isSortByDarts = value;
				SortLegs();
				OnPropertyChanged("IsSortByDarts");
			}
		}

		private void SortLegs()
		{
			if(this.IsSortByDarts)
				this.Legs = new List<Leg>(this.Legs.OrderBy(x => x.AmountDarts));
			else
			{
				this.Legs = new List<Leg>(this.Legs.OrderBy(x => x.Index));
			}
		}

		public int BestLeg
		{
			get { return this.bestLeg; }
			set
			{
				this.bestLeg = value;
				OnPropertyChanged(nameof(this.BestLeg));
			}
		}

		public int WorstLeg
		{
			get { return this.worstLeg; }
			set
			{
				this.worstLeg = value;
				OnPropertyChanged(nameof(this.WorstLeg));
			}
		}

		public int Hundrets
		{
			get { return this.hundrets; }
			set
			{
				this.hundrets = value;
				OnPropertyChanged(nameof(this.Hundrets));
			}
		}

		public int HundretFourties
		{
			get { return this.hundretFourties; }
			set
			{
				this.hundretFourties = value;
				OnPropertyChanged(nameof(this.HundretFourties));
			}
		}

		public int HundretEighties
		{
			get { return this.hundretEighties; }
			set
			{
				this.hundretEighties = value;
				OnPropertyChanged(nameof(this.HundretEighties));
			}
		}

		public int AmountLegs
		{
			get
			{
				return this.amountLegs;
			}
			set
			{
				this.amountLegs = value;
				OnPropertyChanged("AmountLegs");
			}
		}
		public int SingleQuote
		{
			get
			{
				return this.singleQuote;
			}
			set
			{
				this.singleQuote = value;
				OnPropertyChanged("SingleQuote");
			}
		}
		public int DoubleQuote
		{
			get
			{
				return this.doubleQuote;
			}
			set
			{
				this.doubleQuote = value;
				OnPropertyChanged("DoubleQuote");
			}
		}
		public int TripleQuote
		{
			get
			{
				return this.tripleQuote;
			}
			set
			{
				this.tripleQuote = value;
				OnPropertyChanged("TripleQuote");
			}
		}
		public Leg SelectedLeg
		{
			get
			{
				return this.selectedLeg;
			}
			set
			{
				this.selectedLeg = value;
				this.Runden = this.selectedLeg != null ? new List<Round>(this.selectedLeg.Runden) : new List<Round>();
				OnPropertyChanged("SelectedLeg");
			}
		}
		public List<Round> Runden
		{
			get
			{
				return this.runden;
			}
			set
			{
				this.runden = value;
				OnPropertyChanged(nameof(this.Runden));
			}
		}
		public List<Leg> Legs
		{
			get
			{
				return this.legs;
			}
			set
			{
				this.legs = value;
				OnPropertyChanged(nameof(this.Legs));
			}
		}
		public string SimulatedDoubleQuote
		{
			get { return this.simulatedDoubleQuote; }
			set
			{
				this.simulatedDoubleQuote = value;
				OnPropertyChanged(nameof(this.SimulatedDoubleQuote));
			}
		}
		#endregion

		#region private methods
		private void Start()
		{
			this.result = this.controller.StartSimulation(this.AmountLegs, this.SingleQuote, this.DoubleQuote, this.TripleQuote);
			Refresh();
			this.SelectedLeg = this.Legs.FirstOrDefault();
		}
		private bool CanStart()
		{
			return this.SingleQuote > 0 && this.DoubleQuote > 0 && this.TripleQuote > 0;
		}

		private void Refresh()
		{
			this.Average = this.result.Average;
			this.SimulatedDoubleQuote = $"{this.result.DoubleQuote:0.##}" + "% (" + this.result.Hits + "/" + this.result.Tries + ")";
			this.BestLeg = this.result.BestLeg;
			this.WorstLeg = this.result.WorstLeg;
			this.HundretEighties = this.result.HundretEighties;
			this.HundretFourties = this.result.HundretFourties;
			this.Hundrets = this.result.Hundrets;
			this.DartAverage = this.result.DartAverage;
			if(this.IsSortByDarts)
				this.Legs = new List<Leg>(this.result.Legs.OrderBy(x => x.AmountDarts));
			else
			{
				this.Legs = new List<Leg>(this.result.Legs);
			}
		}
		#endregion

		#region public methods
		#endregion
	}
}
