using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public int TotalTime = 60;

    void Start()
    {
        StartCoroutine(Count());
    }

    private void Update()
    {
        if (TotalTime == 0)
        {
            Destroy(gameObject);
            Debug.Log("boom");
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
