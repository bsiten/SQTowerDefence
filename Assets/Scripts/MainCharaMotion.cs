using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCharaMotion : MonoBehaviour
{

    public float Dist = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, Mathf.PingPong(Time.deltaTime, Dist), this.transform.position.z);
    }
}
