using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCornerIn : Wall {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Vector3 GetNormal(Vector3 atPoint)
    {
        Vector3 v45 = transform.forward + transform.right;
        Vector3 v = atPoint - transform.position;

        float dotF = Vector3.Dot(v, transform.forward);
        float dotR = Vector3.Dot(v, transform.right);

        return dotF < dotR ? transform.right : transform.forward;
    }
}
