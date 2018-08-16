using UnityEngine;
using strange.extensions.mediation.impl;

using MiniGolf.Game;
using MiniGolf.MVCS.Signals;
using MiniGolf.MVCS.Services;


namespace MiniGolf.MVCS.Views
{
    public class CourseCompleteScreenMediator : Mediator
    {
        [Inject(GameState.Current)]
        public Course course { get; set; }

        [Inject]
        public StartLevelSignal startLevel { get; set; }

        [Inject]
        public CourseCompleteScreenView view { get; set; }

        [Inject]
        public IUIManager UI { get; set; }

        [Inject]
        public ILocaleService localeService { get; set; }
   
        void okHandler()
        {
            UI.Hide(UIMap.Id.CourseCompleteScreen);
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