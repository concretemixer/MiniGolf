using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelStartEvent : UnityEvent
{
}

public class LevelCompleteEvent : UnityEvent
{
}


public class Level : MonoBehaviour {
    
    public LevelCompleteEvent levelCompleteEvent = new LevelCompleteEvent();
    public LevelStartEvent levelStartEvent = new LevelStartEvent();

    Vector3 ballPrevPos;
    Ball2 ball;

    // Use this for initialization
    void Start () {
        levelStartEvent.Invoke();
        ball = GameObject.FindObjectOfType<Ball2>();
        if (ball!=null)
        {
            ball.ballHoleEvent.AddListener(onBallHole);
            ball.ballHitEvent.AddListener(onBallHit);
            ball.ballLostEvent.AddListener(onBallLost);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void onBallHole()
    {
        levelCompleteEvent.Invoke();        
    }

    void onBallHit()
    {
        ballPrevPos = ball.transform.position;    
    }

    void onBallLost()
    {
        ball.State = Ball2.BallState.Still;
        ball.transform.position = ballPrevPos;        
    }
}
