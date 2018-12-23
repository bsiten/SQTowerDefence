using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : Entity
{
    // public float speed = 0.2f;

    public List<GameObject> minionList;
    public List<Minion> minionCompList = new List<Minion>();
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
            foreach (var minion in minionCompList)
            {
                minion.ToggleAttacking(forceMinionsAttack);
            }
        }
    }
    void initMinions()
    {
        for (int i = 0; i < minionList.Count; i++)
        {
            // Debug.Log(i.ToString() + "th debug");
            var minion = minionList[i].GetComponent<Minion>();
            minionCompList.Add(minion);
            if (i == 0)
            {
                // Debug.Log(i.ToString() + "th debug");

                minion.leader = transform.gameObject;
            }
            else
            {
                minion.leader = minionList[i - 1];
            }
            if (i != minionList.Count - 1)
            {
                minion.follower = minionList[i + 1];
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
