// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffencePlayer : Entity
{
    // Vector3 vel = Vector3.zero;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate;
    [SerializeField] float fireDistance;
    float fireInterval;


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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 4") || Input.GetKey("joystick button 5"))
        {
            if (fireInterval < 0)
            {
                Fire(new Vector3(transform.forward.x, 0, transform.forward.z) * fireDistance + transform.position);
                fireInterval = fireRate;
            }
        }
        //move
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
        fireInterval -= Time.deltaTime;

    }

    void Fire(Vector3 target)
    {
        // var attackObject = Instantiate(bullet, transform.position + transform.forward * fireDistance, transform.rotation);
        var fire_bullet = GameObject.Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        var comp_bullet = fire_bullet.GetComponent<BulletBase>();
        comp_bullet.SetInpactPoint(target);
    }
}
