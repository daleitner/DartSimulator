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
		private double singleQuote;
		private double doubleQuote;
		private double tripleQuote;
		private Leg selectedLeg = null;
		private Result result;
		private readonly ISimulationController controller;
		#endregion

		#region ctors
		public MainViewModel(ISimulationController controller)
		{
			this.controller = controller;
			this.result = new Result();
		}
		#endregion

		#region properties
		public double Average
		{
			get
			{
				return this.result.Average;
			}
			set
			{
				this.result.Average = value;
				OnPropertyChanged("Average");
			}
		}
		public double DartAverage
		{
			get
			{
				return this.result.DartAverage;
			}
			set
			{
				this.result.DartAverage = value;
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
				return this.result.BestLeg;
			}
			set
			{
				this.result.BestLeg = value;
				OnPropertyChanged("BestLeg");
			}
		}
		public int WorstLeg
		{
			get
			{
				return this.result.WorstLeg;
			}
			set
			{
				this.result.WorstLeg = value;
				OnPropertyChanged("WorstLeg");
			}
		}
		public int Hundrets
		{
			get
			{
				return this.result.Hundrets;
			}
			set
			{
				this.result.Hundrets = value;
				OnPropertyChanged("Hundrets");
			}
		}
		public int HundretFourties
		{
			get
			{
				return this.result.HundretFourties;
			}
			set
			{
				this.result.HundretFourties = value;
				OnPropertyChanged("HundretFourties");
			}
		}
		public int HundretEighties
		{
			get
			{
				return this.result.HundretEighties;
			}
			set
			{
				this.result.HundretEighties = value;
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
				return this.result.Legs;
			}
			set
			{
				this.result.Legs = value;
				OnPropertyChanged("Legs");
			}
		}
		public string SimulatedDoubleQuote
		{
			get
			{
				return $"{this.result.DoubleQuote:0.##}"+"%";
			}
			set
			{
				//this.result.DoubleQuote = value;
				OnPropertyChanged("SimulatedDoubleQuote");
			}
		}
		#endregion

		#region private methods
		private void Start()
		{
			this.result = this.controller.StartSimulation();
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
