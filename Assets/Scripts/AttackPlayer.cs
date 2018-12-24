using System.Collections;
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
    [SerializeField] float attackRate;
    [SerializeField] float power;

    bool forceMinionsAttack;

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
        StatusCheck();
        BuffProcess();
        Move();
    }
    protected new void StatusCheck()
    {
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
        m_velocity = -(mousePos - screenPos).normalized * speed;
        m_velocity.z = m_velocity.y;
        m_velocity.y = 0.0f;

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
