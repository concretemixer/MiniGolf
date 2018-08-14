using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public float normalBounceK = 0;
      
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual Vector3 GetNormal(Vector3 atPoint)
    {
        return Vector3.up;
    }
}
