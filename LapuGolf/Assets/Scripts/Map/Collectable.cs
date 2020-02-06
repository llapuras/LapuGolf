using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    public string nextLevelScene;
    public int golacoincount = 0; 
    GameObject nextLevel;
    int coinCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        nextLevel = GameObject.FindGameObjectWithTag("NextLevel");

        if (coinCount == golacoincount)
        {
            nextLevel.GetComponent<Light>().enabled = true;
            nextLevel.GetComponent<BoxCollider>().enabled = true;
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("coin"))
        {
            Destroy(col.gameObject);
            coinCount++;
        }

        if (col.gameObject.tag == "NextLevel")
        {
            nextLevel.GetComponent<Light>().enabled = false;
            nextLevel.GetComponent<BoxCollider>().enabled = false;

            SceneManager.LoadScene(nextLevelScene);
        }
    }
}
