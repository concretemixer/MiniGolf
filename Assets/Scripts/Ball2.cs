using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BallLostEvent : UnityEvent
{
}

public class BallHitEvent : UnityEvent
{
}

public class BallStoppedEvent : UnityEvent
{
}

public class BallHoleEvent : UnityEvent
{
}



public class Ball2 : MonoBehaviour
{
    public float AirDragDefault = 0.5f;
    public float GroundDragDefault = 1.0f;

    public BallHitEvent ballHitEvent = new BallHitEvent();
    public BallLostEvent ballLostEvent = new BallLostEvent();
    public BallStoppedEvent ballStoppedEvent = new BallStoppedEvent();
    public BallHoleEvent ballHoleEvent = new BallHoleEvent();

    private float Radius = 0.1f;

    enum BallState
    {
        Launch,
        Air,
        Hit,
        Ground,
        Still        
    }


    Vector3 forceToApply = Vector3.zero;
    BallState state = BallState.Air;
    float timeInState = 0;
    float angularVelocity = 0;
    float stillTimer = 0;

    bool aiming = false;
    bool ballistic = false;

    int inCollisionCount = 0;
    private Dictionary<int, float> dragKStack = new Dictionary<int, float>();


    public void SetBallistic(bool value)
    {
        ballistic = value;
    }

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
    void Start()
    {

    }


    void UpdateHUD()
    {
        GameObject _state = GameObject.Find("BallStateText");
        if (_state != null)
        {
            _state.GetComponent<Text>().text = State.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();

        timeInState += Time.deltaTime;
        //if (State == BallState.Still)
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

                        float forceK = Mathf.Clamp(dir.magnitude, 2f, 10.0f);
                        forceK = (forceK - 2f) / 8f;
                        dir.y = 0;
                        dir.Normalize();

                        if (ballistic)
                        {
                            Vector3 tangent = Vector3.Cross(Vector3.up, dir);

                            dir = Quaternion.AngleAxis(45, tangent) * dir;
                            forceToApply = -dir * Mathf.Lerp(100, 300, forceK);
                            State = BallState.Launch;
                        }
                        else
                        {
                            forceToApply = -dir * Mathf.Lerp(100, 300, forceK);
                            State = BallState.Hit;
                        }

                        Debug.Log("d = " + forceToApply.magnitude);

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
            //  Vector3 velocity = GetComponent<Rigidbody>().velocity;
            //  float distance = velocity.magnitude * Time.deltaTime;
            //  Vector3 tangent = Vector3.Cross(Vector3.up, velocity);

            //  float angle = distance / 0.1f;


            //  transform.Rotate(tangent, Mathf.Rad2Deg * angle);
            // angularVelocity = angle;
        }
        if (state == BallState.Air)
        {
            //Vector3 velocity = GetComponent<Rigidbody>().velocity;
            //   Vector3 tangent = Vector3.Cross(Vector3.up, velocity);

            //   transform.Rotate(tangent, Mathf.Rad2Deg * angularVelocity);
            //   angularVelocity = Mathf.Lerp(angularVelocity, 0, Time.deltaTime);
        }
    }




    void FixedUpdate()
    {
        if (State == BallState.Air)
        {
            GetComponent<Rigidbody>().drag = AirDragDefault; 
            if (inCollisionCount > 0)
                State = BallState.Ground;
        }
        else if (State == BallState.Ground)
        {
            GetComponent<Rigidbody>().drag = dragModifier.dragK;
            //  GetComponent<Rigidbody>().drag = GroundDragDefault;
            if (inCollisionCount <= 0)
                State = BallState.Air;
        }
        if (State == BallState.Still)
        {

        }
        if (State == BallState.Launch)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(forceToApply, ForceMode.Force);
            ballHitEvent.Invoke();
            State = BallState.Air;
        }
        if (State == BallState.Hit)
        {

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(forceToApply, ForceMode.Force);
            ballHitEvent.Invoke();
            State = BallState.Ground;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inCollisionCount++;
        // Debug.Log("collider = " + other.name);
        Hole hole = other.GetComponent<Hole>();
        if (hole != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            ballHoleEvent.Invoke();
        }

        DragModifier dragModifier = other.GetComponent<DragModifier>();
        if (dragModifier != null)
        {
            dragKStack[other.gameObject.GetInstanceID()] = dragModifier.dragK;
          
        }
    }

    void OnTriggerExit(Collider other)
    {
        inCollisionCount--;
        DragModifier dragModifier = other.GetComponent<DragModifier>();
        if (dragModifier != null)
        {
            dragKStack.Remove(other.gameObject.GetInstanceID());
         
        }
    }

    

    void OnCollisionEnter(Collision other)
    {
        inCollisionCount++;

        DragModifier dragModifier = other.gameObject.GetComponent<DragModifier>();
        if (dragModifier != null)
        {
            dragKStack[other.gameObject.GetInstanceID()] = dragModifier.dragK;            
        }
        //   Debug.Log("collider = " + other.gameObject.name);
    }

    void OnCollisionExit(Collision other)
    {
        inCollisionCount--;
        DragModifier dragModifier = other.gameObject.GetComponent<DragModifier>();
        if (dragModifier != null)
        {
            dragKStack.Remove(other.gameObject.GetInstanceID());            
        }
        //   Debug.Log("collider = " + other.gameObject.name);
    }
}
