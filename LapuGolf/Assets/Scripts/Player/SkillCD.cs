using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCD : MonoBehaviour
{
    public int TotalTime = 60;

    void Start()
    {
        StartCoroutine(Count());
    }

    private void Update()
    {
       
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
