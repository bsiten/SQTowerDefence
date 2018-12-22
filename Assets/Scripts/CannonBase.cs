using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBase : MonoBehaviour
{

    Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("debug");

    }

}
