
using UnityEngine;
using strange.extensions.mediation.impl;
//using Commons.SN.Facebook;
using MiniGolf.MVCS.Signals;
using MiniGolf.MVCS.Services;
using MiniGolf.Game;


namespace MiniGolf.MVCS.Views
{
    public class GameHUDMediator : Mediator
    {
        [Inject(GameState.Current)]
        public Level level { get; set; }

        [Inject(GameState.Current)]
        public Ball2 ball { get; set; }

        [Inject(EntryPoint.Container.Stage)]
        public GameObject stage { get; set; }

        [Inject]
        public GameHUDView view { get; set; }

        [Inject]
        public IUIManager UI { get; set; }

        [Inject]
        public ILocaleService localeService { get; set; }
   
        void hitToggleHandler(bool state)
        {
            ball.SetBallistic(view.hitUpToggle.isOn);          
        }

        void setForceHandler(float value)
        {
            view.ShowHitForce(value, -1);
        }

        void setPrevForceHandler(float value)
        {
            view.ShowHitForce(-1, value);
        }

        void levelCompleteHandler()
        {
            UI.Show(UIMap.Id.LevelCompleteScreen);
        }

        public override void OnRegister()
        {
            view.SetHitTypeHorizontal(false);
            ball.SetBallistic(view.hitUpToggle.isOn);

            view.onToggle.AddListener(hitToggleHandler);

            ball.ballSetForce.AddListener(setForceHandler);
            ball.ballHit.AddListener(setPrevForceHandler);

            level.levelComplete.AddListener(levelCompleteHandler);

            base.OnRegister();
        }

        public override void OnRemove()
        {
            view.onToggle.RemoveListener(hitToggleHandler);
            ball.ballSetForce.RemoveListener(setForceHandler);
            ball.ballHit.RemoveListener(setPrevForceHandler);
            level.levelComplete.RemoveListener(levelCompleteHandler);
            base.OnRemove();
        }
    }
}