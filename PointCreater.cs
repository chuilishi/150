using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreater : MonoBehaviour
{
    public GameObject point;
    public float frequence = 0.1f;
    private void Start()
    {
        StartCoroutine(draw());
    }
    
    IEnumerator draw()
    {
        while (true)
        {
            Instantiate(point, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(frequence);
        }
    }
}
