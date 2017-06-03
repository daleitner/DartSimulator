using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base;

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
		private double hitQuote;
		private double hitRadius;
		private Leg selectedLeg = null;
		private ObservableCollection<Leg> legs = null;
		private string doubleQuote = "";
		#endregion

		#region ctors
		public MainViewModel()
		{
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
						param => Start()
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
		public double HitQuote
		{
			get
			{
				return this.hitQuote;
			}
			set
			{
				this.hitQuote = value;
				OnPropertyChanged("HitQuote");
			}
		}
		public double HitRadius
		{
			get
			{
				return this.hitRadius;
			}
			set
			{
				this.hitRadius = value;
				OnPropertyChanged("HitRadius");
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
		public string DoubleQuote
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
		#endregion

		#region private methods
		private void Start()
		{
		}

		#endregion

		#region public methods
		#endregion
	}
}
