using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControl : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("Drop Mode");
            player.GetComponent<Drop>().enabled = !player.GetComponent<Drop>().enabled;
        }

        if (Input.GetKeyDown("q"))
        {
            player.GetComponent<PlayerMove>().enabled = !player.GetComponent<PlayerMove>().enabled;
            player.GetComponent<Shoot>().enabled = !player.GetComponent<Shoot>().enabled;
            Debug.Log("Shoot Mode");
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
    }



}
