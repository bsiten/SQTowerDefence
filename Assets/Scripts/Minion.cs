using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{
    public GameObject leader;
    public GameObject follower;
    // GameObject targetEnemy;
    [SerializeField] GameObject bullet;
    [SerializeField] float followerStayDistance;
    [SerializeField] float targetStayDistance;
    // [SerializeField] float speed = 10;
    [SerializeField] bool isCharge = true;//charge or not
    [SerializeField] float fireRate;

    DetectRange targetRange;
    DetectRange attackRange;
    float fireInterval;

    bool isAtacking = false;

    public new void Start()
    {
        base.Start();
        foreach (Transform child in transform)
        {
            if (child.name == "TargetRange")
            {
                targetRange = child.GetComponent<DetectRange>();
            }
            if (child.name == "AttackRange")
            {
                attackRange = child.GetComponent<DetectRange>();
            }
        }
    }

    public new void Update()
    {
        //velocity setting
        if (isAtacking && isCharge)
        {
            var targetObject = NearestObject(targetRange);
            if (targetObject != null)
            {
                // Debug.Log("Attacking called");
                Charging(targetObject.transform.position);

            }
            else
            {
                m_velocity = Vector3.zero;
            }
        }
        else if (leader != null)
        {
            Follow(leader.transform.position);
        }
        if (isAtacking || !isCharge)
        {
            var attackObject = NearestObject(attackRange);
            if (attackObject != null && bullet != null && fireInterval < 0)
            {
                var target = attackObject.transform.position;
                Fire(new Vector3(target.x, 0, target.z));
                fireInterval = fireRate;
            }
        }
        fireInterval -= Time.deltaTime;

        // base.Update();
        StatusCheck();
        BuffProcess();
        Move();
    }
    protected void Follow(Vector3 target)
    {
        var xzTarget = new Vector3(target.x, transform.position.y, target.z) - transform.position;
        if (xzTarget.magnitude > followerStayDistance)
        {
            m_velocity = xzTarget.normalized * speed;
        }
        else
        {
            m_velocity = Vector3.zero;
        }

    }
    protected void Charging(Vector3 target)
    {
        var xzTarget = new Vector3(target.x, transform.position.y, target.z) - transform.position;
        if (xzTarget.magnitude > targetStayDistance)
        {
            m_velocity = xzTarget.normalized * speed;
        }
        else { m_velocity = Vector3.zero; }
    }
    protected void Fire(Vector3 target)
    {
        // Debug.Log("Minion Fire called");
        var fire_bullet = GameObject.Instantiate(bullet, transform.position, transform.rotation);
        var comp_bullet = fire_bullet.GetComponent<BulletBase>();
        comp_bullet.SetInpactPoint(target);
    }
    public bool ToggleAttacking()
    {
        isAtacking = (!isAtacking);
        return isAtacking;
    }
    public bool ToggleAttacking(bool attack)
    {
        isAtacking = attack;
        return isAtacking;
    }

    protected new void StatusCheck()
    {
        //死亡処理
        if (status.health <= 0)
        {
            this.Dead();
        }
    }
    protected new void Dead()
    {
        // Debug.Log("Minion dead");
        if (follower.tag == "Minion")
        {
            var comp = follower.GetComponent<Minion>();
            comp.leader = leader;
        }
        if (leader.tag == "Minion")
        {
            var comp = leader.GetComponent<Minion>();
            comp.follower = follower;
        }
        var player = GameObject.FindWithTag("AttackPlayer");
        var playercomp = player.GetComponent<AttackPlayer>();
        playercomp.minionList.Remove(transform.gameObject);
        playercomp.minionCompList.Remove(this);
        base.Dead();
    }
}