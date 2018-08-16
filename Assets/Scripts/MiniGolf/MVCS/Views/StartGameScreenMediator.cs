
using UnityEngine;
using strange.extensions.mediation.impl;
//using Commons.SN.Facebook;
using MiniGolf.MVCS.Signals;
using MiniGolf.MVCS.Services;


namespace MiniGolf.MVCS.Views
{
    public class StartGameScreenMediator : Mediator
    {
        [Inject]
        public StartCourseSignal startCourse { get; set; }

        [Inject(EntryPoint.Container.Stage)]
        public GameObject stage { get; set; }

        [Inject]
        public StartGameScreenView view { get; set; }

        [Inject]
        public IUIManager UI { get; set; }

        [Inject]
        public ILocaleService localeService { get; set; }
   
        void startHandler()
        {            
            startCourse.Dispatch();
        }


        void quitHandler()
        {
            Application.Quit();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                quitHandler();
        }

        public override void OnRegister()
        {
            view.onButtonStart.AddListener(startHandler);
            base.OnRegister();
        }

        public override void OnRemove()
        {
            view.onButtonStart.RemoveListener(startHandler);
            base.OnRemove();
        }
    }
}