using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{

    public float radius = 10f;   //定义一个要添加爆炸力的半径
    public float power = 600f;   //定义一个爆炸力
    public GameObject particle;   //得到播放粒子特效的物体

    public int TotalTime = 60;
    public AudioSource boom;

    void Start()
    {
        StartCoroutine(Count());
    }

    // Update is called once per frame
    void Update()
    {
        if (TotalTime == 0)
        {
            boom.Play();
            Debug.Log("boom");

            //Physics.OverlapSphere（）：球体投射，给定一个球心和半径，返回球体投射到的物体的碰撞器
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hits in colliders)  //遍历碰撞器数组
            {
                //如果这个物体有刚体组件
                if (hits.GetComponent<Rigidbody>())
                {
                    hits.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //可移动
                                                                                            //给定爆炸力大小，爆炸点，爆炸半径
                                                                                            //利用刚体组件添加爆炸力AddExplosionForce
                    hits.GetComponent<Rigidbody>().AddExplosionForce(power, hits.transform.position, radius);
                }
            }

            Destroy(gameObject);
        }

    }

    IEnumerator Count()
    {
        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);
            TotalTime--;
        }

    }
}
