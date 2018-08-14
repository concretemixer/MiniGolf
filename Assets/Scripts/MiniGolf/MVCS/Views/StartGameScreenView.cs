using UnityEngine;
using UnityEngine.UI;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace MiniGolf.MVCS.Views
{
    public class StartGameScreenView : View
    {

        [SerializeField]
        Button startButton;


        public readonly Signal onButtonStart = new Signal();

        protected override void Awake()
        {
            startButton.onClick.AddListener(onButtonStart.Dispatch);
            base.Awake();
        }
    

        protected override void OnDestroy()
        {
            startButton.onClick.RemoveListener(onButtonStart.Dispatch);

            base.OnDestroy();
        }

    }
}