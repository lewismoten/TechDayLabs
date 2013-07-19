namespace Bawler.Business
{
	public class Frame
	{
		private readonly Frame _previousFrame;
		public int Score;
		public int Turns;
		public bool IsSpare;
		public bool IsStrike;

		public Frame()
		{
			
		}

		public int Number
		{
			get { return _previousFrame == null ? 1 : _previousFrame.Number + 1; }
		}

		public Frame(Frame previousFrame)
		{
			_previousFrame = previousFrame;
		}

		public int BallCount 
		{ 
			get { return Number == 10 ? 3 : 2; }
		}

		public void KnockDown(int pins)
		{
			Score += pins;
			Turns++;
			if (Score == 10 && Turns == 1)
			{
				IsStrike = true;
			}
			else if (Score == 10 && Turns == 2)
			{
				IsSpare = true;
			}

			ApplyScoreToPreviousFrames(pins);
		}

		private void ApplyScoreToPreviousFrames(int pins)
		{
			if (_previousFrame == null)
			{
				return;
			}

			if ((_previousFrame.IsSpare || _previousFrame.IsStrike) && Turns == 1)
			{
				_previousFrame.Score += pins;
				if (_previousFrame.IsStrike && _previousFrame._previousFrame != null && _previousFrame._previousFrame.IsStrike)
				{
					_previousFrame._previousFrame.Score += pins;
				}
			}
			else if (_previousFrame.IsStrike && Turns == 2)
			{
				_previousFrame.Score += pins;
			}
		}

		public int GetTotalScore()
		{	
			return Score + (_previousFrame != null ? _previousFrame.GetTotalScore() : 0);
		}
	}
}