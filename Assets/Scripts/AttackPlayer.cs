using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : Entity
{
    public float speed = 0.2f;

    [SerializeField] List<GameObject> minionList;
    List<Minion> minionCompList;
    [SerializeField] float attackRate;
    [SerializeField] float power;

    bool forceMinionsAttack;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        GetPlayerInput();
        // CheckMinionList();
        base.Update();
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

        if (Input.GetMouseButtonDown(0))
        {
            forceMinionsAttack = (!forceMinionsAttack);
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
