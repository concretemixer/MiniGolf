using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using MiniGolf.MVCS.Signals;

namespace MiniGolf.Game
{    
    public class Level : MonoBehaviour
    {
        [Inject]
        public LevelComplete levelComplete { get; set; }

        Vector3 ballPrevPos;
        Ball2 ball;

        // Use this for initialization
        void Start()
        {            
            ball = GameObject.FindObjectOfType<Ball2>();
        }

       
        [PostConstruct] 
        void RegisterListeners()
        {
            ball.ballHole.AddListener(onBallHole);
            ball.ballHit.AddListener(onBallHit);
            ball.ballLost.AddListener(onBallLost);
        }

        void OnDestroy()
        {
            ball.ballHole.RemoveListener(onBallHole);
            ball.ballHit.RemoveListener(onBallHit);
            ball.ballLost.RemoveListener(onBallLost);
        }
        // Update is called once per frame
        void Update()
        {

        }

        void onBallHole()
        {
            levelComplete.Dispatch();
        }

        void onBallHit(float forceK)
        {
            ballPrevPos = ball.transform.position;
        }

        void onBallLost()
        {
            ball.State = Ball2.BallState.Still;
            ball.transform.position = ballPrevPos;
        }
    }
}