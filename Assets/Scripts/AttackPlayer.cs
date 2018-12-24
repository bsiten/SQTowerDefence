﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : Entity
{
    // public float speed = 0.2f;

    public List<GameObject> chargeMinionList;
    public List<GameObject> supportMinionList;
    public List<Minion> chargeMinionCompList = new List<Minion>();
    Dictionary<Minion, Vector3> chargeMinionRandPos;
    public List<Minion> supportMinionCompList = new List<Minion>();
    Dictionary<Minion, Vector3> supportMinionRandPos;
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireDistance;
    [SerializeField] float smoothMoveTime = 1;
    [SerializeField] float stopDistance = (float)0.1;
    Vector3 current_m_vel;
    bool forceMinionsAttack;
    float fireInterval;

    // Start is called before the first frame update
    public new void Start()
    {
        initMinions();
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        GetPlayerInput();
        // CheckMinionList();
        // base.Update();
        BuffProcess();
        StatusCheck();
        Move();
        fireInterval -= Time.deltaTime;
    }
    protected new void StatusCheck()
    {
        base.StatusCheck();
        //死亡処理
        if (status.health <= 0)
        {
            Dead();
        }
    }
    //private
    void GetPlayerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // m_velocity = (mousePos - screenPos).normalized * speed;
        // m_velocity = -(mousePos - screenPos).normalized * speed;
        // m_velocity.z = m_velocity.y;
        // m_velocity.y = 0.0f;
        var aimvel = -(mousePos - screenPos).normalized * speed;
        aimvel.z = aimvel.y;
        aimvel.y = 0.0f;
        // m_velocity = Vector3.SmoothDamp(m_velocity, aimvel, ref current_m_vel, smoothMoveTime);
        m_velocity = ((mousePos - screenPos).magnitude > stopDistance) ? aimvel : Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            if (fireInterval < 0)
            {
                var forward = transform.forward;
                Fire(new Vector3(forward.x, 0, forward.z) - transform.position);
                fireInterval = fireRate;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            forceMinionsAttack = (!forceMinionsAttack);
            foreach (var minion in chargeMinionCompList)
            {
                minion.ToggleAttacking(forceMinionsAttack);
            }
        }
    }
    void initMinions()
    {
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        foreach (var minion in minions)
        {
            if (minion.GetComponent<Minion>().isCharge)
            {
                chargeMinionList.Add(minion);
                chargeMinionCompList.Add(minion.GetComponent<Minion>());
            }
            else
            {
                supportMinionList.Add(minion);
                supportMinionCompList.Add(minion.GetComponent<Minion>());
            }
        }
        //set following
        for (int i = 0; i < chargeMinionList.Count; i++)
        {
            var minion = chargeMinionList[i].GetComponent<Minion>();
            if (i == 0)
            {
                // Debug.Log(i.ToString() + "th debug");

                minion.leader = transform.gameObject;
            }
            else
            {
                minion.leader = chargeMinionList[i - 1];
            }
            if (i != chargeMinionList.Count - 1)
            {
                minion.follower = chargeMinionList[i + 1];
            }
            else
            {
                minion.follower = null;
            }
        }
        for (int i = 0; i < supportMinionList.Count; i++)
        {
            var minion = supportMinionList[i].GetComponent<Minion>();
            if (i == 0)
            {
                // Debug.Log(i.ToString() + "th debug");

                minion.leader = transform.gameObject;
            }
            else
            {
                minion.leader = supportMinionList[i - 1];
            }
            if (i != supportMinionList.Count - 1)
            {
                minion.follower = supportMinionList[i + 1];
            }
            else
            {
                minion.follower = null;
            }
        }
    }

    protected new void Dead()
    {
        if (destroyObjectList.Count != 0)
        {
            foreach (var destroyObject in destroyObjectList)
            {
                Instantiate(destroyObject, transform.position, transform.rotation);
            }
        }
        foreach (var minion in chargeMinionCompList)
        {
            minion.leader = null;
        }
        foreach (var minion in supportMinionCompList)
        {
            minion.leader = null;
        }
        // Destroy(transform.gameObject);
        transform.gameObject.SetActive(false);
    }
    void Fire(Vector3 target)
    {
        // var attackObject = Instantiate(bullet, transform.position + transform.forward * fireDistance, transform.rotation);
        var fire_bullet = GameObject.Instantiate(bullet, transform.position + transform.forward * fireDistance, transform.rotation);
        var comp_bullet = fire_bullet.GetComponent<BulletBase>();
        comp_bullet.SetInpactPoint(target);
    }
    // void CheckMinionList()
    // {
    //     foreach (var minion in minionList)
    //     {
    //         if (minion == null)
    //         {
    //             minionList.Remove(minion);

    //         }
    //     }
    // }
}
