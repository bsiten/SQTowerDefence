using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffencePlayer : Entity
{
    public float speed = 0.2f;
    Vector3 vel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            vel -= transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel += transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel += transform.right * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel -= transform.right * speed;
        }
        m_velocity = vel;
        base.Update();
        vel = Vector3.zero;
    }
}
