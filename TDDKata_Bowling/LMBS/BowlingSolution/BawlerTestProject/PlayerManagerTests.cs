using Bawler.Business;
using NUnit.Framework;

namespace BawlerTestProject
{
	[TestFixture]
	public class PlayerManagerTests
	{

		/*
			PlayerManager
		 * 
		 *  ShowCurrentScore
		 *  ShowScoresForAllFrames
		 *  GameFinished
		 *  StartGame -> If not first game -> Save previous game or print
		*/

		[Test]
		public void SumsTotalScore()
		{
			PlayerManager playerManager = new PlayerManager();
			playerManager.Roll(3);
			playerManager.Roll(6);
			Assert.That(playerManager.Score(), Is.EqualTo(9));
		}

		[Test]
		public void ShowsCurrentFrame()
		{
			PlayerManager playerManager = new PlayerManager();
			playerManager.Roll(3);
			playerManager.Roll(6);
			Assert.That(playerManager.Frame, Is.EqualTo(2));
		}

		[Test]
		public void StartsOnFirstFrame()
		{
			PlayerManager playerManager = new PlayerManager();
			Assert.That(playerManager.Frame, Is.EqualTo(1));
		}

		[Test]
		public void ShowsCorrectFrameAfterStrike()
		{
			PlayerManager playerManager = new PlayerManager();
			playerManager.Roll(10);
			Assert.That(playerManager.Frame, Is.EqualTo(2));
		}

		[Test]
		public void ShowsCorrectFrameAfterTwoGutterBalls()
		{
			PlayerManager playerManager = new PlayerManager();
			playerManager.Roll(0);
			playerManager.Roll(0);
			Assert.That(playerManager.Frame, Is.EqualTo(2));
		}

		[Test]
		public void ShowsCorrectFrameAfterAGutterBall()
		{
			PlayerManager playerManager = new PlayerManager();
			playerManager.Roll(0);
			Assert.That(playerManager.Frame, Is.EqualTo(1));
		}

	}
}