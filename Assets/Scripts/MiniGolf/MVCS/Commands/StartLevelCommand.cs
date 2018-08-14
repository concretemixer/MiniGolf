using UnityEngine;
using UnityEngine.SceneManagement;
using strange.extensions.command.impl;
using strange.extensions.context.api;

using MiniGolf.MVCS;
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


		[Inject]
		public int levelIndex{get;set;}

       
        public override void Execute()
		{

            Time.timeScale = 0.85f;

			//safeUnbind<ILevelModel>(GameState.Current);
            //LevelModel levelModel = injectionBinder.GetInstance<ILevelModel>() as LevelModel;
            //injectionBinder.Bind<ILevelModel>().To(levelModel).ToName(GameState.Current);
	        
            SceneManager.LoadScene("Level1", LoadSceneMode.Additive);                
	    }
        /*
        public override void Execute()
        {
            UI.Hide(UIMap.Id.ScreenHUD);
            UI.Hide(UIMap.Id.LevelFailedMenu);
            UI.Hide(UIMap.Id.LevelDoneMenu);
            UI.Hide(UIMap.Id.PauseMenu);
            UI.Hide(UIMap.Id.LevelListScreen);

            safeUnbind<ILevelModel>(GameState.Current);

            if (stage.transform.childCount > 0)
                GameObject.Destroy(stage.transform.GetChild(0).gameObject);


            GameplayConfig levelCfgs = new GameplayConfig();

            for (int a=0;a<levels.LevelNames.Length;a++) {
                GameObject instance = Object.Instantiate(Resources.Load("levels/" + levels.LevelNames[a], typeof(GameObject))) as GameObject;

                LevelConfig cfg = new LevelConfig();

                cfg.threeStarsScore = 10000;
                cfg.twoStarsScore = 5000;

                cfg.target = instance.GetComponentInChildren<Level>().targetScore;

                foreach (var pitcher in instance.transform.GetComponentsInChildren<Pitcher>())
                {                    
                    PitcherConfig p = new PitcherConfig();
                    p.startDelay = pitcher.Pause;
                    p.intervalMin = pitcher.IntervalMin;
                    p.intervalMax = pitcher.IntervalMax;

                    cfg.pitchers.Add(pitcher.gameObject.name, p);
                }

                GameObject.Destroy(instance);

                levelCfgs.levels[a+1] = cfg;
                
            }

            string data = JsonWriter.Serialize(levelCfgs);
            var streamWriter = new StreamWriter("out.json");
            streamWriter.Write(data);
            streamWriter.Close();
           
        }
        */

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

