using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    enum BallState
    {
        Air,
        Ground,
        Still
    }

    BallState state = BallState.Still;
    float timeInState = 0;

    bool aiming = false;
    bool ballistic = false;

    private BallState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
            timeInState = 0;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeInState += Time.deltaTime;
        if (State == BallState.Still)
        {
            if (aiming)
            {
                Plane ground = new Plane(Vector3.up, transform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float rayDistance;
                if (ground.Raycast(ray, out rayDistance))
                {
                    Vector3 touchGround = ray.origin + ray.direction * rayDistance;
                    if (Input.GetMouseButton(0))
                    {
                        GetComponent<LineRenderer>().enabled = true;
                        GetComponent<LineRenderer>().SetPosition(0, transform.position);
                        GetComponent<LineRenderer>().SetPosition(1, touchGround);
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        GetComponent<LineRenderer>().enabled = false;
                        Vector3 dir = touchGround - transform.position;

                        float forceK = Mathf.Clamp(dir.magnitude, 0.5f, 3.0f);
                        forceK = (forceK - 0.5f) / 2.5f;
                        dir.Normalize();

                        if (ballistic)
                        {
                            Vector3 tangent = Vector3.Cross(Vector3.up, dir);

                            dir = Quaternion.AngleAxis(45, tangent) * dir;
                            GetComponent<Rigidbody>().AddForce(-dir * Mathf.Lerp(10, 100, forceK), ForceMode.Force);
                            State = BallState.Air;
                        }
                        else
                        {
                            GetComponent<Rigidbody>().AddForce(-dir * Mathf.Lerp(10, 100, forceK), ForceMode.Force);
                            State = BallState.Ground;
                        }

                        //Debug.Log("d = " + dir.magnitude);

                        aiming = false;
                        
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && !aiming)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 0x100))
                {
                    //Vector3 touchBall = ray.origin + ray.direction * hit.distance;
                    //target.transform.position = touchGround;  
                    Debug.Log("Touch");
                    aiming = true;
                }
            }
        }
        if (State == BallState.Ground)
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            float distance = velocity.magnitude * Time.deltaTime;
            Vector3 tangent = Vector3.Cross(Vector3.up,velocity);

            float angle = distance / 0.1f;
            

            transform.Rotate(tangent, Mathf.Rad2Deg * angle);
        }
    }




    void FixedUpdate()
    {
        if (State == BallState.Air)
        {   
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().AddForce(-Vector3.up * 0.5f, ForceMode.Force);
        }
        if (State == BallState.Ground)
        {
            if (timeInState > 1.0f && GetComponent<Rigidbody>().velocity.magnitude < 0.01f)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                State = BallState.Still;
            }
        }
     
    }

}
