using UnityEngine;
using UnityEngine.UI;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace MiniGolf.MVCS.Views
{
    public class LevelCompleteScreenView : View
    {

        [SerializeField]
        Button okButton;


        public readonly Signal onButtonOk = new Signal();

        protected override void Awake()
        {
            okButton.onClick.AddListener(onButtonOk.Dispatch);
            base.Awake();
        }
    

        protected override void OnDestroy()
        {
            okButton.onClick.RemoveListener(onButtonOk.Dispatch);

            base.OnDestroy();
        }

    }
}