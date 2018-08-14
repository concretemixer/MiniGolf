using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHelper : MonoBehaviour {

    public Vector3 GetNormal()
    {
        return transform.up;
    }
}
