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


    // Use this for initialization
    void Start () {
        levelStartEvent.Invoke();
        Ball2 ball = GameObject.FindObjectOfType<Ball2>();
        if (ball!=null)
        {
            ball.ballHoleEvent.AddListener(onBallHole);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void onBallHole()
    {
        Debug.Log("HOLE!");
    }
}
