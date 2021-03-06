﻿using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;

using MiniGolf.MVCS.Signals;
using MiniGolf.MVCS.Commands;
using MiniGolf.MVCS.Commands.Init;
using MiniGolf.MVCS.Services;
using MiniGolf.MVCS.Views;

namespace MiniGolf.MVCS
{
    public class AppContext : MVCSContext
    {
      //  [Inject]
        //public AnalyticsCollector analitycs { private get; set; }

        EntryPoint entryPoint;

        public void OnPause(bool pauseStatus)
        {            
            if (!pauseStatus)
            {
                /*
                AnalyticsCollector analitycs = injectionBinder.GetInstance<AnalyticsCollector>();
                if (analitycs!=null)
                    analitycs.SessionStart();*/
            }
        }

        public AppContext(EntryPoint _view) : base(_view, true)
        {
            entryPoint = _view;
        }

        /// remap command binder to signals
        protected override void addCoreComponents()
        {
            // up signals
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch()
        {
            injectionBinder.GetInstance<StartupSignal>().Dispatch();
        }

        protected override void mapBindings()
        {
            mapCommands();
            mapSignals();
            mapModels();
            mapStageMediators();
            mapUIMediators();
            mapOthers();            
        }

        void mapCommands()
        {
            
            // init commands
            commandBinder.Bind<StartupSignal>().InSequence()
                .To<CreateServiceItemsCommand>()
                .To<StartupCommand>();

            commandBinder.Bind<StartCourseSignal>().To<StartCourseCommand>();
            commandBinder.Bind<StartLevelSignal>().To<StartLevelCommand>();            
            commandBinder.Bind<InitLevelSignal>().To<InitLevelCommand>();

            /*
            commandBinder.Bind<LevelPause>().To<PauseLevelCommand>();
            commandBinder.Bind<LevelResume>().To<ResumeLevelCommand>();
            commandBinder.Bind<LevelRetry>().To<RetryLevelCommand>();
            commandBinder.Bind<TutorialPoint>().To<TutorialPointCommand>();
            commandBinder.Bind<SwitchToMainScreenSignal>().To<SwitchToMainScreenCommand>();
            commandBinder.Bind<SwitchToStartScreenSignal>().To<SwitchToStartScreenCommand>();
            commandBinder.Bind<SwitchToSettingsScreenSignal>().To<SwitchToSettingsScreenCommand>();
              */
            // Ads commands
            
            /*
            commandBinder.Bind<ShowAdsSignal>().InSequence().
                To<InitializeUnityAdsCommand>().
                To<ShowAdsCommand>();*/
           // commandBinder.Bind<ShowAdsSignal>().InSequence().                
          //      To<ShowAppodealAdsCommand>();
          //  commandBinder.Bind<AddLivesForAdsSignal>().To<AddLivesForAdsCommad>();
        }

        void mapSignals()
        {
            injectionBinder.Bind<BallHit>().ToSingleton();
            injectionBinder.Bind<BallHole>().ToSingleton();
            injectionBinder.Bind<BallLost>().ToSingleton();
            injectionBinder.Bind<BallSetForce>().ToSingleton();
            injectionBinder.Bind<BallStopped>().ToSingleton();

            injectionBinder.Bind<LevelComplete>().ToSingleton();

            /*
            injectionBinder.Bind<OrientationChangedSignal>().ToSingleton();
            injectionBinder.Bind<VehicleReachedDestination>().ToSingleton();
            injectionBinder.Bind<VehicleCrashed>().ToSingleton();
            injectionBinder.Bind<LevelFailed>().ToSingleton();
            
            injectionBinder.Bind<ScoreGrow>().ToSingleton();
            injectionBinder.Bind<ResumeTutorial>().ToSingleton();
            injectionBinder.Bind<PurchaseFailed>().ToSingleton();
            injectionBinder.Bind<PurchaseCancelled>().ToSingleton();
            injectionBinder.Bind<RestorePurchasesFailed>().ToSingleton();
            injectionBinder.Bind<PurshaseOk>().ToSingleton();
            */
        }

        void mapModels()
        {
            /*
            injectionBinder.Bind<ILevelModel>().To<LevelModel>();
            injectionBinder.Bind<ILevelListModel>().To<LevelListModel>().ToSingleton();
#if UNITY_STANDALONE 
            injectionBinder.Bind<IAPService>().To<IAPServiceDummy>().ToSingleton();
#elif UNITY_WEBGL
            injectionBinder.Bind<IAPService>().To<IAPServiceVK>().ToSingleton();
#else
            injectionBinder.Bind<IAPService>().To<IAPServiceUnity>().ToSingleton();
#endif
            injectionBinder.Bind<ILocaleService>().To<LocaleService>().ToSingleton();
            */
            injectionBinder.Bind<ILocaleService>().To<LocaleService>().ToSingleton();
            injectionBinder.Bind<IUIManager>().To<UIManager>().ToSingleton();
        }

        void mapStageMediators()
        {
          //  mediationBinder.Bind<LevelView>().To<LevelMediator>();
        }

        void mapUIMediators()
        {
            mediationBinder.Bind<StartGameScreenView>().To<StartGameScreenMediator>();
            mediationBinder.Bind<GameHUDView>().To<GameHUDMediator>();
            mediationBinder.Bind<LevelCompleteScreenView>().To<LevelCompleteScreenMediator>();
            mediationBinder.Bind<CourseCompleteScreenView>().To<CourseCompleteScreenMediator>();

            /*
            mediationBinder.Bind<LevelListScreenView>().To<LevelListScreenMediator>();
            mediationBinder.Bind<LevelFailedMenuView>().To<LevelFailedMenuMediator>();
            mediationBinder.Bind<PauseMenuView>().To<PauseMenuMediator>();
            mediationBinder.Bind<LoadingScreenView>().To<LoadingScreenMediator>();
            mediationBinder.Bind<TutorialStepScreen>().To<TutorialStepMediator>();            
            mediationBinder.Bind<ScreenDebugView>().To<ScreenDebugMediator>();
            mediationBinder.Bind<SettingsMenuView>().To<SettingsMenuMediator>();
            mediationBinder.Bind<InfoMessageView>().To<InfoMesageMediator>();
            mediationBinder.Bind<NoTriesMessageView>().To<NoTriesMessageMediator>();
            mediationBinder.Bind<LevelPackDoneMessageView>().To<LevelPackDoneMessageMediator>();
              */
        }

        void mapOthers()
        {
            injectionBinder.Bind<GameObject>().To(entryPoint.StageMenu).ToName(EntryPoint.Container.StageMenu);
            injectionBinder.Bind<GameObject>().To(entryPoint.Stage).ToName(EntryPoint.Container.Stage);
            injectionBinder.Bind<GameObject>().To(entryPoint.UI).ToName(EntryPoint.Container.UI);
           // injectionBinder.Bind<AnalyticsCollector>().To<AnalyticsCollector>().ToSingleton();
        }
    }
}
