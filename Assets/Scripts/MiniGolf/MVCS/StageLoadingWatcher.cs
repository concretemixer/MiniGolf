using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using MiniGolf.MVCS;
using MiniGolf.MVCS.Signals;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;

namespace MiniGolf {

    public class StageLoadingWatcher : MonoBehaviour {

        [Inject]
        public InitLevelSignal initLevel { get; set; }

        [Inject]
        public StartLevelSignal startLevel { get; set; }

        IEnumerator waitForInit()
        {
            while (true)
            {
                if (GetComponentInParent<EntryPoint>().context == null)
                    yield return null;
                break;
            }

            (GetComponentInParent<EntryPoint>().context as AppContext).injectionBinder.injector.Inject(this);
            startLevel.AddListener(onStartLevel);
        }


        IEnumerator waitForLevelLoad()
        {
            while (true)
            {
                if (GameObject.FindGameObjectWithTag("Root") == null)
                    yield return null;
                break;
            }

            initLevel.Dispatch();
        }

        // Use this for initialization
        void Start() {
            StartCoroutine("waitForInit");
        }

        void onStartLevel(int a)
        {
            Debug.Log("Loading " + a);
            StartCoroutine("waitForLevelLoad");
        }
    }
}
