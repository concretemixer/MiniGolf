using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using MiniGolf.MVCS.Services;

namespace MiniGolf.MVCS.Commands
{
    public class StartupCommand : Command
    {
        [Inject]
        public IUIManager UI { private get; set; }

        [Inject(EntryPoint.Container.StageMenu)]
        public GameObject stageMenu { get; set; }

        public override void Execute()
        {
            UI.Hide(UIMap.Id.ScreenLoading);
            UI.Show(UIMap.Id.ScreenMain);
        }
    }
}