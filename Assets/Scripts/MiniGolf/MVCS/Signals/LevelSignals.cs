//using Traffic.MVCS.model;
//using Traffic.MVCS.model.level;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MiniGolf.MVCS.Signals
{
    public class LevelPackComplete : Signal
    {
    }

    public class LevelFailed : Signal
    {
    }

    public class LevelComplete : Signal
    {
    }

	public class LevelPause : Signal
	{
	}

	public class LevelResume : Signal
	{
	}

    public class LevelRetry : Signal
    {
    }

    public class BallLost : Signal
    {
    }

    public class BallHit : Signal<float>
    {
    }

    public class BallStopped : Signal
    {
    }

    public class BallHole : Signal
    {
    }

    public class BallSetForce : Signal<float>
    {
    }

}