using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{

    public Vector3 pos0;
    public Vector3 pos1;
    public float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards( pos0, pos1, Time.deltaTime * speed);
        Debug.Log(transform.position);
    }

}
