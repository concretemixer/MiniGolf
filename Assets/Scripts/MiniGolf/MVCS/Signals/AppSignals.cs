using strange.extensions.signal.impl;

namespace MiniGolf.MVCS.Signals
{
    public class StartupSignal : Signal
    {
    }

    public class StartCourseSignal : Signal
    {
    }

    public class StartLevelSignal : Signal<int>
    {
    }

    public class InitLevelSignal : Signal
    {
    }

    public class SwitchToMainScreenSignal : Signal
    {
    }

    public class SwitchToStartScreenSignal : Signal
    {
    }

    public class SwitchToSettingsScreenSignal : Signal
    {
    }

    public class OrientationChangedSignal : Signal
    {
    }

    public class ShowAdsSignal : Signal
    {
    }

    public class AddLivesForAdsSignal : Signal
    {
    }
}