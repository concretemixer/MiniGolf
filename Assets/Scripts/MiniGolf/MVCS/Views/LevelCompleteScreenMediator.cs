
using UnityEngine;
using strange.extensions.mediation.impl;
//using Commons.SN.Facebook;
using MiniGolf.MVCS.Signals;
using MiniGolf.MVCS.Services;


namespace MiniGolf.MVCS.Views
{
    public class LevelCompleteScreenMediator : Mediator
    {
        [Inject]
        public LevelComplete startLevel { get; set; }

        [Inject(EntryPoint.Container.Stage)]
        public GameObject stage { get; set; }

        [Inject]
        public LevelCompleteScreenView view { get; set; }

        [Inject]
        public IUIManager UI { get; set; }

        [Inject]
        public ILocaleService localeService { get; set; }
   
        void okHandler()
        {
            UI.Hide(UIMap.Id.LevelCompleteScreen);
        }

        public override void OnRegister()
        {
            view.onButtonOk.AddListener(okHandler);
            base.OnRegister();
        }

        public override void OnRemove()
        {
            view.onButtonOk.RemoveListener(okHandler);
            base.OnRemove();
        }
    }
}