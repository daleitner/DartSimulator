﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Dart.Base;

namespace DartBot.Player
{
	public class PlayerService : IPlayerService
	{
		private readonly DartBoard dartBoard = DartBoard.Instance;
		private readonly IPlayerHand playerHand;
		public List<Point> HitPoints { get; private set; } = new List<Point>();
		public PlayerService(IPlayerHand playerHand)
		{
			this.playerHand = playerHand;
		}

		public Leg PlayLeg()
		{
			HitPoints = new List<Point>();
			var leg = new Leg();
			//var leg2 = new Leg();
			var index = 1;
			while (leg.Points != 501)// && leg2.Points != 501)
			{
				var leftScore = 501-leg.Points;
			//	var leftScore2 = 501 - leg2.Points;

				var newRound = GetRound(leftScore, index);
				//var newRound2 = GetRound(leftScore2, index);
				leg.Runden.Add(newRound);
				//leg2.Runden.Add(newRound2);
				index++;
			}
			return leg;
		}

		public Round GetRound(int leftScoreAtStart, int index)
		{
			var round = new Round();
			var leftScore = leftScoreAtStart;
			var leftDarts = 3;
			var tries = 0;
			var darts = new List<Field> {round.Dart1, round.Dart2, round.Dart3};
			for (int i = 0; i < darts.Count; i++)
			{
				var target = ValidateTarget(leftScore, leftDarts);
				if (target.Type == FieldEnum.Double || target.Type == FieldEnum.DoubleBull)
					tries++;

				darts[i] = playerHand.ThrowDart(target);
				if(target.Value == 60)
					HitPoints.Add(playerHand.HitPoint);
				leftScore -= darts[i].Value;
				if (leftScore == 0 && (darts[i].Type == FieldEnum.Double || darts[i].Type == FieldEnum.DoubleBull))
				{
					break;
				}
				if (leftScore <= 1)
				{
					var noScore = GetNoScore();
					noScore.Tries = tries;
					noScore.Rest = leftScoreAtStart;
					noScore.Index = index;
					return noScore;
				}
				leftDarts--;
			}
			round.Dart1 = darts[0];
			round.Dart2 = darts[1];
			round.Dart3 = darts[2];
			round.Tries = tries;
			round.Rest = leftScore;
			round.Index = index;
			return round;
		}

		public Round GetNoScore()
		{
			return new Round {Dart1 = this.dartBoard.GetOutside(), Dart2 = this.dartBoard.GetOutside(), Dart3 = this.dartBoard.GetOutside()} ;
		}

		public Field ValidateTarget(int leftScore, int leftDarts)
		{
			if (leftScore >= 99)
			{
				if (leftDarts == 2)
				{
					if(leftScore == 110)
						return dartBoard.GetTripleField(60);
					if (leftScore == 107)
						return dartBoard.GetTripleField(57);
					if (leftScore == 104)
						return dartBoard.GetTripleField(54);
					if (leftScore == 101)
						return dartBoard.GetTripleField(51);
				}
				if (leftScore == 100 || leftScore == 118 || leftScore == 120 || leftScore == 121 ||
					leftScore == 124 || leftScore == 127 || leftScore == 130 || leftScore == 156 || 
					leftScore == 158 || leftScore == 160 || leftScore == 170)
						return dartBoard.GetTripleField(60);

				if (leftScore == 119 || leftScore == 123 || leftScore == 126 || leftScore == 129)
					return dartBoard.GetTripleField(57);

				if (leftScore == 122 || leftScore == 125 || leftScore == 128)
					return dartBoard.GetTripleField(54);

				return playerHand.Score19 ? dartBoard.GetTripleField(57) : dartBoard.GetTripleField(60);
			}
			var doubleField = GetDoubleField(leftScore);
			if (doubleField == null)
				return GetFieldToCheckNumber(leftScore, leftDarts);
			return this.dartBoard.GetDoubleField(leftScore);
		}

		public void AssignQuotes(double my, double sigma)
		{
			playerHand.AssignHitQuotes(my, sigma);
		}

		public void AssignPreferredTarget(bool score19)
		{
			playerHand.AssignPreferredTarget(score19);
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
				var selectedFields = dartBoard.GetFieldsByType(type).ToList();
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
			return playerHand.Score19 ? dartBoard.GetTripleField(57) : dartBoard.GetTripleField(60);
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
