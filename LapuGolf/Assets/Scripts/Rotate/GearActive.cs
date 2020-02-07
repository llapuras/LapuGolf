using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearActive : MonoBehaviour
{

    public AudioSource swicth;
    private Vector3 origin;
    private Vector3 mid;

    public GameObject SwitchBoard;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        mid = origin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Block")
        {
            swicth.Play();
            mid.y -= 0.2f;
            transform.position = mid;
            SwitchBoard.GetComponent<selfrotate>().enabled = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        transform.position = origin;
        SwitchBoard.GetComponent<selfrotate>().enabled = false;
    }
}
