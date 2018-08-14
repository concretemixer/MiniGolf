using UnityEngine;
using UnityEngine.SceneManagement;

using strange.extensions.command.impl;
using MiniGolf.MVCS.Services;
using MiniGolf.Game;


namespace MiniGolf.MVCS.Commands
{

	public class InitLevelCommand : Command
	{
		[Inject]
		public IUIManager UI { private get; set; }

		[Inject(EntryPoint.Container.Stage)]
		public  GameObject stage { get  ; set ;}

        [Inject(EntryPoint.Container.StageMenu)]
        public GameObject stageMenu { get; set; }

        public override void Execute()
        {
            UI.HideAll();

            safeUnbind<Level>(GameState.Current);
            safeUnbind<Ball2>(GameState.Current);

            stageMenu.SetActive(false);

            if (stage.transform.childCount > 0)
                GameObject.Destroy(stage.transform.GetChild(0).gameObject);

            GameObject root = GameObject.FindGameObjectWithTag("Root");
    		SceneManager.MergeScenes(SceneManager.GetActiveScene(),root.scene);

            root.transform.SetParent(stage.transform);
            root.tag = "Untagged";

            Level level = root.GetComponentInChildren<Level>();
            Ball2 ball = root.GetComponentInChildren<Ball2>();
            injectionBinder.injector.Inject(level);
            injectionBinder.injector.Inject(ball);
            
            injectionBinder.Bind<Level>().To(level).ToName(GameState.Current);
            injectionBinder.Bind<Ball2>().To(ball).ToName(GameState.Current);

            UI.Show(UIMap.Id.GameHUD);
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

