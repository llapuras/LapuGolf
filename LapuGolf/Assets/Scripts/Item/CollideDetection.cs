using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDetection : MonoBehaviour
{

    public GameObject loadedEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter (Collider col)
    {
         if(col.gameObject.tag == "Player")
        {
            Debug.Log("enter");
            loadedEffect = Instantiate(Resources.Load("Effects/Cartoon_Fire2"), col.gameObject.transform.position, Quaternion.identity) as GameObject;
            loadedEffect.transform.SetParent(col.gameObject.transform);
        }
    }
}
