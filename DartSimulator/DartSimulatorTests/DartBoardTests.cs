using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;

namespace DartSimulatorTests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class DartBoardTests
	{
		[TestMethod]
		public void DartBoardShouldHaveGotAllFields()
		{
			var dartBoard = DartBoard.GetInstance();
			var fields = dartBoard.Fields.Select(x => x.ToString());
			Approvals.VerifyAll(fields, "Field");
		}

		[TestMethod]
		public void GetDoubleBullShouldReturnDoubleBullField()
		{
			var dartBoard = DartBoard.GetInstance();
			var doubleBull = dartBoard.GetDoubleBull();
			Approvals.Verify(doubleBull);
		}

		[TestMethod]
		public void GetSingleBullShouldReturnSingleBullField()
		{
			var dartBoard = DartBoard.GetInstance();
			var singleBull = dartBoard.GetSingleBull();
			Approvals.Verify(singleBull);
		}

		[TestMethod]
		public void GetSingleInFieldShouldReturnSingleInFieldWithRightValue()
		{
			var dartBoard = DartBoard.GetInstance();
			var fields =
				dartBoard.Fields.Where(x => x.Type == FieldEnum.SingleIn).Select(x => dartBoard.GetSingleInField(x.Value));
			Approvals.VerifyAll(fields, "Field");
		}
		[TestMethod]
		public void GetSingleOutFieldShouldReturnSingleOutFieldWithRightValue()
		{
			var dartBoard = DartBoard.GetInstance();
			var fields =
				dartBoard.Fields.Where(x => x.Type == FieldEnum.SingleOut).Select(x => dartBoard.GetSingleOutField(x.Value));
			Approvals.VerifyAll(fields, "Field");
		}
		[TestMethod]
		public void GetDoubleFieldShouldReturnDoubleFieldWithRightValue()
		{
			var dartBoard = DartBoard.GetInstance();
			var fields =
				dartBoard.Fields.Where(x => x.Type == FieldEnum.Double).Select(x => dartBoard.GetDoubleField(x.Value));
			Approvals.VerifyAll(fields, "Field");
		}
		[TestMethod]
		public void GetTripleFieldShouldReturnTripleFieldWithRightValue()
		{
			var dartBoard = DartBoard.GetInstance();
			var fields =
				dartBoard.Fields.Where(x => x.Type == FieldEnum.Triple).Select(x => dartBoard.GetTripleField(x.Value));
			Approvals.VerifyAll(fields, "Field");
		}

		[TestMethod]
		public void VerifyNeighboursOfFields()
		{
			var dartBoard = DartBoard.GetInstance();
			string verifyString = "";
			foreach (var field in dartBoard.Fields)
			{
				verifyString += field.ToString() + "\n";
				foreach (var neighbour in field.Neighbours)
				{
					verifyString += "\t" + neighbour.ToString() + "\n";
				}
			}
			Approvals.Verify(verifyString);
		}
	}
}
