using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    [SerializeField] float maxMovementSpeed;
    [SerializeField] Status status;

    [SerializeField] List<GameObject> destroyObjectList;

    Vector3 m_velocity;

    public void Start()
    {
        status.Reset();
        // foreach (var destroyObject in destroyObjectList)
        // {
        //     Instantiate(destroyObject, transform.position + Vector3.up, transform.rotation);
        // }
    }

    public void Update()
    {
        StatusCheck();
        BuffProcess();
        Move();
    }

    //概要：
    // Entityのステータス管理．
    [System.Serializable]
    public struct Status
    {
        public float health;
        public float maxHealth;
        [SerializeField] HashSet<Buff> buffList;

        public void Reset()
        {
            health = maxHealth;
            buffList = new HashSet<Buff>();
        }

        public void ClearBuff()
        {
            buffList.Clear();
        }
        public void AddBuff(Buff buff)
        {
            buffList.Add(buff);
        }

    }

    //概要:
    // Entityに与える効果の汎用クラス
    // buffの種類ごとの処理はEntity側で実装する．
    public struct Buff
    {
        string name;    //id
        uint level;     //intensity
        float duration; //duration time
    }

    //////////////////////////////////////////////////////////////////////////////
    //protected virtual member function

    //概要:
    // Entityをvelocity方向に移動．maxMovementSpeedで制限する．
    protected void Move()
    {
        if (m_velocity.magnitude > maxMovementSpeed)
        {
            m_velocity = m_velocity.normalized * maxMovementSpeed;
        }
        transform.position += m_velocity * Time.deltaTime;
    }
    //概要
    // 各buffごとの処理
    protected void BuffProcess() { }

    //概要
    // Status値ごとの処理
    //死亡などを実行する．
    protected void StatusCheck()
    {
        //死亡処理
        if (status.health <= 0)
        {
            if (destroyObjectList.Count != 0)
            {
                foreach (var destroyObject in destroyObjectList)
                {
                    Instantiate(destroyObject, transform.position, transform.rotation);
                }
            }
            Destroy(transform.gameObject);
        }
    }

    //////////////////////////////////////////////////////////////////////////////
    //private member function

    //debug用
    private void CalcVelocity()
    {
        m_velocity = movementSpeed * Vector3.right;
    }

}
