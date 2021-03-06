using System;
using System.Collections.Generic;

namespace MiniGolf.MVCS.Services
{
    public class UIMap
    {
        public static Dictionary<Id, string> map = new Dictionary<Id, string>
        {
            {Id.GameHUD, "UI/GameHUD"},
            {Id.PauseMenu, "UI/ScreenPauseMenu"},
            {Id.LevelFailedMenu, "UI/LevelFailedMenu"},
            {Id.LevelCompleteScreen, "UI/LevelCompleteScreen"},
            {Id.TutorialDoneMenu,"UI/TutorialDoneMenu"},
            {Id.TutorialFailedMenu,"UI/TutorialFailedMenu"},
            {Id.LevelListScreen, "UI/LevelListScreen"},
            { Id.ScreenMain, "UI/StartGameScreen" },
            { Id.ScreenLoading, "UI/LoadingPanel" },
            { Id.ScreenTutorial, "UI/TutorialStepScreen" },
            { Id.ScreenDebug, "UI/ScreenDebug" },
            { Id.ScreenSettings, "UI/SettingsMenu" },
            { Id.InfoMessage, "UI/InfoMessage" },
            { Id.NoTriesMessage, "UI/NoTriesMessage" },
            { Id.CourseCompleteScreen, "UI/CourseCompleteScreen" },
            { Id.GameCompleteMessage, "UI/GameCompleteMessage" }
        };

        public enum Id
        {
            GameHUD,
            PauseMenu,
            LevelCompleteScreen,
            TutorialDoneMenu,
            TutorialFailedMenu,
            LevelFailedMenu,
            LevelListScreen,
            ScreenMain,
            ScreenLoading,
            ScreenTutorial,
            ScreenDebug,
            ScreenSettings,
            InfoMessage,
            NoTriesMessage,
            CourseCompleteScreen,
            GameCompleteMessage
        }

        public static string GetPath(UIMap.Id _id)
        {
            string path;
            if (!map.TryGetValue(_id, out path))
                throw new ArgumentException(string.Format("Undefined resource for UI id: {0}", _id.ToString()));

            return map[_id];
        }
    }
}