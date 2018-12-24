// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffencePlayer : Entity
{
    // Vector3 vel = Vector3.zero;
    [SerializeField] GameObject mainCamera;


    // Start is called before the first frame update
    new void Start()
    {

        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        Vector3 vel = new Vector3();
        var right = mainCamera.transform.right;
        var forward = new Vector3(-right.z, 0, right.x);
        //keyboard controll
        if (Input.GetKey(KeyCode.W))
        {
            // vel -= transform.forward * speed;
            vel += forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // vel += transform.forward * speed;
            vel -= forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // vel += transform.right * speed;
            vel -= mainCamera.transform.right * speed;

        }
        if (Input.GetKey(KeyCode.D))
        {
            // vel -= transform.right * speed;
            vel += mainCamera.transform.right * speed;
        }
        //gamepad controll
        vel += Input.GetAxis("Horizontal") * right * speed;
        vel += Input.GetAxis("Vertical") * forward * speed;
        m_velocity = vel;
        base.Update();
        vel = Vector3.zero;
    }
}
