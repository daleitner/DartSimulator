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
		public double Average => this.result.Average;
		public double DartAverage => this.result.DartAverage;

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

		public int BestLeg => this.result.BestLeg;

		public int WorstLeg => this.result.WorstLeg;

		public int Hundrets => this.result.Hundrets;

		public int HundretFourties => this.result.HundretFourties;

		public int HundretEighties => this.result.HundretEighties;

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
		public List<Round> Runden
		{
			get
			{
				return this.SelectedLeg?.Runden;
			}
			set
			{
				this.SelectedLeg.Runden = value;
				OnPropertyChanged("Runden");
			}
		}
		public List<Leg> Legs
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
				return $"{this.result.DoubleQuote:0.##}"+"% (" + this.result.Hits + "/" + this.result.Tries + ")";
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
			this.SelectedLeg = this.Legs.FirstOrDefault();
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
