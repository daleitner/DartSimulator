using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Base;

namespace DartSimulator.CanvasDialog
{
	public class TargetViewModel : ViewModelBase
	{
		#region members
		private double _mittelwert;
		private double _standardAbweichung;
		private RelayCommand _upCommand;
		private RelayCommand _downCommand;
		private RelayCommand _leftCommand;
		private RelayCommand _rightCommand;
		private RelayCommand _nextCommand;
		private RelayCommand _previousCommand;
		private BitmapImage _content;
		private readonly Bitmap _defaultBitmap;
		private readonly List<Dart> _darts;
		private Dart _selectedDart;
		#endregion

		#region ctors
		public TargetViewModel()
		{
			var canvas = new CanvasUserControl();
			var pixels = canvas.GetBytes();
			_defaultBitmap = new Bitmap(pixels.First().Count, pixels.Count);
			for (int i = 0; i < pixels.Count; i++)
			{
				for (int j = 0; j < pixels[i].Count; j++)
				{
					_defaultBitmap.SetPixel(j, i, pixels[i][j]);
				}
			}

			_darts = new List<Dart>
			{
				new Dart(0,true)
			};
			_selectedDart = _darts.First();
			_selectedDart.IsSelected = true;
			RefreshImage();
		}
		#endregion

		#region properties
		public double Mittelwert
		{
			get
			{
				return _mittelwert;
			}
			set
			{
				_mittelwert = value;
				OnPropertyChanged(nameof(Mittelwert));
			}
		}
		public double StandardAbweichung
		{
			get
			{
				return _standardAbweichung;
			}
			set
			{
				_standardAbweichung = value;
				OnPropertyChanged(nameof(StandardAbweichung));
			}
		}
		public ICommand UpCommand
		{
			get
			{
				if (_upCommand == null)
				{
					_upCommand = new RelayCommand(
						param => Up()
					);
				}
				return _upCommand;
			}
		}
		public ICommand DownCommand
		{
			get
			{
				if (_downCommand == null)
				{
					_downCommand = new RelayCommand(
						param => Down()
					);
				}
				return _downCommand;
			}
		}
		public ICommand LeftCommand
		{
			get
			{
				if (_leftCommand == null)
				{
					_leftCommand = new RelayCommand(
						param => Left()
					);
				}
				return _leftCommand;
			}
		}
		public ICommand RightCommand
		{
			get
			{
				if (_rightCommand == null)
				{
					_rightCommand = new RelayCommand(
						param => Right()
					);
				}
				return _rightCommand;
			}
		}
		public ICommand NextCommand
		{
			get
			{
				if (_nextCommand == null)
				{
					_nextCommand = new RelayCommand(
						param => Next()
					);
				}
				return _nextCommand;
			}
		}
		public ICommand PreviousCommand
		{
			get
			{
				if (_previousCommand == null)
				{
					_previousCommand = new RelayCommand(
						param => Previous()
					);
				}
				return _previousCommand;
			}
		}

		public int CurrentX => SelectedDart?.X ?? 0;
		public int CurrentY => SelectedDart?.Y ?? 0;
		public BitmapImage Content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
				OnPropertyChanged(nameof(Content));
			}
		}
		public string Current => SelectedDart?.Index.ToString() ?? "";

		public Dart SelectedDart
		{
			get => _selectedDart;
			set
			{
				_selectedDart.IsSelected = false;
				_selectedDart = value;
				_selectedDart.IsSelected = true;
				OnPropertyChanged(nameof(SelectedDart));
			}
		}
		#endregion

		#region private methods
		private void Up()
		{
			SelectedDart.Y--;
			RefreshImage();
		}

		private void Down()
		{
			SelectedDart.Y++;
			RefreshImage();
		}

		private void Left()
		{
			SelectedDart.X--;
			RefreshImage();
		}

		private void Right()
		{
			SelectedDart.X++;
			RefreshImage();
		}

		private void Next()
		{
			var index = _darts.IndexOf(SelectedDart);
			if (index < 0)
				return;
			if(index == _darts.Count-1)
				_darts.Add(new Dart(index+1));
			SelectedDart = _darts[index + 1];
			//RefreshImage();
		}

		private void Previous()
		{
			var index = _darts.IndexOf(SelectedDart);
			if (index < 1)
				return;
			SelectedDart = _darts[index - 1];
			RefreshImage();
		}

		private void RefreshImage()
		{
			Bitmap img = (Bitmap)_defaultBitmap.Clone();

			foreach (var dart in _darts)
			{
				img.SetPixel(dart.X, dart.Y, dart.Color);
			}

			BitmapImage bimg;
			using (MemoryStream memory = new MemoryStream())
			{
				img.Save(memory, ImageFormat.Png);
				memory.Position = 0;
				bimg = new BitmapImage();
				bimg.BeginInit();
				bimg.StreamSource = memory;
				bimg.CacheOption = BitmapCacheOption.OnLoad;
				bimg.EndInit();
			}

			Content = bimg;
			OnPropertyChanged(nameof(Current));
			OnPropertyChanged(nameof(CurrentX));
			OnPropertyChanged(nameof(CurrentY));
			var distances = CalculateDistances();
			CalculateMittelwert(distances);
			CalculateStandardAbweichung(distances);
		}

		private List<double> CalculateDistances()
		{
			var ret = new List<double>();
			var target = _darts.First();
			foreach (var dart in _darts)
			{
				if (dart == target)
					continue;

				ret.Add(Math.Sqrt(Math.Pow(target.X-dart.X, 2) + Math.Pow(target.Y-dart.Y, 2)));
			}
			return ret;
		}

		private void CalculateMittelwert(List<double> distances)
		{
			var sum = 0.0;
			distances.ForEach(x => sum+=x);
			Mittelwert = distances.Count > 0 ? Math.Round(sum / distances.Count, 2) : 0.0;
		}

		private void CalculateStandardAbweichung(List<double> distances)
		{
			var sum = 0.0;
			distances.ForEach(x => sum += Math.Pow(x-Mittelwert, 2));
			StandardAbweichung = distances.Count > 0 ? Math.Round(Math.Sqrt(sum / distances.Count), 2) : 0.0;
		}
		#endregion

		public void SetPosition(double x, double y)
		{
			SelectedDart.X = (int) x;
			SelectedDart.Y = (int) y;
			RefreshImage();
		}
	}
}
