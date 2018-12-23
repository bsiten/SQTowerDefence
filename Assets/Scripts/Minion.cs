using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{
    public GameObject follower;
    GameObject targetEnemy;
    float speed = 0;

    bool isAtacking = false;

    public new void Start()
    {

        base.Start();
    }

    public new void Update()
    {
        if (isAtacking)
        {
            Follow(targetEnemy.transform.position);
        }
        else
        {
            Follow(follower.transform.position);
        }

        base.Update();
    }
    protected void Follow(Vector3 target)
    {

    }
}