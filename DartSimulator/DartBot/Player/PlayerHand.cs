using System;
using System.Linq;

namespace DartBot.Player
{
	public class PlayerHand : IPlayerHand
	{
		private int tripleQuote = 100;
		private int doubleQuote = 100;
		private int singleQuote = 100;
		private readonly Random random;

		public PlayerHand()
		{
			this.random = new Random();
		}
		public Field ThrowDart(Field target)
		{
			switch (target.Type)
			{
				case FieldEnum.SingleIn:
				case FieldEnum.SingleOut:
					return ThrowSingleDart(target);
				case FieldEnum.Double: return ThrowDoubleDart(target);
				case FieldEnum.SingleBull: return ThrowSingleBull(target);
					
				case FieldEnum.Triple: return ThrowTripleDart(target);
				case FieldEnum.DoubleBull: return ThrowDoubleBull(target);
					
			}
			return target;
		}

		public void AssignHitQuotes(int singleQuote, int doubleQuote, int tripleQuote)
		{
			this.singleQuote = singleQuote;
			this.doubleQuote = doubleQuote;
			this.tripleQuote = tripleQuote;
		}

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
