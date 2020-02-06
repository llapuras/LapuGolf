using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    public Vector3 pos0;
    public Vector3 pos1;
    public float speed = 10;
    private float stoptime = 15;

    private float pingpongx;
    private float pingpongy;
    private float pingpongz;

    private float count;

    // Start is called before the first frame update
    void Start()
    {
        count = stoptime;
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 0)
        {
            pingpongx = CaculatePingPong(pos0.x, pos1.x);
            pingpongy = Mathf.Max(pos0.y, pos0.y) - Mathf.Abs(posn1 - posn2) * ;
            pingpongz = CaculatePingPong(pos0.z, pos1.z);

            transform.localPosition = new Vector3(pingpongx, pingpongy, pingpongz);
        }

    }

    float CaculatePingPong(float posn1, float posn2)
    {
        if (posn1 != posn2)
            return Mathf.Max(posn1, posn2) - Mathf.PingPong(Time.time * speed, Mathf.Abs(posn1 - posn2));
        else return posn2;
    }

}
