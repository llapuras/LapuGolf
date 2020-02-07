using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    public Vector3 pos0;
    public Vector3 pos1;
    public float speed = 20;

    //private float stoptime = 15;
    //private float count;

    private float pingpongx;
    private float pingpongy;
    private float pingpongz;

    private float[,] storage;
    private int mode;

    private string debugtxt;

    // Start is called before the first frame update
    void Start()
    {
        //count = stoptime;
        storage = new float[3, 2];

        storage[0, 0] = pos0.x;
        storage[0, 1] = pos1.x;
        storage[1, 0] = pos0.y;
        storage[1, 1] = pos1.y;
        storage[2, 0] = pos0.z;
        storage[2, 1] = pos1.z;

        mode = Mode1Search();

    }

    int Mode1Search()
    {
        for (int i = 0; i < 3; i++)
        {
            if (storage[i, 0] != storage[i, 1])
            {
                //Debug.Log(i);
                return i;//必然会返回一个值
            }
        }
        Debug.Log("本脚本不适用于相同点，请修改！");
        return 3;
    }

    // Update is called once per frame
    void Update()
    {
        FinalCaculate();
        debugtxt = pingpongx + "," + pingpongy + "," + pingpongz;
        //Debug.Log(debugtxt);
        transform.localPosition = new Vector3(pingpongx, pingpongy, pingpongz);

    }

    void FinalCaculate()
    {
        if (mode  == 0)
        {
            pingpongx = CaculatePingPong(pos0.x, pos1.x);
            pingpongy = CaculateOther(pos0.y, pos1.y, pos0.x, pos1.x, pingpongx);
            pingpongz = CaculateOther(pos0.z, pos1.z, pos0.x, pos1.x, pingpongx);
        }
        else if(mode == 1)
        {
            pingpongy = CaculatePingPong(pos0.y, pos1.y);
            pingpongx = CaculateOther(pos0.x, pos1.x, pos0.y, pos1.y, pingpongy);
            pingpongz = CaculateOther(pos0.z, pos1.z, pos0.y, pos1.y, pingpongy);
        }
        else if(mode == 2)
        {
            pingpongz = CaculatePingPong(pos0.z, pos1.z);
            pingpongx = CaculateOther(pos0.x, pos1.x, pos0.z, pos1.z, pingpongz);
            pingpongy = CaculateOther(pos0.y, pos1.y, pos0.z, pos1.z, pingpongz);
        }

    }

    float CaculatePingPong(float posn1, float posn2)
    {
        if (posn1 != posn2)
        {
            return Mathf.Max(posn1, posn2) - Mathf.PingPong(Time.time * speed, Mathf.Abs(posn1 - posn2));
        }
        else
        {
            return posn2;
        }
    }

    float CaculateOther(float posn1, float posn2, float posx1, float posx2, float pp)
    {
        if (Mathf.Abs(posx1 - posx2) != 0 && posn1 != posn2) 
        {
            //Debug.Log((Mathf.Max(posx1, posx2) - pp));
            return Mathf.Max(posn1, posn2) - Mathf.Abs(posn1 - posn2) * ((Mathf.Max(posx1, posx2) - pp) / Mathf.Abs(posx1 - posx2));
        }
        else
            return posn2;
    }
}
