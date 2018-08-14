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
    }
}