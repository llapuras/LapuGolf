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
    public Rigidbody rb;
    GameObject powerBar;
    GameObject canv;

    private float maxbar_len;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        canv = GameObject.Find("Canvas");

        powerBar = Instantiate(Resources.Load("Prefabs/PowerBar") as GameObject, new Vector3(0,0,0), Quaternion.identity);
        powerBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    void FixedUpdate()
    {
       if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (thrust <= 10)
                    {
                        thrust++;
                        Debug.Log(thrust);
                    }
                    else thrust = 10;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            rb.AddForce(transform.up * thrust * 100);
            thrust = 0;
        }


    }

}
