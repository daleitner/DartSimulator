using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DartSimulator.Player
{
	public class PlayerService : IPlayerService
	{
		private readonly DartBoard dartBoard = DartBoard.GetInstance();
		public Leg PlayLeg()
		{
			var leg = new Leg();
			while (leg.Points != 501)
			{
				var newRound = GetRound();
				leg.Runden.Add(newRound);
			}
			return leg;
		}

		public Round GetRound()
		{
			return new Round {Dart1 = this.dartBoard.GetTripleField(60), Dart2 = this.dartBoard.GetTripleField(57), Dart3 = this.dartBoard.GetDoubleBull()};
		}

		public Round GetNoScore(int triesToDouble)
		{
			return new Round {Dart1 = this.dartBoard.GetOutside(), Dart2 = this.dartBoard.GetOutside(), Dart3 = this.dartBoard.GetOutside(), Tries = triesToDouble} ;
		}

		public Field ValidateTarget(int leftScore, int leftDarts)
		{
			if (leftScore >= 99)
			{
				if (leftDarts == 2)
				{
					if (leftScore == 107)
						return this.dartBoard.GetTripleField(57);
					if (leftScore == 104)
						return this.dartBoard.GetTripleField(54);
					if (leftScore == 101)
						return this.dartBoard.GetTripleField(51);
				}
				return this.dartBoard.GetTripleField(60);
			}
			var doubleField = GetDoubleField(leftScore);
			if (doubleField == null)
				return GetFieldToCheckNumber(leftScore, leftDarts);
			return this.dartBoard.GetDoubleField(leftScore);
		}

		private Field GetFieldToCheckNumber(int leftScore, int leftDarts)
		{
			var doubleBull = this.dartBoard.GetDoubleBull();
			if (leftScore == 50 && leftDarts == 1)
				return doubleBull;
			var prioList = new List<List<Field>> {new List<Field>(), new List<Field>(), new List<Field>(), new List<Field>()};
			var typeList = new List<FieldEnum> {FieldEnum.SingleOut, FieldEnum.SingleBull, FieldEnum.Double, FieldEnum.Triple};
			foreach (var type in typeList)
			{
				var selectedFields = this.dartBoard.Fields.Where(x => x.Type == type).ToList();
				if(type == FieldEnum.Triple)
					selectedFields.Add(doubleBull);
				foreach (var field in selectedFields)
				{
					var doubleField = GetDoubleField(leftScore - field.Value);
					if (doubleField == null)
					{
						if(leftDarts == 2 && leftScore-field.Value == 50)
							prioList[GetPrio(doubleBull)].Add(field);
						continue;
					}
					prioList[GetPrio(doubleField)].Add(field);
				}
				foreach (var list in prioList)
				{
					if (list.Count > 0)
						return list.First(x => list.Max(y => y.Value) == x.Value);
				}
			}
			return this.dartBoard.GetTripleField(60);
		}

		private Field GetDoubleField(int leftScore)
		{
			return this.dartBoard.GetDoubleField(leftScore);
		}

		private int GetPrio(Field doubleField)
		{
			if (doubleField.Value % 16 == 0)
				return 0;
			if (doubleField.Value % 8 == 0)
				return 1;
			if (doubleField.Value % 4 == 0)
				return 2;
			return 3;
		}
	}
}
