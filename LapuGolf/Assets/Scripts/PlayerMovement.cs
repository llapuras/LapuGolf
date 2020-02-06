using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1.click object and add force
//2. hold mouse add powerup force
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float thrust = 1.0f;
    public float max_thrust = 10.0f;
    public Rigidbody rb;
    GameObject powerBar;
    GameObject canv;

    LineRenderer dirline;

    Vector3 direction;

    private float maxbar_len;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirline = GetComponent<LineRenderer>();
        canv = GameObject.Find("Canvas");

        powerBar = Instantiate(Resources.Load("Prefabs/PowerBar") as GameObject, new Vector3(0,0,0), Quaternion.identity);
        powerBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    void FixedUpdate()
    {
        //ClickAddForce();

        DirectionAddForce();

    }

    void ClickAddForce()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == "Player")
            {
                if (Input.GetMouseButton(0))
                {

                    if (thrust <= max_thrust)
                    {
                        thrust++;
                        Debug.Log(thrust);
                    }
                    else thrust = max_thrust;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    rb.AddForce(transform.up * thrust * 100);
                    thrust = 0;
                }
            }
        }

    }

    void DirectionAddForce() {

 

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;
    
        
        if (Input.GetMouseButton(0))
        {
            if (thrust <= max_thrust)
            {
                thrust++;
            }
            else thrust = max_thrust;

            dirline.positionCount = 2;
            dirline.SetPosition(0, rb.transform.position);
            dirline.SetPosition(1, pos);

        }

        if (Input.GetMouseButtonUp(0))
        {
            dirline.positionCount = 0;
            direction = pos - rb.transform.position;
            rb.AddForce(direction * thrust * 100);
            thrust = 0;
        }

    }

}


