using UnityEngine;
using UnityEngine.UI;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace MiniGolf.MVCS.Views
{
    public class GameHUDView : View
    {

        [SerializeField]
        public Toggle hitUpToggle;

        [SerializeField]
        Toggle hitHorizontalToggle;

        [SerializeField]
        Slider forceSlider;

        [SerializeField]
        Slider forceSliderHistory;

        public readonly Signal<bool> onToggle = new Signal<bool>();

        protected override void Awake()
        {
            hitHorizontalToggle.onValueChanged.AddListener(onToggle.Dispatch);
            hitUpToggle.onValueChanged.AddListener(onToggle.Dispatch);
            base.Awake();
        }
    

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }


        public void SetHitTypeHorizontal(bool value)
        {
            hitHorizontalToggle.isOn = value;
            hitUpToggle.isOn = !value;
        }

        public void ShowHitForce(float value, float prev)
        {
            if (value >= 0)
                forceSlider.value = value;
            if (prev >= 0)
                forceSliderHistory.value = prev;
        }
    }
}