using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DartBot.Player
{
	public class PlayerHand : IPlayerHand
	{
		private int tripleQuote = 100;
		private int doubleQuote = 100;
		private int singleQuote = 100;
		private double my = 0.0;
		private double sigma = 0.0;
		private readonly Random random;
		private readonly List<List<Color>> _pixels;
		public Point HitPoint { get; private set; }
		public PlayerHand(List<List<Color>> pixels)
		{
			_pixels = pixels;
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
			if (x < 0 || x >= _pixels.First().Count || y < 0 || y >= _pixels.Count)
				return DartBoard.GetInstance().GetOutside();
			var hit = DartBoard.GetInstance().GetFieldByColor(_pixels[(int) y][(int) x]);
			var rnddir = random.Next(4);
			while (hit == null)
			{
				if (rnddir == 0)
					x++;
				else if (rnddir == 1)
					x--;
				else if (rnddir == 2)
					y++;
				else
					y--;
				if (x < 0 || x >= _pixels.First().Count || y < 0 || y >= _pixels.Count)
					return DartBoard.GetInstance().GetOutside();
				hit = DartBoard.GetInstance().GetFieldByColor(_pixels[(int)y][(int)x]);
			}
			return hit;


			//switch (target.Type)
			//{
			//	case FieldEnum.SingleIn:
			//	case FieldEnum.SingleOut:
			//		return ThrowSingleDart(target);
			//	case FieldEnum.Double: return ThrowDoubleDart(target);
			//	case FieldEnum.SingleBull: return ThrowSingleBull(target);
					
			//	case FieldEnum.Triple: return ThrowTripleDart(target);
			//	case FieldEnum.DoubleBull: return ThrowDoubleBull(target);
					
			//}
			//return target;
		}

		private int GetRandomQuadrant()
		{
			return random.Next(4);
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

		public void AssignHitQuotes(int singleQuote, int doubleQuote, int tripleQuote)
		{
			this.singleQuote = singleQuote;
			this.doubleQuote = doubleQuote;
			this.tripleQuote = tripleQuote;
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

		private Field ThrowTripleDart(Field target)
		{
			if (HitTriple())
				return target;

			if (HitSingle())
				return HitSingleInOrSingleOut(target);

			var hit = GetNeighbourSegment(target);
			if (HitTriple())
				return hit;
			return HitSingleInOrSingleOut(hit);
		}

		private Field ThrowDoubleDart(Field target)
		{
			if (HitDouble())
				return target;

			if (HitSingle())
				return HitSingleOrOutside(target);

			var hit = GetNeighbourSegment(target);
			if (HitDouble())
				return hit;
			return HitSingleOrOutside(hit);
		}

		private Field ThrowSingleDart(Field target)
		{
			if (HitSingle())
			{
				if (Hit(5))
					return target.Neighbours.First(x => x.Type == FieldEnum.Triple);
				return target;
			}

			var hit = GetNeighbourSegment(target);
			if (Hit(5))
				return hit.Neighbours.First(x => x.Type == FieldEnum.Triple);
			return hit;
		}

		private Field ThrowSingleBull(Field target)
		{
			if (HitDouble())
				return target;

			if (HitTriple())
				return target.Neighbours.First(x => x.Type == FieldEnum.DoubleBull);

			var singles = target.Neighbours.Where(x => x.Type == FieldEnum.SingleIn).ToList();
			var rnd = this.random.Next(singles.Count);
			return singles[rnd];
		}

		private Field ThrowDoubleBull(Field target)
		{
			if (HitTriple())
				return target;

			if (HitDouble())
				return target.Neighbours.First(x => x.Type == FieldEnum.SingleBull);

			var singles = target.Neighbours.First(x => x.Type == FieldEnum.SingleBull).Neighbours.Where(x => x.Type == FieldEnum.SingleIn).ToList();
			var rnd = this.random.Next(singles.Count);
			return singles[rnd];
		}

		private Field HitSingleInOrSingleOut(Field target)
		{
			var rnd = this.random.Next(2);
			if (rnd == 0)
				return target.Neighbours.First(x => x.Type == FieldEnum.SingleIn);
			return target.Neighbours.First(x => x.Type == FieldEnum.SingleOut);
		}

		private Field HitSingleOrOutside(Field target)
		{
			var rnd = this.random.Next(2);
			if (rnd == 0)
				return target.Neighbours.First(x => x.Type == FieldEnum.Outside);
			return target.Neighbours.First(x => x.Type == FieldEnum.SingleOut);
		}

		private bool Hit(int propability)
		{
			var rnd = this.random.Next(100);
			if (rnd < propability)
				return true;
			return false;
		}

		private bool HitTriple()
		{
			return Hit(this.tripleQuote);
		}

		private bool HitDouble()
		{
			return Hit(this.doubleQuote);
		}

		private bool HitSingle()
		{
			return Hit(this.singleQuote);
		}

		private Field GetNeighbourSegment(Field target)
		{
			//links oder rechts
			var tripleNeighbours = target.Neighbours.Where(x => x.Type == target.Type).ToList();
			var rnd = this.random.Next(2);
			var hitSide = tripleNeighbours[rnd];

			//ein feld daneben oder zwei
			rnd = this.random.Next(5);
			if (rnd > 0)
				return hitSide;
			return hitSide.Neighbours.First(x => x.Type == target.Type && x.Value != target.Value);
		}
	}
}
