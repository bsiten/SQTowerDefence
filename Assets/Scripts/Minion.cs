using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{
    public GameObject follower;
    GameObject targetEnemy;

    [SerializeField] float followerStayDistance;
    [SerializeField] float targetStayDistance;
    // [SerializeField] float speed = 10;

    bool isAtacking = false;

    public new void Start()
    {
        base.Start();
    }

    public new void Update()
    {
        if (isAtacking && targetEnemy != null)
        {
            Attacking(targetEnemy.transform.position);
        }
        else if (follower != null)
        {
            Follow(follower.transform.position);
        }

        base.Update();
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
}