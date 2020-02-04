using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMech : MonoBehaviour
{
    Component sc;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "RotateBoard")
        {
            Debug.Log("awsl");
            col.gameObject.AddComponent<RotateBoard>();
        }
        else {
            Debug.Log("noononono");

        }
    }

    void OnCollisionExit(Collision col)
    {
        Debug.Log(col.gameObject.name);
        Destroy(col.gameObject.GetComponent<RotateBoard>());
    }

}
