using UnityEngine;
using UnityEngine.SceneManagement;

using strange.extensions.command.impl;
using MiniGolf.MVCS.Services;
using MiniGolf.MVCS;


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

            stageMenu.SetActive(false);

            if (stage.transform.childCount > 0)
                GameObject.Destroy(stage.transform.GetChild(0).gameObject);

            GameObject root = GameObject.FindGameObjectWithTag("Root");
    		SceneManager.MergeScenes(SceneManager.GetActiveScene(),root.scene);

            root.transform.SetParent(stage.transform);
            root.tag = "Untagged";


            //UI.Show(UIMap.Id.ScreenHUD);
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

