using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public float normalBounceK = 0;
    public bool isHorisontal = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetNormal(Vector3 atPoint)
    {
        return Vector3.up;
    }
}
