using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSlope : Floor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 normal = Vector3.zero;
    public override Vector3 GetNormal(Vector3 atPoint)
    {
        if (normal == Vector3.zero)
        {
            normal = GetComponentInChildren<NormalHelper>().GetNormal();
        }
        return normal;
    }

}
