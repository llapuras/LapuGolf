using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{
    Rigidbody x;
    private void Start()
    {
        x = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Rotate(x);
    }

    private void Rotate(Rigidbody rb)
    {
        if (Input.GetKey("w"))
        {
            rb.transform.Rotate(0.0f, 0.0f, 3.0f, Space.World);
        }
        else if (Input.GetKey("s"))
        {
            rb.transform.Rotate(0.0f, 0.0f, -3 * 1.0f, Space.World);
        }
    }
}
