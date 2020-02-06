using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootBall : MonoBehaviour
{
    public float thrust = 1.0f;
    public float max_thrust = 10.0f;
    public float speed = 15.0f;
    public float jumpspeed = 0.1f;

    private Rigidbody rb;
    private LineRenderer dirline;

    private Vector3 direction;
    private Vector3 pos;
    Vector3 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirline = GetComponent<LineRenderer>();
    }


    void FixedUpdate()
    {

        if (Input.GetKey("w"))
            dir = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        rb.AddForce(dir * speed * 10);

        if (Input.GetKey("s"))
            rb.AddForce(-1 * dir * speed * 10);

        if (Input.GetKey("a"))
            rb.AddForce(-1 * Camera.main.transform.right * speed * 10);

        if (Input.GetKey("d"))
            rb.AddForce(Camera.main.transform.right * speed * 10);

        if (Input.GetKey("space"))
        {
            rb.AddForce(Vector3.up * jumpspeed * 5);
        }

        shootSelf();
    }

    void shootSelf()
    {
        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.name == "Ground")
                {

                    if (thrust <= max_thrust)
                    {
                        thrust += 0.2f;
                    }
                    else thrust = max_thrust;

                    dirline.positionCount = 2;
                    dirline.SetPosition(0, rb.transform.position);
                    dirline.SetPosition(1, hit.point);
                    pos = hit.point;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            direction = pos - rb.transform.position;
            dirline.positionCount = 0;

            Vector3 newpos = rb.transform.position;
            newpos.y += 1;

            rb.AddForce(direction * thrust * 100);
            thrust = 0;
        }

    }

}

