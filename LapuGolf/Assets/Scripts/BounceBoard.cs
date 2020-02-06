using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBoard : MonoBehaviour
{
    public float bouncingForce = 10;
    public Vector3 direction = new Vector3(1, 0, 0);
    private Rigidbody rb;

    // if ball is collide at Bounce Board, it will bounce up
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){


    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        
        if (col.gameObject.tag == ("Player"))
        {
            Debug.Log("awsl");
            addF(rb, direction);
        }
        else {
            Debug.Log("nononono");
        }
    }

    private void addF(Rigidbody rb, Vector3 dir) {
        rb.AddForce(dir * bouncingForce * 100);
    }
}
