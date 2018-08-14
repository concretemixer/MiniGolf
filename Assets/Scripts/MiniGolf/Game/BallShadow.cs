using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGolf.Game
{
    public class BallShadow : MonoBehaviour
    {
        Ball2 ball;

        // Use this for initialization
        void Start()
        {
            ball = GameObject.FindObjectOfType<Ball2>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = ball.transform.position;
        }
    }
}
