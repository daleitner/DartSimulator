using System;
using System.Drawing;
using Dart.Base;

namespace DartBot.Player
{
	public class PlayerHand : IPlayerHand
	{
		private double my = 0.0;
		private double sigma = 0.0;
		private readonly Random random;
		public Point HitPoint { get; private set; }
		public PlayerHand()
		{
			this.random = new Random();
		}
		public Field ThrowDart(Field target)
		{
			var distance = GetNormalDistributedRandom();
			var angle = GetRandomAngle();
			var quadrant = GetRandomQuadrant();

			var x = Math.Round(Math.Cos(Math.PI * angle / 180.0) * distance, 0);
			var y = Math.Round(Math.Sin(Math.PI * angle / 180.0) * distance, 0);
			
			if (quadrant == 1 || quadrant == 2)
				x = x * -1;
			if (quadrant == 2 || quadrant == 3)
				y = y * -1;

			x += target.Target.X;
			y += target.Target.Y;

			HitPoint = new Point((int) x, (int) y);
			DartBoard.Instance.Hit((int)x, (int)y);
			var hit = DartBoard.Instance.GetFieldByXY((int) x,(int) y);
			return hit;
		}

		private int GetRandomQuadrant()
		{
			var updown = random.Next(3);
			if (updown == 1)
				return random.Next(3) == 1 ? 1 : 4;
			return random.Next(7) <= 3 ? 3 : 2;
			//return random.Next(4);
		}

		private int GetRandomAngle()
		{
			return random.Next(91);
		}

		private double GetNormalDistributedRandom()
		{
			double u1 = 1.0 - random.NextDouble(); //uniform(0,1] random doubles
			double u2 = 1.0 - random.NextDouble();
			double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
			double erg = my + sigma * randStdNormal; //random normal(mean,stdDev^2)
			return erg;
		}

		public void AssignPreferredTarget(bool score19)
		{
			Score19 = score19;
		}

		public void AssignHitQuotes(double my, double sigma)
		{
			this.my = my;
			this.sigma = sigma;
		}

		public bool Score19 { get; private set; }
		
		private bool Hit(int propability)
		{
			var rnd = this.random.Next(100);
			if (rnd < propability)
				return true;
			return false;
		}
	}
}
