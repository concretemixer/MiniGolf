using UnityEngine;
using UnityEngine.SceneManagement;
using strange.extensions.command.impl;
using strange.extensions.context.api;

using MiniGolf.Game;
using MiniGolf.MVCS.Services;


namespace MiniGolf.MVCS.Commands
{
	public class StartLevelCommand : Command
	{
		[Inject]
		public IUIManager UI { private get; set; }

		[Inject(EntryPoint.Container.Stage)]
		public  GameObject stage { get  ; set ;}

        [Inject(EntryPoint.Container.StageMenu)]
        public GameObject stageMenu { get; set; }

        [Inject(GameState.Current)]
        public Course course { get; set; }

       
        public override void Execute()
		{
            Time.timeScale = 0.85f;
            UI.Show(UIMap.Id.ScreenLoading);
            SceneManager.LoadScene(course.LevelAt(course.currentLevelIndex), LoadSceneMode.Additive);                
	    }

		void safeUnbind<T>()
		{
			var binding = injectionBinder.GetBinding<T>();
			if (binding != null)
				injectionBinder.Unbind<T>();
		}
		
		void safeUnbind<T>(object name)
		{
			var binding = injectionBinder.GetBinding<T>(name);
			if (binding != null)
				injectionBinder.Unbind<T>(name);
		}
	}
}

