using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{

    public AudioSource originBGM;
    public AudioSource winBGM;
    public bool isActiveOnAwake = true;
    public Text notice;
    public GameObject effecs;

    // Start is called before the first frame update
    void Start()
    {
        if(isActiveOnAwake == false)
        {
            effecs.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveOnAwake == true)
        {
            effecs.SetActive(true);
     
        }
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player" && winBGM.isPlaying == false)
        {
            if (isActiveOnAwake == true)
            {
                originBGM.Stop();
                winBGM.Play();
                ClearText();
            }
            else
            {
                notice.text = "The cube hasn't been activated."; 
            }

        }
    }

    void ClearText()
    {
        notice.text = "";
    }    
}
