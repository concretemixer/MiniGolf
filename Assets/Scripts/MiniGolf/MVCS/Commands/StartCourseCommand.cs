using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using strange.extensions.command.impl;
using strange.extensions.context.api;

using MiniGolf.MVCS.Signals;
using MiniGolf.MVCS.Services;
using MiniGolf.Game;


namespace MiniGolf.MVCS.Commands
{

	public class StartCourseCommand : Command
	{
		[Inject]
		public IUIManager UI { private get; set; }

        [Inject]
        public StartLevelSignal startLevel { get; set; }

        public override void Execute()
		{

            Time.timeScale = 0.85f;

            safeUnbind<Course>(GameState.Current);
            Course course = new Course(new string[] { "Level1","Level2"}, 5);

            injectionBinder.Bind<Course>().To(course).ToName(GameState.Current);
            startLevel.Dispatch(course.currentLevelIndex);
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

