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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
       if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                   rb.AddForce(transform.forward * thrust*100);
                }
            }
        }
          
    }

}
