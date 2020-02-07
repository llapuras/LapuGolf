using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectTask : MonoBehaviour
{
    public int Goal = 12;
    public bool shouldActiveCube = false;

    private int Count = 0;

    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Count == Goal)
        {
            shouldActiveCube = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Collectible")
        {
            Destroy(col.gameObject);
            Count++;
            countText.text = Count.ToString();
        }
    }
}
