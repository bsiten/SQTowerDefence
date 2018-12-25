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
    [SerializeField] Vector3 launchPoint;
    float fireInterval;

    public int PlayerID;

    public LayerMask mask;

    public int CannonLimit = 8;
    public int NowCannonNum = 0;

    // Start is called before the first frame update
    public new void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        Vector3 vel = new Vector3();
        var right = mainCamera.transform.right;
        var forward = new Vector3(-right.z, 0, right.x);
        //keyboard controll
        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 4") || Input.GetKey("joystick button 5"))
        {
            if (fireInterval < 0)
            {
                // Fire(new Vector3(transform.forward.x, 0, transform.forward.z) * fireDistance + transform.position);
                Fire(transform.forward * fireDistance + transform.position + launchPoint);
                // Fire((transform.forward + launchPoint) * fireDistance + transform.position);
                fireInterval = fireRate;
            }
        }

        // 1.
        // Rayの作成
        // Ray ray = new Ray(transform.position + transform.up * 0.5f, transform.forward + new Vector3(0, -0.6f, 0));
        Ray ray = new Ray(transform.position + transform.up * 0.7f, transform.forward + new Vector3(0, -0.6f, 0));

        // 2.		
        // Rayが衝突したコライダーの情報を得る
        RaycastHit hit;
        // Rayが衝突したかどうか
        if (Physics.Raycast(ray, out hit, mask))
        {
            // Examples
            // 衝突したオブジェクトの色を赤に変える
            var plane = hit.collider.GetComponent<Plane>();
            //hit.collider.GetComponent<Plane>().IsColorChange = true;
            if (plane != null)
            {
                hit.collider.GetComponent<Plane>().IsColorChange = true;

                //B button
                if (!hit.collider.GetComponent<Plane>().IsLocated && NowCannonNum <= CannonLimit)
                {
                    if (Input.GetKey("joystick button 1") || Input.GetKey(KeyCode.R))
                    {
                        hit.collider.GetComponent<Plane>().LocateObject = (GameObject)Resources.Load("Cannon");
                        //GameObject Instant = Instantiate(obj, hit.collider.GetComponent<Plane>().transform.position, Quaternion.identity);
                        //Cannons.Add(Instant);
                        hit.collider.GetComponent<Plane>().LocateCannon();
                        ++NowCannonNum;
                    }
                    if (Input.GetKey("joystick button 2") || Input.GetKey(KeyCode.T))
                    {
                        hit.collider.GetComponent<Plane>().LocateObject = (GameObject)Resources.Load("Cannon_Beam");
                        //GameObject Instant = Instantiate(obj, hit.collider.GetComponent<Plane>().transform.position, Quaternion.identity);
                        //Cannons.Add(Instant);
                        hit.collider.GetComponent<Plane>().LocateCannon();
                        ++NowCannonNum;
                    }
                    if (Input.GetKey("joystick button 3") || Input.GetKey(KeyCode.Y))
                    {
                        hit.collider.GetComponent<Plane>().LocateObject = (GameObject)Resources.Load("Cannon_SlowRange");
                        //GameObject Instant = Instantiate(obj, hit.collider.GetComponent<Plane>().transform.position, Quaternion.identity);
                        //Cannons.Add(Instant);
                        hit.collider.GetComponent<Plane>().LocateCannon();
                        ++NowCannonNum;
                    }
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 1.0f);

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
        var fire_bullet = GameObject.Instantiate(bullet, transform.position + launchPoint + transform.forward, transform.rotation);
        var comp_bullet = fire_bullet.GetComponent<BulletBase>();
        comp_bullet.SetInpactPoint(target);
    }
}
