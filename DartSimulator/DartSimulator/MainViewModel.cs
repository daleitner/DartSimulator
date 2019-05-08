using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Base;
using DartBot;
using DartSimulator.Controller;

namespace DartSimulator
{
	public class MainViewModel : ViewModelBase
	{
		#region members
		private RelayCommand _startCommand;
		private int _amountLegs = 10000;
		private int _singleQuote;
		private int _doubleQuote;
		private int _tripleQuote;
		private double _average;
		private double _dartAverage;
		private int _bestLeg;
		private int _worstLeg;
		private int _hundrets;
		private int _hundretFourties;
		private int _hundretEighties;
		private List<Leg> _legs;
		private List<Round> _runden; 
		private string _simulatedDoubleQuote; 
		private Leg _selectedLeg;
		private Result _result;
		private bool _isSortByDarts;
		private bool _score19;
		private int _maxCount;
		private int _halfMaxCount;
		private ObservableCollection<RoundCount> _roundCounts;
		private readonly ISimulationController _controller;
		#endregion

		#region ctors
		public MainViewModel(ISimulationController controller)
		{
			_controller = controller;
			_roundCounts = controller.InitializeRoundCounts();
			_simulatedDoubleQuote = "0% (0/0)";
			_result = new Result();
		}
		#endregion

		#region properties

		public double Average
		{
			get
			{
				return _average; 
			}
			set
			{
				_average = value;
				OnPropertyChanged(nameof(Average));
			}
		}

		public double DartAverage
		{
			get { return _dartAverage; }
			set
			{
				_dartAverage = value;
				OnPropertyChanged(nameof(DartAverage));
			}
		}

		public ICommand StartCommand
		{
			get
			{
				if (_startCommand == null)
				{
					_startCommand = new RelayCommand(
						param => Start(),
						param => CanStart()
					);
				}
				return _startCommand;
			}
		}

		public bool IsSortByDarts
		{
			get { return _isSortByDarts; }
			set
			{
				_isSortByDarts = value;
				SortLegs();
				OnPropertyChanged("IsSortByDarts");
			}
		}

		private void SortLegs()
		{
			if(IsSortByDarts)
				Legs = new List<Leg>(Legs.OrderBy(x => x.AmountDarts));
			else
			{
				Legs = new List<Leg>(Legs.OrderBy(x => x.Index));
			}
		}

		public int BestLeg
		{
			get { return _bestLeg; }
			set
			{
				_bestLeg = value;
				OnPropertyChanged(nameof(BestLeg));
			}
		}

		public int WorstLeg
		{
			get { return _worstLeg; }
			set
			{
				_worstLeg = value;
				OnPropertyChanged(nameof(WorstLeg));
			}
		}

		public int Hundrets
		{
			get { return _hundrets; }
			set
			{
				_hundrets = value;
				OnPropertyChanged(nameof(Hundrets));
			}
		}

		public int HundretFourties
		{
			get { return _hundretFourties; }
			set
			{
				_hundretFourties = value;
				OnPropertyChanged(nameof(HundretFourties));
			}
		}

		public int HundretEighties
		{
			get { return _hundretEighties; }
			set
			{
				_hundretEighties = value;
				OnPropertyChanged(nameof(HundretEighties));
			}
		}

		public int AmountLegs
		{
			get
			{
				return _amountLegs;
			}
			set
			{
				_amountLegs = value;
				OnPropertyChanged("AmountLegs");
			}
		}
		public int SingleQuote
		{
			get
			{
				return _singleQuote;
			}
			set
			{
				_singleQuote = value;
				OnPropertyChanged("SingleQuote");
			}
		}
		public int DoubleQuote
		{
			get
			{
				return _doubleQuote;
			}
			set
			{
				_doubleQuote = value;
				OnPropertyChanged("DoubleQuote");
			}
		}
		public int TripleQuote
		{
			get
			{
				return _tripleQuote;
			}
			set
			{
				_tripleQuote = value;
				OnPropertyChanged("TripleQuote");
			}
		}
		public Leg SelectedLeg
		{
			get
			{
				return _selectedLeg;
			}
			set
			{
				_selectedLeg = value;
				Runden = _selectedLeg != null ? new List<Round>(_selectedLeg.Runden) : new List<Round>();
				OnPropertyChanged("SelectedLeg");
			}
		}
		public List<Round> Runden
		{
			get
			{
				return _runden;
			}
			set
			{
				_runden = value;
				OnPropertyChanged(nameof(Runden));
			}
		}
		public List<Leg> Legs
		{
			get
			{
				return _legs;
			}
			set
			{
				_legs = value;
				OnPropertyChanged(nameof(Legs));
			}
		}
		public string SimulatedDoubleQuote
		{
			get { return _simulatedDoubleQuote; }
			set
			{
				_simulatedDoubleQuote = value;
				OnPropertyChanged(nameof(SimulatedDoubleQuote));
			}
		}

		public int MaxCount
		{
			get { return _maxCount; }
			set
			{
				_maxCount = value;
				OnPropertyChanged(nameof(MaxCount));
			}
		}

		public int HalfMaxCount
		{
			get { return _halfMaxCount; }
			set
			{
				_halfMaxCount = value;
				OnPropertyChanged(nameof(HalfMaxCount));
			}
		}

		public ObservableCollection<RoundCount> RoundCounts
		{
			get
			{
				return _roundCounts;
			}
			set
			{
				_roundCounts = value;
				OnPropertyChanged(nameof(RoundCounts));
			}
		}

		public bool Score19
		{
			get { return _score19; }
			set
			{
				_score19 = value;
				OnPropertyChanged(nameof(Score19));
			}
		}
		#endregion

		#region private methods
		private void Start()
		{
			_result = _controller.StartSimulation(AmountLegs, SingleQuote, DoubleQuote, TripleQuote, Score19);
			RoundCounts = _controller.FillRoundCounts(RoundCounts, _result);
			
			Refresh();
			SelectedLeg = Legs.FirstOrDefault();
		}
		private bool CanStart()
		{
			return SingleQuote > 0 && DoubleQuote > 0 && TripleQuote > 0;
		}

		private void Refresh()
		{
			Average = _result.Average;
			SimulatedDoubleQuote = $"{_result.DoubleQuote:0.##}" + "% (" + _result.Hits + "/" + _result.Tries + ")";
			BestLeg = _result.BestLeg;
			WorstLeg = _result.WorstLeg;
			HundretEighties = _result.HundretEighties;
			HundretFourties = _result.HundretFourties;
			Hundrets = _result.Hundrets;
			DartAverage = _result.DartAverage;
			if(IsSortByDarts)
				Legs = new List<Leg>(_result.Legs.OrderBy(x => x.AmountDarts));
			else
			{
				Legs = new List<Leg>(_result.Legs);
			}

			MaxCount = RoundCounts.Select(x => x.Count).Max();
			HalfMaxCount = MaxCount / 2;
		}
		#endregion

		#region public methods
		#endregion
	}
}
