using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTestEntity : MonoBehaviour
{
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos += velocity * Time.deltaTime;
        transform.position = pos;
    }


}
