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
		private double average = 0.0;
		private double dartAverage = 0.0;
		private RelayCommand startCommand = null;
		private int bestLeg = 0;
		private int worstLeg = 0;
		private int hundrets = 0;
		private int hundretFourties = 0;
		private int hundretEighties = 0;
		private int amountLegs = 10000;
		private double singleQuote;
		private double doubleQuote;
		private double tripleQuote;
		private Leg selectedLeg = null;
		private ObservableCollection<Leg> legs = null;
		private string simulatedDoubleQuote = "";
		private readonly ISimulationController controller;
		#endregion

		#region ctors
		public MainViewModel(ISimulationController controller)
		{
			this.controller = controller;
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
				OnPropertyChanged("Average");
			}
		}
		public double DartAverage
		{
			get
			{
				return this.dartAverage;
			}
			set
			{
				this.dartAverage = value;
				OnPropertyChanged("DartAverage");
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

		public int BestLeg
		{
			get
			{
				return this.bestLeg;
			}
			set
			{
				this.bestLeg = value;
				OnPropertyChanged("BestLeg");
			}
		}
		public int WorstLeg
		{
			get
			{
				return this.worstLeg;
			}
			set
			{
				this.worstLeg = value;
				OnPropertyChanged("WorstLeg");
			}
		}
		public int Hundrets
		{
			get
			{
				return this.hundrets;
			}
			set
			{
				this.hundrets = value;
				OnPropertyChanged("Hundrets");
			}
		}
		public int HundretFourties
		{
			get
			{
				return this.hundretFourties;
			}
			set
			{
				this.hundretFourties = value;
				OnPropertyChanged("HundretFourties");
			}
		}
		public int HundretEighties
		{
			get
			{
				return this.hundretEighties;
			}
			set
			{
				this.hundretEighties = value;
				OnPropertyChanged("HundretEighties");
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
		public double SingleQuote
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
		public double DoubleQuote
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
		public double TripleQuote
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
				OnPropertyChanged("SelectedLeg");
			}
		}
		public ObservableCollection<string> Runden
		{
			get
			{
				return this.SelectedLeg.Runden;
			}
			set
			{
				this.SelectedLeg.Runden = value;
				OnPropertyChanged("Runden");
			}
		}
		public ObservableCollection<Leg> Legs
		{
			get
			{
				return this.legs;
			}
			set
			{
				this.legs = value;
				OnPropertyChanged("Legs");
			}
		}
		public string SimulatedDoubleQuote
		{
			get
			{
				return this.simulatedDoubleQuote;
			}
			set
			{
				this.simulatedDoubleQuote = value;
				OnPropertyChanged("SimulatedDoubleQuote");
			}
		}
		#endregion

		#region private methods
		private void Start()
		{
			this.controller.StartSimulation();
		}
		private bool CanStart()
		{
			return this.SingleQuote > 0 && this.DoubleQuote > 0 && this.TripleQuote > 0;
		}
		#endregion

		#region public methods
		#endregion
	}
}
