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
    public float AirDragDefault = 0.2f;
    public float GroundDragDefault = 1.0f;
    public float LowGroundLevel = -5f;

    public BallHitEvent ballHitEvent = new BallHitEvent();
    public BallLostEvent ballLostEvent = new BallLostEvent();
    public BallStoppedEvent ballStoppedEvent = new BallStoppedEvent();
    public BallHoleEvent ballHoleEvent = new BallHoleEvent();   

    public enum BallState
    {
        None,
        Launch,
        Air,
        Hit,
        Ground,
        Still,
        Lost
    }


    Vector3 forceToApply = Vector3.zero;
    BallState state = BallState.Air;
    float timeInState = 0;
    float stillTimer = 0;


    bool aiming = false;
    bool ballistic = false;

    int inCollisionCount = 0;
    int inWater = 0;
    private Dictionary<int, float> dragKStack = new Dictionary<int, float>();

    public void SetBallistic(bool value)
    {
        ballistic = value;
    }

    public BallState State
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
                        ballHitEvent.Invoke();
                        aiming = false;

                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && !aiming)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,1000.0f, 0x100))
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
        timeInState += Time.fixedDeltaTime;
        if (State != BallState.Lost)
        {
            if (transform.position.y < LowGroundLevel)
            {
                State = BallState.Lost;
            }
        }
        else
        {
            if (timeInState > 0.5f)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                State = BallState.None;
                ballLostEvent.Invoke();
            }
        }

        if (State == BallState.Air)
        {
            GetComponent<Rigidbody>().drag = AirDragDefault; 
            if (inCollisionCount > 0)
                State = BallState.Ground;
        }
        else if (State == BallState.Ground)
        {
            if (dragKStack.Count == 0)
            {
                GetComponent<Rigidbody>().drag = GroundDragDefault;
            }
            else
            {
                float drag = 0;
                foreach (var k in dragKStack.Values)
                    if (k > drag)
                        drag = k;
                GetComponent<Rigidbody>().drag = drag;
            }

            if (inCollisionCount <= 0)
                State = BallState.Air;
            else
            {
                if (GetComponent<Rigidbody>().velocity.magnitude < 0.05f)
                {
                    stillTimer += Time.fixedDeltaTime;
                    if (stillTimer > 0.5f)
                    {
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        if (inWater>0)
                            State = BallState.Lost;
                        else
                            State = BallState.Still;
                    }
                }
                else
                    stillTimer = 0;
            }
        }
        if (State == BallState.Still)
        {            
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (State == BallState.Launch)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(forceToApply, ForceMode.Force);
            
            State = BallState.Air;
        }
        if (State == BallState.Hit)
        {

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(forceToApply, ForceMode.Force);
          
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

        if (other.GetComponent<Water>() != null)
            inWater++;
    }

    void OnTriggerExit(Collider other)
    {
        inCollisionCount--;
        DragModifier dragModifier = other.GetComponent<DragModifier>();
        if (dragModifier != null)
        {
            dragKStack.Remove(other.gameObject.GetInstanceID());         
        }
        if (other.GetComponent<Water>() != null)
            inWater--;

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
