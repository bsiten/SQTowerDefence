using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : Entity
{
    // [SerializeField] protected float speed = 10;
    [SerializeField] protected float aliveTime = 10;
    [SerializeField] GameObject explodeObject = null;
    [SerializeField] Vector3 inpactPoint = Vector3.zero;

    float m_decreaseHealth;
    [SerializeField] bool isGravity = true;
    [SerializeField] float gravity = 1;
    float m_inpact_time;



    public new void Start()
    {
        base.Start();
        CalculateVelocity();
        m_decreaseHealth = this.status.maxHealth / aliveTime;
        var pos = transform.position;
        m_inpact_time = (new Vector2(pos.x - inpactPoint.x, pos.z - inpactPoint.z)).magnitude / speed;
        // Debug.Log(transform.name + " is constructed");
    }

    public new void Update()
    {
        status.health -= m_decreaseHealth * Time.deltaTime;
        UpdateVelocity();
        // base.Update();
        StatusCheck();
        BuffProcess();
        Move();
    }

    protected void StatusCheck()
    {
        //死亡処理
        if (status.health <= 0)
        {
            Dead();
        }
    }
    // void OnCollisionEnter(Collision other)
    void OnTriggerEnter(Collider other)
    {
        var layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName == "Floor")
        {
            Explode();
        }
    }

    // public method
    public void SetInpactPoint(Vector3 p)
    {
        inpactPoint = p;
        CalculateVelocity();
    }

    //概要:
    //  着弾・爆破についてを処理
    protected void Explode()
    {
        if (explodeObject != null)
        {
            var pos = transform.position;
            // Instantiate(explodeObject, transform.position, transform.rotation);
            Instantiate(explodeObject, new Vector3(pos.x, 0, pos.z), new Quaternion(1, 0, 0, 0));
            Destroy(transform.gameObject);
        }
    }
    //概要
    //  放物線を描くように速度をupdate
    protected void UpdateVelocity()
    {
        if (isGravity == true)
        {
            m_velocity.y -= gravity * Time.deltaTime;
        }
    }
    //概要
    //  所定のスピードで着弾点に到着する速度を計算する．
    protected new void CalculateVelocity()
    {
        // var pos = transform.position;
        // m_velocity = new Vector3(inpactPoint.x - pos.x, 0, inpactPoint.z - pos.z);
        // if (isGravity)
        // {
        //     var pos = transform.position;
        //     var xzDistance = new Vector3(inpactPoint.x - pos.x, 0, inpactPoint.z - pos.z);
        //     var xzVelocity = xzDistance.normalized * speed;
        //     var inpactTime = xzDistance.magnitude / speed;
        //     var yVelocity = (inpactPoint.y - pos.y) / inpactTime + gravity * inpactTime;
        //     m_velocity = new Vector3(xzVelocity.x, yVelocity, xzVelocity.z);
        // }
        // else
        // {
        m_velocity = (inpactPoint - transform.position).normalized * speed;
        // }
    }

    //概要
    // velocity 指定では正確に着弾しないので別個で重力を計算
    protected void fallBullet()
    {
        // transform.position.y -= 
    }

    protected new void Dead()
    {
        Explode();
        base.Dead();
    }
}