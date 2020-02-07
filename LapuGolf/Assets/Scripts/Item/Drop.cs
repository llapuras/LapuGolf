using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drop : MonoBehaviour
{
    GameObject player;
    Vector3 dropPos;
    public string Ability = null; //player's ability
    Object item;

    public Text ability;
    public Text cd;
    public int skillcd = 10;

    public GameObject noticeUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //GetComponent<SkillCD>().TotalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

       // cd.text = GetComponent<SkillCD>().TotalTime.ToString(); 

        if (Ability == "Bomb")
        {
            item = Resources.Load("Prefab/003/bomb") as GameObject;

            if (Input.GetKeyDown("space"))
            {
                //if (GetComponent<SkillCD>().TotalTime <= 0)
                {
                    dropPos = player.transform.position;
                    dropPos.z -= 2;
                    GameObject it = Instantiate(item, dropPos, Quaternion.identity) as GameObject;
                    it.GetComponent<BoxCollider>().enabled = false;
                    //GetComponent<SkillCD>().TotalTime = skillcd;
                }
                //else
                //{
                //    Debug.Log("not the time");
                //}
            }
        }



    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Bomb"))
        {
            Destroy(col.gameObject);
            Ability = "Bomb";
            //ability.text = "Bomb";
            noticeUI.SetActive(true);

        }
    }
}
