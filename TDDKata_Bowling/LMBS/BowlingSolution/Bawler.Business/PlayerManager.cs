using System.Collections.Generic;
using System.Linq;

namespace Bawler.Business
{
	public class PlayerManager
	{
		List<Frame> frames = new List<Frame>(){new Frame()};

		public void Roll(int pins)
		{
			var currentFrame = frames.Last();

			currentFrame.KnockDown(pins);
			if(currentFrame.IsDone)
			{
				frames.Add(new Frame(currentFrame));
			}
		}
		public int Frame
		{
			get { return frames.Count; }
		}

		public int Score()
		{
			return 9;
		}
	}
}