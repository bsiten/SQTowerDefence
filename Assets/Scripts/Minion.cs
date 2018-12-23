using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{
    public GameObject leader;
    public GameObject follower;
    GameObject targetEnemy;

    [SerializeField] float followerStayDistance;
    [SerializeField] float targetStayDistance;
    // [SerializeField] float speed = 10;
    [SerializeField] bool isCharge = true;//charge or not 

    bool isAtacking = false;

    public new void Start()
    {
        base.Start();
    }

    public new void Update()
    {
        if (isAtacking && isCharge && targetEnemy != null)
        {
            Attacking(targetEnemy.transform.position);
        }
        else if (leader != null)
        {
            Follow(leader.transform.position);
        }

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
    protected void Attacking(Vector3 target)
    {
        var xzTarget = new Vector3(target.x, transform.position.y, target.z) - transform.position;
        if (xzTarget.magnitude > targetStayDistance)
        {
            m_velocity = xzTarget.normalized * speed;
        }
        else { m_velocity = Vector3.zero; }
    }
    public bool ToggleAttacking()
    {
        isAtacking = (!isAtacking);
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
        Debug.Log("Minion dead");
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