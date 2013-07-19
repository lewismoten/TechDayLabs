using Bawler.Business;
using NUnit.Framework;

namespace BawlerTestProject
{
	[TestFixture]
	public class FrameTest
	{
		//[Test]
		//public void LastFrameHasThreeBalls()
		//{
		//    Frame frame = new Frame();
		//    frame.Number = 10;
		//    Assert.That(frame.BallCount, Is.EqualTo(3));
		//}

		//[Test]
		//public void NonLastFrameHasTwoBalls()
		//{
		//    Frame frame = new Frame();
		//    for (int frameNumber = 1; frameNumber < 10; frameNumber++)
		//    {
		//        frame.Number = frameNumber;
		//        Assert.That(frame.BallCount, Is.EqualTo(2));
		//    }
		//}

		[Test]
		public void FrameStartsWithZeroScore()
		{
			Frame frame = new Frame();
			Assert.That(frame.Score, Is.EqualTo(0));
		}

		[Test]
		public void FrameWithAGutterBallIsZero()
		{
			Frame frame = new Frame();
			frame.KnockDown(0);
			Assert.That(frame.Score, Is.EqualTo(0));
		}

		[Test]
		public void FrameWithTwoGutterBallsIsZero()
		{
			Frame frame = new Frame();
			frame.KnockDown(0);
			frame.KnockDown(0);
			Assert.That(frame.Score, Is.EqualTo(0));
		}

		[Test]
		public void OneRollCountsAsOneTurn()
		{
			Frame frame = new Frame();
			frame.KnockDown(0);
			Assert.That(frame.Turns, Is.EqualTo(1));
		}


		[Test]
		public void TwoRollsCountsAsSecondTurn()
		{
			Frame frame = new Frame();
			frame.KnockDown(0);
			frame.KnockDown(0);
			Assert.That(frame.Turns, Is.EqualTo(2));
		}

		[Test]
		public void ScoreSumsEachTurn()
		{
			Frame frame = new Frame();
			frame.KnockDown(2);
			frame.KnockDown(4);
			Assert.That(frame.Score, Is.EqualTo(6));
		}

		[Test]
		public void TwoRollsWith10IsSpare()
		{
			Frame frame = new Frame();
			frame.KnockDown(4);
			frame.KnockDown(6);
			Assert.That(frame.IsSpare, Is.True);
		}

		[Test]
		public void TwoRollsWith10IsNotStrike()
		{
			Frame frame = new Frame();
			frame.KnockDown(4);
			frame.KnockDown(6);
			Assert.That(frame.IsStrike, Is.False);
		}

		[Test]
		public void OneRollWith10IsStrike()
		{
			Frame frame = new Frame();
			frame.KnockDown(10);
			Assert.That(frame.IsStrike, Is.True);
		}

		[Test]
		public void OneRollWith10IsNotSpare()
		{
			Frame frame = new Frame();
			frame.KnockDown(10);
			Assert.That(frame.IsSpare, Is.False);
		}

		[Test]
		public void GutterBallThen10IsSpare()
		{
			Frame frame = new Frame();
			frame.KnockDown(0);
			frame.KnockDown(10);
			Assert.That(frame.IsStrike, Is.False);
			Assert.That(frame.IsSpare, Is.True);

		}
		
		[Test]
		public void ApplyNextFramesFirstBallToSpare()
		{

			Frame frame = new Frame();
			frame.KnockDown(2);
			frame.KnockDown(8);

			Frame nextFrame = new Frame(frame);
			nextFrame.KnockDown(3);
			nextFrame.KnockDown(4);

			Assert.That(frame.Score, Is.EqualTo(13));
		}

		[Test]
		public void ApplyNextFramesFirstAndSecondBallsToStrike()
		{

			Frame frame = new Frame();
			frame.KnockDown(10);

			Frame nextFrame = new Frame(frame);
			nextFrame.KnockDown(3);
			nextFrame.KnockDown(4);

			Assert.That(frame.Score, Is.EqualTo(17));
		}

		[Test]
		public void TurkeyShouldHave30ScoreForFrameWithInitialStrike()
		{
			Frame frame1 = new Frame();
			frame1.KnockDown(10);

			Frame frame2 = new Frame(frame1);
			frame2.KnockDown(10);

			Frame frame3 = new Frame(frame2);
			frame3.KnockDown(10);

			Assert.That(frame1.Score, Is.EqualTo(30));
			Assert.That(frame2.Score, Is.EqualTo(20));
			Assert.That(frame3.Score, Is.EqualTo(10));
		}

		[Test]
		public void AllGuttersIsZero()
		{
			Frame frame = null;
			for(var turn = 0; turn < 10; turn++)
			{
				frame = new Frame(frame);
				frame.KnockDown(0);
				frame.KnockDown(0);
			}
			Assert.That(frame.GetTotalScore(), Is.EqualTo(0));
			Assert.That(frame.Number, Is.EqualTo(10));
		}

		[Test]
		public void FullGameOfSpares()
		{
			Frame frame = new Frame();
			for(int i = 0; i < 10; i++)
			{
				frame = new Frame(frame);
				frame.KnockDown(5);
				frame.KnockDown(5);
			}
			frame.KnockDown(5);
			Assert.That(frame.GetTotalScore(), Is.EqualTo(150));
		}

		[Test]
		public void SeriousGameScenario()
		{
			Frame frame1 = new Frame();
			frame1.KnockDown(10);
			Frame frame2 = new Frame(frame1);
			frame2.KnockDown(10);
			Frame frame3 = new Frame(frame2);
			frame3.KnockDown(4);
			frame3.KnockDown(6);
			Frame frame4 = new Frame(frame3);
			frame4.KnockDown(1);
			frame4.KnockDown(7);

			Assert.That(frame1.GetTotalScore(), Is.EqualTo(24));
			Assert.That(frame2.GetTotalScore(), Is.EqualTo(44));
			Assert.That(frame3.GetTotalScore(), Is.EqualTo(55));
			Assert.That(frame4.GetTotalScore(), Is.EqualTo(63));
		}

		[Test]
		public void PerfectGameIs300()
		{
			Frame frame = null;
			for(var turn = 0; turn < 10; turn++)
			{
				frame = new Frame(frame);
				frame.KnockDown(10);
			}
			frame.KnockDown(10);
			frame.KnockDown(10);

			Assert.That(frame.GetTotalScore(), Is.EqualTo(300));
		}

		[Test]
		public void SpareInTenthFrame()
		{
			Frame frame = null;
			for (var turn = 0; turn < 9; turn++)
			{
				frame = new Frame(frame);
				frame.KnockDown(10);
			}
			frame = new Frame(frame);
			frame.KnockDown(8);
			frame.KnockDown(2);
			frame.KnockDown(1);

			Assert.That(frame.GetTotalScore(), Is.EqualTo(269));
		}

		[Test]
		public void CanTellWhatFrameNumberWereOn()
		{
			Frame frame = null;
			for (var turn = 0; turn < 10; turn++)
			{
				frame = new Frame(frame);
				frame.KnockDown(10);
			}

			Assert.That(frame.Number, Is.EqualTo(10));
		}
	}
}
