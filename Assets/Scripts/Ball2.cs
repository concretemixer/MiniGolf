using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball2 : MonoBehaviour
{

    private float Radius = 0.1f;

    enum BallState
    {
        Launch,
        Air,
        Hit,
        Ground,
        Still,
        Sinking
    }


    Vector3 forceToApply = Vector3.zero;
    BallState state = BallState.Air;
    float timeInState = 0;
    float angularVelocity = 0;
    float stillTimer = 0;

    bool aiming = false;
    bool ballistic = false;

    int inCollisionCount = 0;

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
        if (state == BallState.Air || state == BallState.Sinking)
        {
            //Vector3 velocity = GetComponent<Rigidbody>().velocity;
            //   Vector3 tangent = Vector3.Cross(Vector3.up, velocity);

            //   transform.Rotate(tangent, Mathf.Rad2Deg * angularVelocity);
            //   angularVelocity = Mathf.Lerp(angularVelocity, 0, Time.deltaTime);
        }
    }




    void FixedUpdate()
    {
        if (State == BallState.Sinking)
        {

        }
        if (State == BallState.Air)
        {

        }
        if (State == BallState.Ground)
        {

        }
        if (State == BallState.Still)
        {

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

    /*
void OnTriggerEnter(Collider other)
{
inCollisionCount++;
// Debug.Log("collider = " + other.name);
Hole hole = other.GetComponentInParent<Hole>();
if (hole != null)
{
  Vector3 v = GetComponent<Rigidbody>().velocity;
  GetComponent<Rigidbody>().velocity = v * 0.3f;
  State = BallState.Sinking;
  foreach (var c in hole.GetComponentsInChildren<BoxCollider>())
      c.enabled = true;
}

Wall wall = other.GetComponentInParent<Wall>();
if (wall != null)
{
  Debug.Log("Wall = " + other.gameObject.name);
  Vector3 normal = wall.GetNormal(other.ClosestPoint(transform.position));
  Vector3 v = GetComponent<Rigidbody>().velocity;
  if (Vector3.Dot(v, normal) > 0)
  {
      Vector3 vn = Vector3.Project(v, normal);
      v -= vn;
      v -= vn * wall.normalBounceK;
      GetComponent<Rigidbody>().velocity = v;
  }
}

Floor floor = other.GetComponentInParent<Floor>();
if (floor != null)
{
  Vector3 normal = floor.GetNormal(other.ClosestPoint(transform.position));
  Vector3 v = GetComponent<Rigidbody>().velocity;

  if (State == BallState.Air)
  {
      Vector3 vn = Vector3.Project(v, normal);
      v -= vn;

      v -= vn * floor.normalBounceK;

      GetComponent<Rigidbody>().velocity = v;

      if (floor.normalBounceK == 0)
          state = BallState.Ground;
  }
  else if (State == BallState.Ground)
  {
      Vector3 vn = Vector3.Project(v, normal);
      float dir = Vector3.Dot(v, normal);
      if (dir > 0) //  fall
      {
          //state = BallState.Air;
          //GetComponent<Rigidbody>().velocity = Vector3.zero;
      }
      if (dir < 0)  //  climb
      {
          v -= vn;
          GetComponent<Rigidbody>().velocity = v;
      }


  }
}

}

void OnTriggerExit(Collider other)
{
inCollisionCount--;
//Debug.Log("collider out = " + other.name);
}
*/

    void OnCollisionEnter(Collision other)
    {
     //   Debug.Log("collider = " + other.gameObject.name);
    }
}
