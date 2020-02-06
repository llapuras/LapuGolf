using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    private LineRenderer dirline;
    private Rigidbody rb;
    private Vector3 direction;
    private Vector3 shootPos;

    public Text ability;
    public Text cd;

    public float shootrange = 10;
    public string Ability = null; //player's ability
    Object item;

    GameObject it;

    // Start is called before the first frame update
    void Start()
    {
        dirline = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Ability == "Bomb")
        {
            item = Resources.Load("Prefab/003/bomb") as GameObject;
        }


        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.tag == "Ground")
                {
                    Debug.Log(Vector3.Distance(hit.point, rb.transform.position));
                    Debug.Log(shootrange);
                    //射击范围内，才会出现预测线，才可射击
                    if (Vector3.Distance(hit.point, rb.transform.position) <= shootrange)
                    {
                        shootPos = hit.point;
                        Debug.Log("in range");
                        dirline.positionCount = 2;
                        dirline.SetPosition(0, rb.transform.position);
                        dirline.SetPosition(1, shootPos);
                    }

                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            direction = shootPos - rb.transform.position;
            direction.y = 2;

            shootPos.y = 1.2f;

            if (dirline.positionCount != 0)
            {
                it = Instantiate(item, shootPos, Quaternion.identity) as GameObject;
                it.GetComponent<BoxCollider>().enabled = false; //放置的炮弹不可触碰，只会在投掷位置到点爆炸
                dirline.positionCount = 0;
            }
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Bomb"))
        {

            if (Ability != "Bomb")
            {
                //Destroy(col.gameObject);
                Ability = "Bomb";
                ability.text = "Bomb";
            }
            else if(Ability == "Bomb")
            {

            }
        }
    }

}
